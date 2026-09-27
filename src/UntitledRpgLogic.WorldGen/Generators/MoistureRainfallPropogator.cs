using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.GridMaps;

namespace UntitledRpgLogic.WorldGen.Generators;

public record MoistureRainfallMaps(MoistureMap moisture, RainfallMap rainfall);

public static class MoistureRainfallPropogator
{
	public static MoistureRainfallMaps PropogateMoistureAndRainfall(ReadOnlyWorldGenContext context)
	{
		var width = context.MapConfig.WidthTiles;
		var height = context.MapConfig.HeightTiles;
		var totalTiles = width * height;

		// Flattened arrays for high-speed, zero-allocation processing
		var capacityMap = new float[totalTiles];
		var isOceanMap = new bool[totalTiles];
		var backtraceX = new float[totalTiles];
		var backtraceY = new float[totalTiles];

		var currentMoisture = new float[totalTiles];
		var nextMoisture = new float[totalTiles];
		var accumulatedRain = new float[totalTiles];

		// setup, pre-populate the non-changing information from other noise maps
		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				var index = (y * width) + x;

				var temp = context.Climate.GetTemperature(x, y);
				var elevation = Math.Max(0f, context.Terrain.GetElevation(x, y));
				var isOcean = context.Hydrology.GetWaterBodyType(x, y) == WaterBodyType.Ocean;

				var capacity = Math.Max(0f, (temp + 20f) / 50f) * MathF.Exp(-elevation / 2000f);
				capacityMap[index] = capacity;
				isOceanMap[index] = isOcean;

				if (isOcean)
				{
					currentMoisture[index] = capacity;
				}

				// Decode the Polar Wind Vector
				var wind = context.Climate.GetWindVector(x, y);
				var heading = wind.X; // Angle in radians
				var velocity = wind.Y; // Speed magnitude

				// Convert to true Cartesian distance
				// We scale velocity by 0.5 to prevent the fluid from "jumping" over mountains in a single tick
				var dx = MathF.Cos(heading) * velocity * 0.5f;
				var dy = MathF.Sin(heading) * velocity * 0.5f;

				// Calculate the exact floating-point upwind origin
				backtraceX[index] = x - dx;
				backtraceY[index] = y - dy;
			}
		}

		// With velocity driving distance directly, we only need enough steps to cover the map width
		var simulationSteps = (int)(width * 1.5f);
		var ambientRainRate = 0.0005f;

		for (var step = 0; step < simulationSteps; step++)
		{
			for (var i = 0; i < totalTiles; i++)
			{
				var capacity = capacityMap[i];
				float tileMoisture;

				if (isOceanMap[i])
				{
					// Oceans always enforce maximum capacity
					tileMoisture = capacity;
				}
				else
				{
					// Bilinear Interpolation: Sample the exact floating-point air mass upwind
					var sourceX = Math.Clamp(backtraceX[i], 0f, width - 1.001f);
					var sourceY = Math.Clamp(backtraceY[i], 0f, height - 1.001f);

					var x0 = (int)sourceX;
					var y0 = (int)sourceY;
					var x1 = x0 + 1;
					var y1 = y0 + 1;

					var tx = sourceX - x0;
					var ty = sourceY - y0;

					var val00 = currentMoisture[(y0 * width) + x0];
					var val10 = currentMoisture[(y0 * width) + x1];
					var val01 = currentMoisture[(y1 * width) + x0];
					var val11 = currentMoisture[(y1 * width) + x1];

					var top = float.Lerp(val00, val10, tx);
					var bottom = float.Lerp(val01, val11, tx);

					// The volume physically moves inward without needing a transfer-rate clamp
					tileMoisture = float.Lerp(top, bottom, ty);
				}

				// Calculate Precipitation
				var precipitation = accumulatedRain[i];

				if (tileMoisture > capacity)
				{
					precipitation += tileMoisture - capacity;
					tileMoisture = capacity;
				}

				var ambientRain = tileMoisture * ambientRainRate;
				precipitation += ambientRain;
				tileMoisture -= ambientRain;

				nextMoisture[i] = tileMoisture;
				accumulatedRain[i] = precipitation;
			}

			// buffer swap
			(currentMoisture, nextMoisture) = (nextMoisture, currentMoisture);
		}

		// 3. NORMALIZE AND MAP
		var finalMoistureMap = new MoistureMap(width, height);
		var finalRainMap = new RainfallMap(width, height);

		var maxRain = 0.001f;
		for (var i = 0; i < totalTiles; i++)
		{
			maxRain = Math.Max(maxRain, accumulatedRain[i]);
		}

		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				var index = (y * width) + x;
				finalMoistureMap.SetMoistureCapacity(x, y, Math.Clamp(currentMoisture[index], 0f, 1f));
				finalRainMap.SetRainfall(x, y, Math.Clamp(accumulatedRain[index] / maxRain, 0f, 1f));
			}
		}

		return new MoistureRainfallMaps(finalMoistureMap, finalRainMap);
	}
}

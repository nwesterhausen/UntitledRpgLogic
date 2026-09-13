namespace UntitledRpgLogic.WorldGen.Noise;

/// <summary>
///     Thread-safe, deterministic 2D Simplex/Gradient noise generator.
/// </summary>
public static class SimplexNoise
{
    private const float F2 = 0.5f * (1.7320508f - 1.0f);
    private const float G2 = (3.0f - 1.7320508f) / 6.0f;

    /// <summary>
    ///     Evaluates 2D simplex noise at the given coordinate for a specific seed.
    ///     Returns a value in the normalized range [-1.0, 1.0].
    /// </summary>
    public static float Sample(float x, float y, uint seed)
    {
        var s = (x + y) * F2;
        var i = FastFloor(x + s);
        var j = FastFloor(y + s);

        var t = (i + j) * G2;
        var x0 = x - (i - t);
        var y0 = y - (j - t);

        int i1, j1;
        if (x0 > y0)
        {
            i1 = 1;
            j1 = 0;
        }
        else
        {
            i1 = 0;
            j1 = 1;
        }

        var x1 = x0 - i1 + G2;
        var y1 = y0 - j1 + G2;
        var x2 = x0 - 1.0f + (2.0f * G2);
        var y2 = y0 - 1.0f + (2.0f * G2);

        var n0 = 0.0f;
        var n1 = 0.0f;
        var n2 = 0.0f;

        var t0 = 0.5f - (x0 * x0) - (y0 * y0);
        if (t0 > 0.0f)
        {
            t0 *= t0;
            n0 = t0 * t0 * Grad(Hash(i, j, seed), x0, y0);
        }

        var t1 = 0.5f - (x1 * x1) - (y1 * y1);
        if (t1 > 0.0f)
        {
            t1 *= t1;
            n1 = t1 * t1 * Grad(Hash(i + i1, j + j1, seed), x1, y1);
        }

        var t2 = 0.5f - (x2 * x2) - (y2 * y2);
        if (t2 > 0.0f)
        {
            t2 *= t2;
            n2 = t2 * t2 * Grad(Hash(i + 1, j + 1, seed), x2, y2);
        }

        return 70.0f * (n0 + n1 + n2);
    }

    /// <summary>
    ///     Samples multi-octave Fractional Brownian Motion (fBm).
    /// </summary>
    public static float SampleFbm(
        float x,
        float y,
        uint seed,
        int octaves = 5,
        float persistence = 0.5f,
        float lacunarity = 2.0f)
    {
        var total = 0.0f;
        var frequency = 1.0f;
        var amplitude = 1.0f;
        var maxValue = 0.0f;

        for (var i = 0; i < octaves; i++)
        {
            total += Sample(x * frequency, y * frequency, seed + (uint)(i * 31)) * amplitude;
            maxValue += amplitude;
            amplitude *= persistence;
            frequency *= lacunarity;
        }

        return total / maxValue;
    }

    private static int FastFloor(float x) => x > 0 ? (int)x : (int)x - 1;

    private static uint Hash(int x, int y, uint seed)
    {
        var ux = (uint)x;
        var uy = (uint)y;
        var h = seed ^ (ux * 374761393u) ^ (uy * 668265263u);
        h = (h ^ (h >> 13)) * 1274126177u;
        return h ^ (h >> 16);
    }

    private static float Grad(uint hash, float x, float y)
    {
        var h = hash & 7;
        var u = h < 4 ? x : y;
        var v = h < 4 ? y : x;
        return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
    }
}

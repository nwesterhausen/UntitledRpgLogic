using System.Linq.Expressions;
using Microsoft.Extensions.Logging.Abstractions;
using UntitledRpg.LibraryFile;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Materials;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Infrastructure.Configuration;
using UntitledRpgLogic.Services.Data;
using Version = UntitledRpg.LibraryFile.Version;

namespace UntitledRpgLogic.UnitTests.Data;

[TestClass]
public sealed class PackageLoaderServiceTests
{
	private static readonly ItemDefinition ItemDef = new(new Name("Iron Bar"))
	{
		ItemType = ItemType.Consumable, ItemSubtype = ItemSubtype.Bullion
	};

	private static readonly StatDefinition StatDef = new(new Name("Strength")) { Variation = StatVariation.Major };
	private static readonly MaterialDefinition MaterialDef = new(new Name("Iron"));

	private static DefinitionSerializerRouter CreateRouter() =>
		new([new FakeSerializationService()]);

	[TestMethod]
	public async Task IngestPackageAsync_ValidStream_PersistsAllEntitiesAndCommits()
	{
		var uow = new FakeUnitOfWork();
		await using (uow.ConfigureAwait(false))
		{
			var router = CreateRouter();
			var statRepo = new FakeEntityRepository<StatDefinition>();
			var skillRepo = new FakeEntityRepository<SkillDefinition>();
			var itemRepo = new FakeEntityRepository<ItemDefinition>();
			var matRepo = new FakeEntityRepository<MaterialDefinition>();
			var entityRepo = new FakeEntityRepository<EntityDefinition>();
			var logger = NullLogger<PackageLoaderService>.Instance;

			var loader = new PackageLoaderService(
				uow,
				router,
				statRepo,
				skillRepo,
				itemRepo,
				matRepo,
				entityRepo,
				logger);

			var manifest = new PackageManifest
			{
				Id = Ulid.NewUlid(), Name = "Core Mod", AuthorName = "Dev", Version = new Version()
			};

			// Path segments "items" and "stats" are mapped by DefinitionSerializerRouter
			var dummyFiles = new Dictionary<string, byte[]>
			{
				["items/iron_bar.toml"] = "title = \"dummy\""u8.ToArray(),
				["stats/strength.toml"] = "title = \"dummy\""u8.ToArray()
			};

			var tempPackagePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.urpglib");
			try
			{
				await UrpglibWriter.WriteAsync(tempPackagePath, manifest, dummyFiles).ConfigureAwait(false);

				var fileStream = File.OpenRead(tempPackagePath);
				await using (fileStream.ConfigureAwait(false))
				{
					var result = await loader.IngestPackageAsync(fileStream).ConfigureAwait(false);

					Assert.IsNotNull(result);
					Assert.AreEqual(1, result.ItemsLoaded);
					Assert.AreEqual(1, result.StatsLoaded);
					Assert.AreEqual(2, result.TotalDefinitionsLoaded);
					Assert.IsTrue(uow.TransactionStarted);
					Assert.IsTrue(uow.ChangesSaved);
					Assert.IsTrue(uow.TransactionCommitted);
					Assert.IsFalse(uow.TransactionRolledBack);
					Assert.HasCount(1, itemRepo.AddedEntities);
					Assert.HasCount(1, statRepo.AddedEntities);
				}
			}
			finally
			{
				if (File.Exists(tempPackagePath))
				{
					File.Delete(tempPackagePath);
				}
			}
		}
	}

	[TestMethod]
	public async Task IngestPackageAsync_FilePathOverload_Succeeds()
	{
		var uow = new FakeUnitOfWork();
		await using (uow.ConfigureAwait(false))
		{
			var router = CreateRouter();
			var statRepo = new FakeEntityRepository<StatDefinition>();
			var skillRepo = new FakeEntityRepository<SkillDefinition>();
			var itemRepo = new FakeEntityRepository<ItemDefinition>();
			var matRepo = new FakeEntityRepository<MaterialDefinition>();
			var entityRepo = new FakeEntityRepository<EntityDefinition>();
			var logger = NullLogger<PackageLoaderService>.Instance;

			var loader = new PackageLoaderService(
				uow,
				router,
				statRepo,
				skillRepo,
				itemRepo,
				matRepo,
				entityRepo,
				logger);

			var manifest = new PackageManifest
			{
				Id = Ulid.NewUlid(), Name = "Materials Mod", AuthorName = "Dev", Version = new Version()
			};

			// Path segment "materials" is mapped by DefinitionSerializerRouter
			var dummyFiles = new Dictionary<string, byte[]> { ["materials/iron.toml"] = "name = \"Iron\""u8.ToArray() };

			var tempPackagePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.urpglib");
			try
			{
				await UrpglibWriter.WriteAsync(tempPackagePath, manifest, dummyFiles).ConfigureAwait(false);

				var result = await loader.IngestPackageAsync(tempPackagePath).ConfigureAwait(false);

				Assert.IsNotNull(result);
				Assert.AreEqual(1, result.MaterialsLoaded);
				Assert.IsTrue(uow.TransactionCommitted);
				Assert.HasCount(1, matRepo.AddedEntities);
			}
			finally
			{
				if (File.Exists(tempPackagePath))
				{
					File.Delete(tempPackagePath);
				}
			}
		}
	}

	private sealed class FakeSerializationService : IDefinitionSerializationService
	{
		public string DefaultFileExtension => ".toml";
		public IReadOnlyCollection<string> SupportedFileExtensions => [".toml"];

		public TModel Deserialize<TModel>(string content) where TModel : class, IDefined =>
			(TModel)this.DeserializeInto(typeof(TModel), Stream.Null);

		public TModel Deserialize<TModel>(Stream stream) where TModel : class, IDefined =>
			(TModel)this.DeserializeInto(typeof(TModel), stream);

		public string Serialize<TModel>(TModel model) where TModel : class, IDefined => string.Empty;

		public void Serialize<TModel>(TModel model, Stream stream) where TModel : class, IDefined { }

		public IDefined DeserializeInto(Type definitionType, string content) =>
			this.DeserializeInto(definitionType, Stream.Null);

		public IDefined DeserializeInto(Type definitionType, Stream stream)
		{
			if (definitionType == typeof(ItemDefinition))
			{
				return ItemDef;
			}

			if (definitionType == typeof(StatDefinition))
			{
				return StatDef;
			}

			if (definitionType == typeof(MaterialDefinition))
			{
				return MaterialDef;
			}

			throw new NotSupportedException($"Unsupported definition type: {definitionType.Name}");
		}
	}

	private sealed class FakeUnitOfWork : IUnitOfWork
	{
		public bool TransactionStarted { get; private set; }
		public bool TransactionCommitted { get; private set; }
		public bool TransactionRolledBack { get; private set; }
		public bool ChangesSaved { get; private set; }

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			this.ChangesSaved = true;
			return Task.FromResult(1);
		}

		public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
		{
			this.TransactionStarted = true;
			return Task.CompletedTask;
		}

		public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
		{
			this.TransactionCommitted = true;
			return Task.CompletedTask;
		}

		public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
		{
			this.TransactionRolledBack = true;
			return Task.CompletedTask;
		}

		public void Dispose() { }
		public ValueTask DisposeAsync() => ValueTask.CompletedTask;
	}

	private sealed class FakeEntityRepository<TEntity> : IEntityRepository<TEntity, Ulid>
		where TEntity : class, IDbEntity<Ulid>
	{
		public List<TEntity> AddedEntities { get; } = [];

		public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			this.AddedEntities.Add(entity);
			return Task.CompletedTask;
		}

		public Task<TEntity?> GetByIdAsync(Ulid id, CancellationToken cancellationToken = default,
			params Expression<Func<TEntity, object?>>[] includes) =>
			Task.FromResult<TEntity?>(null);

		public Task<TEntity?> GetByIdAsync(Ulid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
			CancellationToken cancellationToken = default) =>
			Task.FromResult<TEntity?>(null);

		public Task<IReadOnlyList<TEntity>> GetByIdsAsync(IEnumerable<Ulid> ids,
			CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[] includes) =>
			Task.FromResult<IReadOnlyList<TEntity>>([]);

		public void Update(TEntity entity) { }
		public void Remove(TEntity entity) { }
	}
}

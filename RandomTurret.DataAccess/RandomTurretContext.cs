namespace RandomTurret.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.DataAccess.Configuration;
    using RandomTurret.DataAccess.Seed;
    using RandomTurret.Entities;

    public class RandomTurretContext : DbContext
    {
        public RandomTurretContext(DbContextOptions<RandomTurretContext> options)
            : base(options)
        {
        }

        public DbSet<GemPlayerEntity> GemPlayers { get; set; }

        public DbSet<GemEntity> Gems { get; set; }

        public DbSet<GemStatEntity> GemStats { get; set; }

        public DbSet<MobEntity> Mobs { get; set; }

        public DbSet<MobStatEntity> MobStats { get; set; }

        public DbSet<PlayerEntity> Players { get; set; }

        public DbSet<RarityEntity> Rarities { get; set; }

        public DbSet<StatEntity> Stats { get; set; }

        public DbSet<TemplatePlayerEntity> TemplatePlayers { get; set; }

        public DbSet<TemplateEntity> Templates { get; set; }

        public DbSet<TemplateStatEntity> TemplateStats { get; set; }

        public DbSet<TowerEntity> Towers { get; set; }

        public DbSet<TowerStatEntity> TowerStats { get; set; }

        public DbSet<WaveEntity> Waves { get; set; }

        public DbSet<WaveMobEntity> WavesMobs { get; set; }

        public async Task EnsureSeedData(bool isProduction)
        {
            ContextInitializer initializer = new ContextInitializer();
            await initializer.Seed(this, isProduction);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Action> listConfiguration = new List<Action>
            {
                new PlayerConfiguration(modelBuilder).Execute,
                new StatConfiguration(modelBuilder).Execute,
                new TowerConfiguration(modelBuilder).Execute,
                new TowerStatConfiguration(modelBuilder).Execute,
                new MobConfiguration(modelBuilder).Execute,
                new MobStatConfiguration(modelBuilder).Execute,
                new WaveConfiguration(modelBuilder).Execute,
                new WaveMobConfiguration(modelBuilder).Execute,
                new RarityConfiguration(modelBuilder).Execute,
                new TemplateConfiguration(modelBuilder).Execute,
                new TemplateStatConfiguration(modelBuilder).Execute,
                new GemConfiguration(modelBuilder).Execute,
                new GemStatConfiguration(modelBuilder).Execute,
                new TemplatePlayerConfiguration(modelBuilder).Execute,
                new GemPlayerConfiguration(modelBuilder).Execute,
            };

            foreach (Action action in listConfiguration)
            {
                action.Invoke();
            }
        }
    }
}
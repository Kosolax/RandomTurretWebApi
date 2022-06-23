namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class WaveConfiguration : ConfigurationManagement<WaveEntity>
    {
        public WaveConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(waveEntity => waveEntity.Id);
            this.EntityConfiguration.Property(waveEntity => waveEntity.WaveNumber).IsRequired(true).HasColumnType("int");
            this.EntityConfiguration.Property(waveEntity => waveEntity.DifficultyMultiplier).IsRequired(true).HasColumnType("float");
        }

        protected override void ProcessForeignKey()
        {
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Waves");
        }
    }
}
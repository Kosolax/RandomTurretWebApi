namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class WaveMobConfiguration : ConfigurationManagement<WaveMobEntity>
    {
        public WaveMobConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(waveMobEntity => waveMobEntity.Id);
            this.EntityConfiguration.Property(waveMobEntity => waveMobEntity.Position).IsRequired(true).HasColumnType("int");
        }

        protected override void ProcessForeignKey()
        {
            this.EntityConfiguration.HasOne(waveMobEntity => waveMobEntity.MobEntity).WithMany().IsRequired(true).HasForeignKey(waveMobEntity => waveMobEntity.MobEntityId).OnDelete(DeleteBehavior.Cascade);
            this.EntityConfiguration.HasOne(waveMobEntity => waveMobEntity.WaveEntity).WithMany().IsRequired(true).HasForeignKey(towerStatEntity => towerStatEntity.WaveEntityId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("WavesMobs");
        }
    }
}
namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class TowerStatConfiguration : ConfigurationManagement<TowerStatEntity>
    {
        public TowerStatConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(towerStatEntity => towerStatEntity.Id);
            this.EntityConfiguration.Property(towerStatEntity => towerStatEntity.Value).IsRequired(true).HasColumnType("float");
        }

        protected override void ProcessForeignKey()
        {
            this.EntityConfiguration.HasOne(towerStatEntity => towerStatEntity.StatEntity).WithMany().IsRequired(true).HasForeignKey(towerStatEntity => towerStatEntity.StatEntityId).OnDelete(DeleteBehavior.Cascade);
            this.EntityConfiguration.HasOne(towerStatEntity => towerStatEntity.TowerEntity).WithMany().IsRequired(true).HasForeignKey(towerStatEntity => towerStatEntity.TowerEntityId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("TowerStats");
        }
    }
}
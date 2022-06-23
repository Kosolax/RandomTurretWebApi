namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class TowerConfiguration : ConfigurationManagement<TowerEntity>
    {
        public TowerConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(towerEntity => towerEntity.Id);
            this.EntityConfiguration.Property(towerEntity => towerEntity.Name).IsRequired(true).HasColumnType("varchar(100)");
            this.EntityConfiguration.Property(towerEntity => towerEntity.TowerType).IsRequired(true).HasColumnType("int");
        }

        protected override void ProcessForeignKey()
        {
            this.EntityConfiguration.HasOne(towerEntity => towerEntity.PlayerEntity).WithMany().IsRequired(true).HasForeignKey(towerEntity => towerEntity.PlayerEntityId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Towers");
        }
    }
}
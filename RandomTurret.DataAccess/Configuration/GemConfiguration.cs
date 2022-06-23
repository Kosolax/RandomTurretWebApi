namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class GemConfiguration : ConfigurationManagement<GemEntity>
    {
        public GemConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(x => x.Id);
            this.EntityConfiguration.Property(x => x.GemType).IsRequired(true).HasColumnType("int");
            this.EntityConfiguration.Property(x => x.ImpactType).IsRequired(false).HasColumnType("int");
            this.EntityConfiguration.Property(x => x.MergeType).IsRequired(false).HasColumnType("int");
            this.EntityConfiguration.Property(x => x.ShootType).IsRequired(false).HasColumnType("int");
        }

        protected override void ProcessForeignKey()
        {
            this.EntityConfiguration.HasOne(x => x.Rarity).WithMany().IsRequired(true).HasForeignKey(x => x.RarityId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Gems");
        }
    }
}
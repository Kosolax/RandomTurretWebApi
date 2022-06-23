namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class GemStatConfiguration : ConfigurationManagement<GemStatEntity>
    {
        public GemStatConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(x => x.Id);
            this.EntityConfiguration.Property(x => x.Value).IsRequired(true).HasColumnType("float");
        }

        protected override void ProcessForeignKey()
        {
            this.EntityConfiguration.HasOne(x => x.Rarity).WithMany().IsRequired(true).HasForeignKey(x => x.RarityId).OnDelete(DeleteBehavior.Cascade);
            this.EntityConfiguration.HasOne(x => x.Stat).WithMany().IsRequired(true).HasForeignKey(x => x.StatId).OnDelete(DeleteBehavior.Cascade);
            this.EntityConfiguration.HasOne(x => x.Gem).WithMany().IsRequired(true).HasForeignKey(x => x.GemId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("GemStats");
        }
    }
}
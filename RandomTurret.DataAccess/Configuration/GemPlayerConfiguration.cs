namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class GemPlayerConfiguration : ConfigurationManagement<GemPlayerEntity>
    {
        public GemPlayerConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(x => x.Id);
        }

        protected override void ProcessForeignKey()
        {
            this.EntityConfiguration.HasOne(x => x.Player).WithMany().IsRequired(true).HasForeignKey(x => x.PlayerId).OnDelete(DeleteBehavior.Cascade);
            this.EntityConfiguration.HasOne(x => x.Gem).WithMany().IsRequired(true).HasForeignKey(x => x.GemId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("GemPlayers");
        }
    }
}
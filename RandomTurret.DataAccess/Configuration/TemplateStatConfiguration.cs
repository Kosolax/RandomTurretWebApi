namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class TemplateStatConfiguration : ConfigurationManagement<TemplateStatEntity>
    {
        public TemplateStatConfiguration(ModelBuilder modelBuilder)
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
            this.EntityConfiguration.HasOne(x => x.Template).WithMany().IsRequired(true).HasForeignKey(x => x.TemplateId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("TemplateStats");
        }
    }
}
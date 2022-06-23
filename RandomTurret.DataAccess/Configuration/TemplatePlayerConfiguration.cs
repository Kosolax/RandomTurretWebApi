namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class TemplatePlayerConfiguration : ConfigurationManagement<TemplatePlayerEntity>
    {
        public TemplatePlayerConfiguration(ModelBuilder modelBuilder)
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
            this.EntityConfiguration.HasOne(x => x.Template).WithMany().IsRequired(true).HasForeignKey(x => x.TemplateId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("TemplatePlayers");
        }
    }
}
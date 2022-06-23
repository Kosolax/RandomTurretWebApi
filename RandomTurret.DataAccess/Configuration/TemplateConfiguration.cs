namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class TemplateConfiguration : ConfigurationManagement<TemplateEntity>
    {
        public TemplateConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(x => x.Id);
            this.EntityConfiguration.Property(x => x.TemplateType).IsRequired(true).HasColumnType("int");
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
            this.EntityConfiguration.ToTable("Templates");
        }
    }
}
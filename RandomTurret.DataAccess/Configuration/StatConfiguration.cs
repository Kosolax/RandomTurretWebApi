namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class StatConfiguration : ConfigurationManagement<StatEntity>
    {
        public StatConfiguration(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(statEntity => statEntity.Id);
            this.EntityConfiguration.Property(statEntity => statEntity.StatType).IsRequired(true).HasColumnType("int");
        }

        protected override void ProcessForeignKey()
        {
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Stats");
        }
    }
}
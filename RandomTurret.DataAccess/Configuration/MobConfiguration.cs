namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class MobConfiguration : ConfigurationManagement<MobEntity>
    {
        public MobConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(mobEntity => mobEntity.Id);
            this.EntityConfiguration.Property(mobEntity => mobEntity.MobType).IsRequired(true).HasColumnType("int");
        }

        protected override void ProcessForeignKey()
        {
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Mobs");
        }
    }
}
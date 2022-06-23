namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class MobStatConfiguration : ConfigurationManagement<MobStatEntity>
    {
        public MobStatConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(mobStatEntity => mobStatEntity.Id);
            this.EntityConfiguration.Property(mobStatEntity => mobStatEntity.Value).IsRequired(true).HasColumnType("float");
        }

        protected override void ProcessForeignKey()
        {
            this.EntityConfiguration.HasOne(mobStatEntity => mobStatEntity.StatEntity).WithMany().IsRequired(true).HasForeignKey(mobStatEntity => mobStatEntity.StatEntityId).OnDelete(DeleteBehavior.Cascade);
            this.EntityConfiguration.HasOne(mobStatEntity => mobStatEntity.MobEntity).WithMany().IsRequired(true).HasForeignKey(mobStatEntity => mobStatEntity.MobEntityId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("MobStats");
        }
    }
}
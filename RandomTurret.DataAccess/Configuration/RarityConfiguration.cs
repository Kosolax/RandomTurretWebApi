namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class RarityConfiguration : ConfigurationManagement<RarityEntity>
    {
        public RarityConfiguration(ModelBuilder modelBuilder)
           : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(rarityType => rarityType.Id);
            this.EntityConfiguration.Property(rarityType => rarityType.RarityType).IsRequired(true).HasColumnType("int");
            this.EntityConfiguration.Property(rarityType => rarityType.LootValue).IsRequired(true).HasColumnType("float");
        }

        protected override void ProcessForeignKey()
        {
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Rarities");
        }
    }
}
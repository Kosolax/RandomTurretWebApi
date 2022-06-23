namespace RandomTurret.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class PlayerConfiguration : ConfigurationManagement<PlayerEntity>
    {
        public PlayerConfiguration(ModelBuilder modelBuilder)
           : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(playerEntity => playerEntity.Id);
            this.EntityConfiguration.Property(playerEntity => playerEntity.Gold).IsRequired(true).HasColumnType("integer");
            this.EntityConfiguration.Property(playerEntity => playerEntity.Elo).IsRequired(true).HasColumnType("integer");
            this.EntityConfiguration.Property(playerEntity => playerEntity.Pseudo).IsRequired(true).HasColumnType("varchar(20)");
            this.EntityConfiguration.Property(playerEntity => playerEntity.Mail).IsRequired(true).HasColumnType("varchar(100)");
            this.EntityConfiguration.Property(playerEntity => playerEntity.Password).IsRequired(true).HasColumnType("varchar(100)");
        }

        protected override void ProcessForeignKey()
        {
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Players");
        }
    }
}
namespace RandomTurret.Entities
{
    using RandomTurret.Entities.Enum;

    public class TowerEntity : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PlayerEntity PlayerEntity { get; set; }

        public int PlayerEntityId { get; set; }

        public TowerType TowerType { get; set; }
    }
}
namespace RandomTurret.Entities
{
    public class TowerStatEntity : BaseEntity
    {
        public int Id { get; set; }

        public StatEntity StatEntity { get; set; }

        public int StatEntityId { get; set; }

        public TowerEntity TowerEntity { get; set; }

        public int TowerEntityId { get; set; }

        public float Value { get; set; }
    }
}
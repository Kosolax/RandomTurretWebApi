namespace RandomTurret.Entities
{
    using RandomTurret.Entities.Enum;

    public class StatEntity : BaseEntity
    {
        public int Id { get; set; }

        public StatType StatType { get; set; }
    }
}
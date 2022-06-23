namespace RandomTurret.Entities
{
    using RandomTurret.Entities.Enum;

    public class MobEntity : BaseEntity
    {
        public int Id { get; set; }

        public MobType MobType { get; set; }
    }
}
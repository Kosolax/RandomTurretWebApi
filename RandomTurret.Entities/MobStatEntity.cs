namespace RandomTurret.Entities
{
    public class MobStatEntity : BaseEntity
    {
        public int Id { get; set; }

        public MobEntity MobEntity { get; set; }

        public int MobEntityId { get; set; }

        public StatEntity StatEntity { get; set; }

        public int StatEntityId { get; set; }

        public float Value { get; set; }
    }
}
namespace RandomTurret.Entities
{
    using RandomTurret.Entities.Enum;

    public class GemEntity : BaseEntity
    {
        public GemType GemType { get; set; }

        public int Id { get; set; }

        public ImpactType? ImpactType { get; set; }

        public MergeType? MergeType { get; set; }

        public RarityEntity Rarity { get; set; }

        public int RarityId { get; set; }

        public ShootType? ShootType { get; set; }
    }
}
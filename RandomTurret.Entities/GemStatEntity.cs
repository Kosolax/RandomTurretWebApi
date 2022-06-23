namespace RandomTurret.Entities
{
    public class GemStatEntity : BaseEntity
    {
        public GemEntity Gem { get; set; }

        public int GemId { get; set; }

        public int Id { get; set; }

        public RarityEntity Rarity { get; set; }

        public int RarityId { get; set; }

        public StatEntity Stat { get; set; }

        public int StatId { get; set; }

        public float Value { get; set; }
    }
}
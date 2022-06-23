namespace RandomTurret.Entities
{
    public class TemplateStatEntity : BaseEntity
    {
        public int Id { get; set; }

        public RarityEntity Rarity { get; set; }

        public int RarityId { get; set; }

        public StatEntity Stat { get; set; }

        public int StatId { get; set; }

        public TemplateEntity Template { get; set; }

        public int TemplateId { get; set; }

        public float Value { get; set; }
    }
}
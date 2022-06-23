namespace RandomTurret.Entities
{
    using RandomTurret.Entities.Enum;

    public class TemplateEntity : BaseEntity
    {
        public int Id { get; set; }

        public RarityEntity Rarity { get; set; }

        public int RarityId { get; set; }

        public TemplateType TemplateType { get; set; }
    }
}
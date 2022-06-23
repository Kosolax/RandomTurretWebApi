namespace RandomTurret.Entities
{
    public class TemplatePlayerEntity : BaseEntity
    {
        public int Id { get; set; }

        public PlayerEntity Player { get; set; }

        public int PlayerId { get; set; }

        public TemplateEntity Template { get; set; }

        public int TemplateId { get; set; }
    }
}
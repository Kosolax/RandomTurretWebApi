namespace RandomTurret.Entities
{
    public class GemPlayerEntity : BaseEntity
    {
        public GemEntity Gem { get; set; }

        public int GemId { get; set; }

        public int Id { get; set; }

        public PlayerEntity Player { get; set; }

        public int PlayerId { get; set; }
    }
}
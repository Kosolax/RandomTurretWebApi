namespace RandomTurret.Entities
{
    public class WaveEntity : BaseEntity
    {
        public float DifficultyMultiplier { get; set; }

        public int Id { get; set; }

        public int WaveNumber { get; set; }
    }
}
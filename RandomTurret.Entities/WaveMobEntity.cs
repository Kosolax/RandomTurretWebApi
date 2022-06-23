namespace RandomTurret.Entities
{
    public class WaveMobEntity : BaseEntity
    {
        public int Id { get; set; }

        public MobEntity MobEntity { get; set; }

        public int MobEntityId { get; set; }

        public int Position { get; set; }

        public WaveEntity WaveEntity { get; set; }

        public int WaveEntityId { get; set; }
    }
}
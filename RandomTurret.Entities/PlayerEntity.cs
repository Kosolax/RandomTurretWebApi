namespace RandomTurret.Entities
{
    public class PlayerEntity : BaseEntity
    {
        public int Elo { get; set; }

        public int Gold { get; set; }

        public int Id { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public string Pseudo { get; set; }
    }
}
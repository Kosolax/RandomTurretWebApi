namespace RandomTurret.BusinessObject
{
    using System.Collections.Generic;

    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;

    public class Player : BaseBusinessObject<PlayerEntity>
    {
        public Player()
        {
            this.ValidationService = new PlayerValidation();
        }

        public Player(PlayerEntity entity, bool hasError)
            : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new PlayerValidation();
            }

            this.Id = entity.Id;
            this.Gold = entity.Gold;
            this.Pseudo = entity.Pseudo;
            this.Mail = entity.Mail;
            this.Password = entity.Password;
            this.Elo = entity.Elo;
        }

        public Player(List<Tower> towers, bool hasError, PlayerEntity entity) : this(entity, hasError)
        {
            this.Towers = towers;
        }

        public int Elo { get; set; }

        public int Gold { get; set; }

        public int Id { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public string Pseudo { get; set; }

        public List<Tower> Towers { get; set; }

        public override PlayerEntity CreateEntity()
        {
            return new PlayerEntity
            {
                Id = this.Id,
                Gold = this.Gold,
                Pseudo = this.Pseudo,
                Mail = this.Mail,
                Password = this.Password,
                Elo = this.Elo,
            };
        }
    }
}
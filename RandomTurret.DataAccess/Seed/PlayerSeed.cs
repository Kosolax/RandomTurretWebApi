namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public class PlayerSeed : IContextSeed
    {
        public PlayerSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Players.Any() && !isProduction)
            {
                List<PlayerEntity> listPlayer = new List<PlayerEntity>
                {
                    new PlayerEntity
                    {
                        Gold = 2590,
                        Pseudo = "Geof",
                        Mail = "g.g@g.g",
                        Password = "Bonjour50!",
                        Elo = 1000,
                    },
                    new PlayerEntity
                    {
                        Gold = 2594,
                        Pseudo = "Tom",
                        Mail = "t.t@t.t",
                        Password = "Bonjour50!",
                        Elo = 2000,
                    },
                };

                await this.Context.Players.AddRangeAsync(listPlayer);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}
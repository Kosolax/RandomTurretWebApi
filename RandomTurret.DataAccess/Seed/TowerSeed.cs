namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class TowerSeed : IContextSeed
    {
        public TowerSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Towers.Any() && !isProduction)
            {
                List<TowerEntity> listTower = new List<TowerEntity>
                {
                    new TowerEntity
                    {
                        Name = "Fire",
                        PlayerEntityId = this.Context.Players.FirstOrDefault().Id,
                        TowerType = TowerType.Fire,
                    },
                    new TowerEntity
                    {
                        Name = "Cold",
                        PlayerEntityId = this.Context.Players.FirstOrDefault().Id,
                        TowerType = TowerType.Cold,
                    },
                    new TowerEntity
                    {
                        Name = "Wind",
                        PlayerEntityId = this.Context.Players.FirstOrDefault().Id,
                        TowerType = TowerType.Wind,
                    },
                    new TowerEntity
                    {
                        Name = "Wood",
                        PlayerEntityId = this.Context.Players.FirstOrDefault().Id,
                        TowerType = TowerType.Wood,
                    },
                    new TowerEntity
                    {
                        Name = "Earth",
                        PlayerEntityId = this.Context.Players.FirstOrDefault().Id,
                        TowerType = TowerType.Earth,
                    },
                    new TowerEntity
                    {
                        Name = "Water",
                        PlayerEntityId = this.Context.Players.FirstOrDefault().Id,
                        TowerType = TowerType.Water,
                    },
                    new TowerEntity
                    {
                        Name = "Fire",
                        PlayerEntityId = this.Context.Players.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                        TowerType = TowerType.Fire,
                    },
                    new TowerEntity
                    {
                        Name = "Cold",
                        PlayerEntityId = this.Context.Players.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                        TowerType = TowerType.Cold,
                    },
                    new TowerEntity
                    {
                        Name = "Wind",
                        PlayerEntityId = this.Context.Players.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                        TowerType = TowerType.Wind,
                    },
                    new TowerEntity
                    {
                        Name = "Wood",
                        PlayerEntityId = this.Context.Players.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                        TowerType = TowerType.Wood,
                    },
                     new TowerEntity
                    {
                        Name = "Earth",
                        PlayerEntityId = this.Context.Players.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                        TowerType = TowerType.Earth,
                    },
                    new TowerEntity
                    {
                        Name = "Water",
                        PlayerEntityId = this.Context.Players.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                        TowerType = TowerType.Water,
                    }
                };

                await this.Context.Towers.AddRangeAsync(listTower);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}
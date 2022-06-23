namespace RandomTurret.BusinessObject
{
    using System.Collections.Generic;

    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class Tower : BaseBusinessObject<TowerEntity>
    {
        public Tower()
        {
            this.ValidationService = new TowerValidation();
        }

        public Tower(TowerEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new TowerValidation();
            }

            this.Id = entity.Id;
            this.Name = entity.Name;
            this.PlayerId = entity.PlayerEntityId;
            this.TowerType = entity.TowerType;
        }

        public Tower(TowerEntity entity, bool hasError, List<TowerStat> towerStats) : this(entity, hasError)
        {
            this.TowerStats = towerStats;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int PlayerId { get; set; }

        public List<TowerStat> TowerStats { get; set; }

        public TowerType TowerType { get; set; }

        public override TowerEntity CreateEntity()
        {
            return new TowerEntity
            {
                Id = this.Id,
                Name = this.Name,
                PlayerEntityId = this.PlayerId,
                TowerType = this.TowerType,
            };
        }
    }
}
namespace RandomTurret.BusinessObject
{
    using System.Collections.Generic;

    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class Gem : BaseBusinessObject<GemEntity>
    {
        public Gem()
        {
            this.ValidationService = new GemValidation();
        }

        public Gem(GemEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new GemValidation();
            }

            this.Id = entity.Id;
            this.RarityId = entity.RarityId;
            this.GemType = entity.GemType;
            this.ShootType = entity.ShootType;
            this.MergeType = entity.MergeType;
            this.ImpactType = entity.ImpactType;
        }

        public Gem(GemEntity entity, bool hasError, List<GemStat> gemStats) : this(entity, hasError)
        {
            this.GemStats = gemStats;
        }

        public List<GemStat> GemStats { get; set; }

        public GemType GemType { get; set; }

        public int Id { get; set; }

        public ImpactType? ImpactType { get; set; }

        public MergeType? MergeType { get; set; }

        public int RarityId { get; set; }

        public ShootType? ShootType { get; set; }

        public override GemEntity CreateEntity()
        {
            return new GemEntity
            {
                Id = this.Id,
                RarityId = this.RarityId,
                GemType = this.GemType,
                ShootType = this.ShootType,
                MergeType = this.MergeType,
                ImpactType = this.ImpactType,
            };
        }
    }
}
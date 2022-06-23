namespace RandomTurret.IDataAccess
{
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public interface ITemplateDataAccess : IBaseDataAccess<TemplateEntity>
    {
        Task<TemplateEntity> GetFromTemplateType(TemplateType templateType, int rarityId);
    }
}
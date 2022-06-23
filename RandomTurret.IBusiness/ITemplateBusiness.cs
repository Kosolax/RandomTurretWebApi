namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.Entities.Enum;

    public interface ITemplateBusiness : IBaseBusiness<Template>
    {
        Task<KeyValuePair<bool, Template>> CreateOrUpdate(Template itemToCreateOrUpdate);

        Task<Template> Delete(TemplateType templateType, RarityType rarityType);

        Task<Template> Get(TemplateType templateType, RarityType rarityType);

        Task<List<Template>> List();
    }
}
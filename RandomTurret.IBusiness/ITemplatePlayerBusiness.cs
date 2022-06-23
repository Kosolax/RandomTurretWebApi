namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;

    public interface ITemplatePlayerBusiness : IBaseBusiness<TemplatePlayer>
    {
        Task<KeyValuePair<bool, TemplatePlayer>> Create(TemplatePlayer itemToCreate);

        Task<TemplatePlayer> Delete(int id);
    }
}
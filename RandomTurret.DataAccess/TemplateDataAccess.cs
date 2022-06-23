namespace RandomTurret.DataAccess
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;
    using RandomTurret.IDataAccess;

    public class TemplateDataAccess : BaseDataAccess<TemplateEntity>, ITemplateDataAccess
    {
        public TemplateDataAccess(RandomTurretContext context) : base(context)
        {
        }

        public async Task<TemplateEntity> GetFromTemplateType(TemplateType templateType, int rarityId)
        {
            return await this.Context.Templates.Where(x => x.TemplateType == templateType && x.RarityId == rarityId).FirstOrDefaultAsync();
        }
    }
}
namespace RandomTurret.DataAccess
{
    using RandomTurret.Entities;
    using RandomTurret.IDataAccess;

    public class TemplatePlayerDataAccess : BaseDataAccess<TemplatePlayerEntity>, ITemplatePlayerDataAccess
    {
        public TemplatePlayerDataAccess(RandomTurretContext context) : base(context)
        {
        }
    }
}
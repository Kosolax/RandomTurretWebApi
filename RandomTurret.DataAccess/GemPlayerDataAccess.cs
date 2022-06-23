namespace RandomTurret.DataAccess
{
    using RandomTurret.Entities;
    using RandomTurret.IDataAccess;

    public class GemPlayerDataAccess : BaseDataAccess<GemPlayerEntity>, IGemPlayerDataAccess
    {
        public GemPlayerDataAccess(RandomTurretContext context) : base(context)
        {
        }
    }
}
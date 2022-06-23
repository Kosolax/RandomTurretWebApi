namespace RandomTurret.IDataAccess
{
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public interface IPlayerDataAccess : IBaseDataAccess<PlayerEntity>
    {
        Task<PlayerEntity> FindByMail(string mail);

        Task<PlayerEntity> FindByMailAndPassword(string mail, string password);
    }
}
namespace RandomTurret.DataAccess
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.IDataAccess;

    public class PlayerDataAccess : BaseDataAccess<PlayerEntity>, IPlayerDataAccess
    {
        public PlayerDataAccess(RandomTurretContext context)
            : base(context)
        {
        }

        public async Task<PlayerEntity> FindByMail(string mail)
        {
            return await this.Context.Players.Where(playerEntity => playerEntity.Mail == mail).FirstOrDefaultAsync();
        }

        public async Task<PlayerEntity> FindByMailAndPassword(string mail, string password)
        {
            return await this.Context.Players.Where(playerEntity => playerEntity.Mail == mail && playerEntity.Password == password).FirstOrDefaultAsync();
        }
    }
}
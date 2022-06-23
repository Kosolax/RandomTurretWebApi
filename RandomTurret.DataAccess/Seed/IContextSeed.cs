namespace RandomTurret.DataAccess.Seed
{
    using System.Threading.Tasks;

    public interface IContextSeed
    {
        RandomTurretContext Context { get; set; }

        Task Execute(bool isProduction);
    }
}
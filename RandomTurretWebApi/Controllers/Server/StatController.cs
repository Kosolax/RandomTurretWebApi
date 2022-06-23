namespace RandomTurret.WebApi.Controllers.Server
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.IBusiness;
    using RandomTurret.WebApi.Route.Server;

    [Route(StatRoute.RoutePrefix)]
    public class StatController : Controller
    {
        private readonly IStatBusiness statBusiness;

        public StatController(IStatBusiness statBusiness)
        {
            this.statBusiness = statBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                return this.Ok(await this.statBusiness.List());
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
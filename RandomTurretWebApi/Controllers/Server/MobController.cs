namespace RandomTurret.WebApi.Controllers.Server
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.IBusiness;
    using RandomTurret.WebApi.Route.Server;

    [Route(MobRoute.RoutePrefix)]
    public class MobController : Controller
    {
        private readonly IMobBusiness mobBusiness;

        public MobController(IMobBusiness mobBusiness)
        {
            this.mobBusiness = mobBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                return this.Ok(await this.mobBusiness.List());
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
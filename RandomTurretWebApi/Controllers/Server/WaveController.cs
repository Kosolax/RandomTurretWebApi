namespace RandomTurret.WebApi.Controllers.Server
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.IBusiness;
    using RandomTurret.WebApi.Route.Server;

    [Route(WaveRoute.RoutePrefix)]
    public class WaveController : Controller
    {
        private readonly IWaveBusiness waveBusiness;

        public WaveController(IWaveBusiness waveBusiness)
        {
            this.waveBusiness = waveBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                return this.Ok(await this.waveBusiness.List());
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
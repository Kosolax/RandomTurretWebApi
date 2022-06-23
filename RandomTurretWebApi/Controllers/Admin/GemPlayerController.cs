namespace RandomTurret.WebApi.Controllers.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using RandomTurret.BusinessObject;
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.IBusiness;
    using RandomTurret.WebApi.Route.Admin;

    [Route(GemPlayerRoute.RoutePrefix)]
    public class GemPlayerController : Controller
    {
        private readonly IGemPlayerBusiness gemPlayerBusiness;

        public GemPlayerController(IGemPlayerBusiness gemPlayerBusiness)
        {
            this.gemPlayerBusiness = gemPlayerBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GemPlayer gemToCreate)
        {
            try
            {
                KeyValuePair<bool, GemPlayer> gemPlayerResult = await this.gemPlayerBusiness.Create(gemToCreate);

                if (!gemPlayerResult.Key)
                {
                    if (gemPlayerResult.Value != null)
                    {
                        return this.BadRequest(gemPlayerResult.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(GemPlayerValidationResource.GemPlayer_Required_Create), GemPlayerValidationResource.GemPlayer_Required_Create }, });
                }

                return this.Ok(gemPlayerResult.Value);
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                GemPlayer gemPlayer = await this.gemPlayerBusiness.Delete(id);

                if (gemPlayer != null)
                {
                    return this.Ok(gemPlayer);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(GemPlayerValidationResource.GemPlayer_Required_Delete), GemPlayerValidationResource.GemPlayer_Required_Delete }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
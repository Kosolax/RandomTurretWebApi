namespace RandomTurret.WebApi.Controllers.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using RandomTurret.BusinessObject;
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.IBusiness;
    using RandomTurret.WebApi.Route.Admin;

    [Route(TemplatePlayerRoute.RoutePrefix)]
    public class TemplatePlayerController : Controller
    {
        private readonly ITemplatePlayerBusiness templatePlayerBusiness;

        public TemplatePlayerController(ITemplatePlayerBusiness templatePlayerBusiness)
        {
            this.templatePlayerBusiness = templatePlayerBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TemplatePlayer templateToCreate)
        {
            try
            {
                KeyValuePair<bool, TemplatePlayer> templatePlayerResult = await this.templatePlayerBusiness.Create(templateToCreate);

                if (!templatePlayerResult.Key)
                {
                    if (templatePlayerResult.Value != null)
                    {
                        return this.BadRequest(templatePlayerResult.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(TemplatePlayerValidationResource.TemplatePlayer_Required_Create), TemplatePlayerValidationResource.TemplatePlayer_Required_Create }, });
                }

                return this.Ok(templatePlayerResult.Value);
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
                TemplatePlayer templatePlayer = await this.templatePlayerBusiness.Delete(id);

                if (templatePlayer != null)
                {
                    return this.Ok(templatePlayer);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(TemplatePlayerValidationResource.TemplatePlayer_Required_Delete), TemplatePlayerValidationResource.TemplatePlayer_Required_Delete }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
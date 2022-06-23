namespace RandomTurret.WebApi.Controllers.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using RandomTurret.BusinessObject;
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.Entities.Enum;
    using RandomTurret.IBusiness;
    using RandomTurret.WebApi.Route.Admin;

    [Route(TemplateRoute.RoutePrefix)]
    public class TemplateController : Controller
    {
        private readonly ITemplateBusiness templateBusiness;

        public TemplateController(ITemplateBusiness templateBusiness)
        {
            this.templateBusiness = templateBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Template templateToCreate)
        {
            try
            {
                KeyValuePair<bool, Template> templateResult = await this.templateBusiness.CreateOrUpdate(templateToCreate);

                if (!templateResult.Key)
                {
                    if (templateResult.Value != null)
                    {
                        return this.BadRequest(templateResult.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(TemplateValidationResource.Template_Required_Create), TemplateValidationResource.Template_Required_Create }, });
                }

                return this.Ok(templateResult.Value);
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpDelete]
        [Route("{intTemplateType:int}/{intRarityType:int}")]
        public async Task<IActionResult> Delete(int intTemplateType, int intRarityType)
        {
            try
            {
                if (Enum.TryParse(intRarityType.ToString(), out RarityType rarityType) && Enum.IsDefined(typeof(RarityType), rarityType) &&
                    Enum.TryParse(intTemplateType.ToString(), out TemplateType templateType) && Enum.IsDefined(typeof(TemplateType), templateType))
                {
                    Template template = await this.templateBusiness.Delete(templateType, rarityType);

                    if (template != null)
                    {
                        return this.Ok(template);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(TemplateValidationResource.Template_Required_Delete), TemplateValidationResource.Template_Required_Delete }, });
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        [Route("{intTemplateType:int}/{intRarityType:int}")]
        public async Task<IActionResult> Get(int intTemplateType, int intRarityType)
        {
            try
            {
                if (Enum.TryParse(intRarityType.ToString(), out RarityType rarityType) && Enum.IsDefined(typeof(RarityType), rarityType) &&
                    Enum.TryParse(intTemplateType.ToString(), out TemplateType templateType) && Enum.IsDefined(typeof(TemplateType), templateType))
                {

                    Template template = await this.templateBusiness.Get(templateType, rarityType);

                    if (template != null)
                    {
                        return this.Ok(template);
                    }

                    return this.NotFound();
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });

            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                return this.Ok(await this.templateBusiness.List());
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Template templateToUpdate)
        {
            try
            {
                if (templateToUpdate != null)
                {
                    templateToUpdate.Id = id;
                    KeyValuePair<bool, Template> result = await this.templateBusiness.CreateOrUpdate(templateToUpdate);
                    if (!result.Key)
                    {
                        if (result.Value != null)
                        {
                            return this.BadRequest(result.Value.ValidationService.ModelState);
                        }

                        return this.BadRequest(new Dictionary<string, string> { { nameof(TemplateValidationResource.Template_Required_Update), TemplateValidationResource.Template_Required_Update }, });
                    }

                    return this.Ok(result.Value);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(TemplateValidationResource.Template_Required_Update), TemplateValidationResource.Template_Required_Update }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
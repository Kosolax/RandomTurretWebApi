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

    [Route(StatRoute.RoutePrefix)]
    public class StatController : Controller
    {
        private readonly IStatBusiness statBusiness;

        public StatController(IStatBusiness statBusiness)
        {
            this.statBusiness = statBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Stat statToCreate)
        {
            try
            {
                KeyValuePair<bool, Stat> result = await this.statBusiness.CreateOrUpdate(statToCreate);
                if (!result.Key)
                {
                    if (result.Value != null)
                    {
                        return this.BadRequest(result.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(StatValidationResource.Stat_Required_Create), StatValidationResource.Stat_Required_Create }, });
                }

                return this.Ok(result.Value);
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpDelete]
        [Route("{intStatType:int}")]
        public async Task<IActionResult> Delete(int intStatType)
        {
            try
            {
                if (Enum.TryParse(intStatType.ToString(), out StatType statType) && Enum.IsDefined(typeof(StatType), statType))
                {
                    Stat stat = await this.statBusiness.Delete(statType);

                    if (stat != null)
                    {
                        return this.Ok(stat);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(StatValidationResource.Stat_Required_Delete), StatValidationResource.Stat_Required_Delete }, });
                }
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }

            return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
        }

        [HttpGet]
        [Route("{intStatType:int}")]
        public async Task<IActionResult> Get(int intStatType)
        {
            try
            {
                if (Enum.TryParse(intStatType.ToString(), out StatType statType) && Enum.IsDefined(typeof(StatType), statType))
                {
                    Stat stat = await this.statBusiness.Get(statType);

                    if (stat != null)
                    {
                        return this.Ok(stat);
                    }

                    return this.NotFound();
                }
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }

            return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
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

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Stat statToUpdate)
        {
            try
            {
                if (statToUpdate != null)
                {
                    statToUpdate.Id = id;
                    KeyValuePair<bool, Stat> result = await this.statBusiness.CreateOrUpdate(statToUpdate);

                    if (!result.Key)
                    {
                        if (result.Value != null)
                        {
                            return this.BadRequest(result.Value.ValidationService.ModelState);
                        }

                        return this.BadRequest(new Dictionary<string, string> { { nameof(StatValidationResource.Stat_Required_Update), StatValidationResource.Stat_Required_Update }, });
                    }

                    return this.Ok(result.Value);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(StatValidationResource.Stat_Required_Update), StatValidationResource.Stat_Required_Update }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
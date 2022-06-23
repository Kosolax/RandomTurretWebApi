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

    [Route(MobRoute.RoutePrefix)]
    public class MobController : Controller
    {
        private readonly IMobBusiness mobBusiness;

        public MobController(IMobBusiness mobBusiness)
        {
            this.mobBusiness = mobBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Mob mobToCreate)
        {
            try
            {
                KeyValuePair<bool, Mob> mobResult = await this.mobBusiness.CreateOrUpdate(mobToCreate);

                if (!mobResult.Key)
                {
                    if (mobResult.Value != null)
                    {
                        return this.BadRequest(mobResult.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(MobValidationResource.Mob_Required_Create), MobValidationResource.Mob_Required_Create }, });
                }

                return this.Ok(mobResult.Value);
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpDelete]
        [Route("{intMobType:int}")]
        public async Task<IActionResult> Delete(int intMobType)
        {
            try
            {
                if (Enum.TryParse(intMobType.ToString(), out MobType mobType) && Enum.IsDefined(typeof(MobType), mobType))
                {
                    Mob mob = await this.mobBusiness.Delete(mobType);

                    if (mob != null)
                    {
                        return this.Ok(mob);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(MobValidationResource.Mob_Required_Delete), MobValidationResource.Mob_Required_Delete }, });
                }
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }

            return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
        }

        [HttpGet]
        [Route("{intMobType:int}")]
        public async Task<IActionResult> Get(int intMobType)
        {
            try
            {
                if (Enum.TryParse(intMobType.ToString(), out MobType mobType) && Enum.IsDefined(typeof(MobType), mobType))
                {
                    Mob mob = await this.mobBusiness.Get(mobType);

                    if (mob != null)
                    {
                        return this.Ok(mob);
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
                return this.Ok(await this.mobBusiness.List());
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Mob mobToUpdate)
        {
            try
            {
                if (mobToUpdate != null)
                {
                    mobToUpdate.Id = id;
                    KeyValuePair<bool, Mob> result = await this.mobBusiness.CreateOrUpdate(mobToUpdate);
                    if (!result.Key)
                    {
                        if (result.Value != null)
                        {
                            return this.BadRequest(result.Value.ValidationService.ModelState);
                        }

                        return this.BadRequest(new Dictionary<string, string> { { nameof(MobValidationResource.Mob_Required_Update), MobValidationResource.Mob_Required_Update }, });
                    }

                    return this.Ok(result.Value);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(MobValidationResource.Mob_Required_Update), MobValidationResource.Mob_Required_Update }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
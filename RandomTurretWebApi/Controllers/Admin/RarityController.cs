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

    [Route(RarityRoute.RoutePrefix)]
    public class RarityController : Controller
    {
        private readonly IRarityBusiness rarityBusiness;

        public RarityController(IRarityBusiness rarityBusiness)
        {
            this.rarityBusiness = rarityBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Rarity rarityToCreate)
        {
            try
            {
                KeyValuePair<bool, Rarity> result = await this.rarityBusiness.CreateOrUpdate(rarityToCreate);
                if (!result.Key)
                {
                    if (result.Value != null)
                    {
                        return this.BadRequest(result.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(RarityValidationResource.Rarity_Required_Create), RarityValidationResource.Rarity_Required_Create }, });
                }

                return this.Ok(result.Value);
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpDelete]
        [Route("{intRarityType:int}")]
        public async Task<IActionResult> Delete(int intRarityType)
        {
            try
            {
                if (Enum.TryParse(intRarityType.ToString(), out RarityType rarityType) && Enum.IsDefined(typeof(RarityType), rarityType))
                {
                    Rarity rarity = await this.rarityBusiness.Delete(rarityType);

                    if (rarity != null)
                    {
                        return this.Ok(rarity);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(RarityValidationResource.Rarity_Required_Delete), RarityValidationResource.Rarity_Required_Delete }, });

                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured }, });

            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        [Route("{intRarityType:int}")]
        public async Task<IActionResult> Get(int intRarityType)
        {
            try
            {
                if (Enum.TryParse(intRarityType.ToString(), out RarityType rarityType) && Enum.IsDefined(typeof(RarityType), rarityType))
                {
                    Rarity rarity = await this.rarityBusiness.Get(rarityType);

                    if (rarity != null)
                    {
                        return this.Ok(rarity);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured }, });
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured }, });
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
                return this.Ok(await this.rarityBusiness.List());
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rarity rarirtyToUpdate)
        {
            try
            {
                if (rarirtyToUpdate != null)
                {
                    rarirtyToUpdate.Id = id;
                    KeyValuePair<bool, Rarity> result = await this.rarityBusiness.CreateOrUpdate(rarirtyToUpdate);

                    if (!result.Key)
                    {
                        if (result.Value != null)
                        {
                            return this.BadRequest(result.Value.ValidationService.ModelState);
                        }

                        return this.BadRequest(new Dictionary<string, string> { { nameof(RarityValidationResource.Rarity_Required_Update), RarityValidationResource.Rarity_Required_Update }, });
                    }

                    return this.Ok(result.Value);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(RarityValidationResource.Rarity_Required_Update), RarityValidationResource.Rarity_Required_Update }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
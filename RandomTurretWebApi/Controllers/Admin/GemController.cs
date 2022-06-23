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

    [Route(GemRoute.RoutePrefix)]
    public class GemController : Controller
    {
        private readonly IGemBusiness gemBusiness;

        public GemController(IGemBusiness gemBusiness)
        {
            this.gemBusiness = gemBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Gem gemToCreate)
        {
            try
            {
                KeyValuePair<bool, Gem> gemResult = await this.gemBusiness.CreateOrUpdate(gemToCreate);

                if (!gemResult.Key)
                {
                    if (gemResult.Value != null)
                    {
                        return this.BadRequest(gemResult.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(GemValidationResource.Gem_Required_Create), GemValidationResource.Gem_Required_Create }, });
                }

                return this.Ok(gemResult.Value);
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpDelete]
        [Route("{intGemType:int}/{intRarityType:int}")]
        public async Task<IActionResult> Delete(int intGemType, int intRarityType)
        {
            try
            {
                if (Enum.TryParse(intRarityType.ToString(), out RarityType rarityType) && Enum.IsDefined(typeof(RarityType), rarityType) &&
                    Enum.TryParse(intGemType.ToString(), out GemType gemType) && Enum.IsDefined(typeof(GemType), gemType))
                {
                    Gem gem = await this.gemBusiness.Delete(gemType, rarityType);

                    if (gem != null)
                    {
                        return this.Ok(gem);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(GemValidationResource.Gem_Required_Delete), GemValidationResource.Gem_Required_Delete }, });
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        [Route("{intGemType:int}/{intRarityType:int}")]
        public async Task<IActionResult> Get(int intGemType, int intRarityType)
        {
            try
            {
                if (Enum.TryParse(intRarityType.ToString(), out RarityType rarityType) && Enum.IsDefined(typeof(RarityType), rarityType) &&
                    Enum.TryParse(intGemType.ToString(), out GemType gemType) && Enum.IsDefined(typeof(GemType), gemType))
                {

                    Gem gem = await this.gemBusiness.Get(gemType, rarityType);

                    if (gem != null)
                    {
                        return this.Ok(gem);
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
                return this.Ok(await this.gemBusiness.List());
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        [Route("{intImpactType:int}" + GemRoute.ImpactType)]
        public async Task<IActionResult> ListFromImpactType(int intImpactType)
        {
            try
            {
                if (Enum.TryParse(intImpactType.ToString(), out ImpactType impactType) && Enum.IsDefined(typeof(ImpactType), impactType))
                {
                    return this.Ok(await this.gemBusiness.ListFromImpactType(impactType));
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        [Route("{intMergeType:int}" + GemRoute.MergeType)]
        public async Task<IActionResult> ListFromMergeType(int intMergeType)
        {
            try
            {
                if (Enum.TryParse(intMergeType.ToString(), out MergeType mergeType) && Enum.IsDefined(typeof(MergeType), mergeType))
                {
                    return this.Ok(await this.gemBusiness.ListFromMergeType(mergeType));
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        [Route("{intShootType:int}" + GemRoute.ShootType)]
        public async Task<IActionResult> ListFromShootType(int intShootType)
        {
            try
            {
                if (Enum.TryParse(intShootType.ToString(), out ShootType shootType) && Enum.IsDefined(typeof(ShootType), shootType))
                {
                    return this.Ok(await this.gemBusiness.ListFromShootType(shootType));
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Gem gemToUpdate)
        {
            try
            {
                if (gemToUpdate != null)
                {
                    gemToUpdate.Id = id;
                    KeyValuePair<bool, Gem> result = await this.gemBusiness.CreateOrUpdate(gemToUpdate);
                    if (!result.Key)
                    {
                        if (result.Value != null)
                        {
                            return this.BadRequest(result.Value.ValidationService.ModelState);
                        }

                        return this.BadRequest(new Dictionary<string, string> { { nameof(GemValidationResource.Gem_Required_Update), GemValidationResource.Gem_Required_Update }, });
                    }

                    return this.Ok(result.Value);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(GemValidationResource.Gem_Required_Update), GemValidationResource.Gem_Required_Update }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
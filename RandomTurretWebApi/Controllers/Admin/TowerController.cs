namespace RandomTurret.WebApi.Controllers.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using RandomTurret.BusinessObject;
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.IBusiness;
    using RandomTurret.WebApi.Route.Admin;

    [Route(TowerRoute.RoutePrefix)]
    public class TowerController : Controller
    {
        private readonly ITowerBusiness towerBusiness;

        public TowerController(ITowerBusiness towerBusiness)
        {
            this.towerBusiness = towerBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tower towerToCreate)
        {
            try
            {
                KeyValuePair<bool, Tower> towerResult = await this.towerBusiness.CreateOrUpdate(towerToCreate);

                if (!towerResult.Key)
                {
                    if (towerResult.Value != null)
                    {
                        return this.BadRequest(towerResult.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(TowerValidationResource.Tower_Required_Create), TowerValidationResource.Tower_Required_Create }, });
                }

                return this.Ok(towerResult.Value);
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
                Tower tower = await this.towerBusiness.Delete(id);

                if (tower != null)
                {
                    return this.Ok(tower);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(TowerValidationResource.Tower_Required_Delete), TowerValidationResource.Tower_Required_Delete }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Tower tower = await this.towerBusiness.Get(id);

                if (tower != null)
                {
                    return this.Ok(tower);
                }

                return this.NotFound();
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tower towerToUpdate)
        {
            try
            {
                if (towerToUpdate != null)
                {
                    towerToUpdate.Id = id;
                    KeyValuePair<bool, Tower> result = await this.towerBusiness.CreateOrUpdate(towerToUpdate);
                    if (!result.Key)
                    {
                        if (result.Value != null)
                        {
                            return this.BadRequest(result.Value.ValidationService.ModelState);
                        }

                        return this.BadRequest(new Dictionary<string, string> { { nameof(TowerValidationResource.Tower_Required_Update), TowerValidationResource.Tower_Required_Update }, });
                    }

                    return this.Ok(result.Value);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(TowerValidationResource.Tower_Required_Update), TowerValidationResource.Tower_Required_Update }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
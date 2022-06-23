namespace RandomTurret.WebApi.Controllers.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using RandomTurret.BusinessObject;
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.IBusiness;
    using RandomTurret.WebApi.Route.Admin;

    [Route(WaveRoute.RoutePrefix)]
    public class WaveController : Controller
    {
        private readonly IWaveBusiness waveBusiness;

        public WaveController(IWaveBusiness waveBusiness)
        {
            this.waveBusiness = waveBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Wave waveToCreate)
        {
            try
            {
                KeyValuePair<bool, Wave> waveResult = await this.waveBusiness.CreateOrUpdate(waveToCreate);

                if (!waveResult.Key)
                {
                    if (waveResult.Value != null)
                    {
                        return this.BadRequest(waveResult.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(WaveValidationResource.Wave_Required_Create), WaveValidationResource.Wave_Required_Create }, });
                }

                return this.Ok(waveResult.Value);
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpDelete]
        [Route("{waveNumber:int}")]
        public async Task<IActionResult> Delete(int waveNumber)
        {
            try
            {
                Wave wave = await this.waveBusiness.Delete(waveNumber);

                if (wave != null)
                {
                    return this.Ok(wave);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(WaveValidationResource.Wave_Required_Delete), WaveValidationResource.Wave_Required_Delete }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        [Route("{waveNumber:int}")]
        public async Task<IActionResult> Get(int waveNumber)
        {
            try
            {
                Wave wave = await this.waveBusiness.GetFromWaveNumber(waveNumber);

                if (wave != null)
                {
                    return this.Ok(wave);
                }

                return this.NotFound();
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
                return this.Ok(await this.waveBusiness.List());
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Wave waveToUpdate)
        {
            try
            {
                if (waveToUpdate != null)
                {
                    waveToUpdate.Id = id;
                    KeyValuePair<bool, Wave> result = await this.waveBusiness.CreateOrUpdate(waveToUpdate);
                    if (!result.Key)
                    {
                        if (result.Value != null)
                        {
                            return this.BadRequest(result.Value.ValidationService.ModelState);
                        }

                        return this.BadRequest(new Dictionary<string, string> { { nameof(WaveValidationResource.Wave_Required_Update), WaveValidationResource.Wave_Required_Update }, });
                    }

                    return this.Ok(result.Value);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(WaveValidationResource.Wave_Required_Update), WaveValidationResource.Wave_Required_Update }, });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
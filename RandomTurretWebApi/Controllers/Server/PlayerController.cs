namespace RandomTurret.WebApi.Controllers.Server
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using RandomTurret.BusinessObject;
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.IBusiness;
    using RandomTurret.WebApi.Route.Server;

    [Route(PlayerRoute.RoutePrefix)]
    public class PlayerController : Controller
    {
        private readonly IPlayerBusiness playerBusiness;

        public PlayerController(IPlayerBusiness playerBusiness)
        {
            this.playerBusiness = playerBusiness;
        }

        [HttpPost]
        [Route(PlayerRoute.Connection)]
        public async Task<IActionResult> Connection([FromBody] Player player)
        {
            try
            {
                return this.Ok(await this.playerBusiness.Connection(player));
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Player playerToCreate)
        {
            try
            {
                KeyValuePair<bool, Player> result = await this.playerBusiness.CreateOrUpdate(playerToCreate);
                if (!result.Key)
                {
                    if (result.Value != null)
                    {
                        return this.BadRequest(result.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { { nameof(PlayerValidationResource.Player_Required_Create), PlayerValidationResource.Player_Required_Create }, });
                }

                return this.Ok(result.Value);
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }

        [HttpPut]
        [Route(PlayerRoute.UpdateElo + "/{id:int}")]
        public async Task<IActionResult> UpdateElo(int id, [FromBody] Player player)
        {
            try
            {
                if (player != null)
                {
                    player.Id = id;
                    KeyValuePair<bool, Player> result = await this.playerBusiness.UpdateElo(player);
                    if (!result.Key)
                    {
                        if (result.Value != null)
                        {
                            return this.BadRequest(result.Value.ValidationService.ModelState);
                        }

                        return this.BadRequest(new Dictionary<string, string> { { nameof(PlayerValidationResource.Player_Required_Update), PlayerValidationResource.Player_Required_Update }, });
                    }

                    return this.Ok(result.Value);
                }

                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerResource.An_Error_Has_Occured), ServerResource.An_Error_Has_Occured } });
            }
        }
    }
}
namespace RandomTurret.Business
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.Entities;
    using RandomTurret.IBusiness;
    using RandomTurret.IDataAccess;

    public class PlayerBusiness : IPlayerBusiness
    {
        private readonly IPlayerDataAccess dataAccess;

        private readonly ITowerBusiness towerBusiness;

        public PlayerBusiness(IPlayerDataAccess playerDataAccess, ITowerBusiness towerBusiness)
        {
            this.dataAccess = playerDataAccess;
            this.towerBusiness = towerBusiness;
        }

        public async Task<Player> Connection(Player player)
        {
            if (player != null)
            {
                PlayerEntity entity = await this.dataAccess.FindByMailAndPassword(player.Mail, player.Password);

                if (entity != null)
                {
                    return await this.GetPlayerFromEntityAsync(entity);
                }
            }

            return null;
        }

        public async Task<KeyValuePair<bool, Player>> CreateOrUpdate(Player playerToCreateOrUpdate)
        {
            if (playerToCreateOrUpdate != null)
            {
                PlayerEntity entity = playerToCreateOrUpdate.CreateEntity();

                if (playerToCreateOrUpdate.ValidationService.Validate(entity))
                {
                    PlayerEntity playerWithMail = await this.dataAccess.FindByMail(entity.Mail);

                    if (entity.Id == default)
                    {
                        if (playerWithMail == null)
                        {
                            entity.Elo = 1000;
                            entity = await this.dataAccess.Create(entity);
                            playerToCreateOrUpdate = await this.GetPlayerFromEntityAsync(entity);

                            return new KeyValuePair<bool, Player>(true, playerToCreateOrUpdate);
                        }
                        else
                        {
                            if (playerWithMail != null)
                            {
                                playerToCreateOrUpdate.ValidationService.AddError(nameof(PlayerValidationResource.Player_Mail_Unique), PlayerValidationResource.Player_Mail_Unique);
                            }
                        }
                    }
                    else
                    {
                        if (playerWithMail == null || (playerWithMail != null && playerWithMail.Id == entity.Id))
                        {
                            if (entity.Elo < 0)
                            {
                                entity.Elo = 0;
                            }

                            entity = await this.dataAccess.Update(entity, entity.Id);
                            playerToCreateOrUpdate = await this.GetPlayerFromEntityAsync(entity);

                            return new KeyValuePair<bool, Player>(true, playerToCreateOrUpdate);
                        }
                        else
                        {
                            if (playerWithMail != null)
                            {
                                playerToCreateOrUpdate.ValidationService.AddError(nameof(PlayerValidationResource.Player_Mail_Unique), PlayerValidationResource.Player_Mail_Unique);
                            }
                        }
                    }
                }
            }

            return new KeyValuePair<bool, Player>(false, playerToCreateOrUpdate);
        }

        public async Task<Player> Delete(int id)
        {
            Player player = await this.Get(id);

            if (player != null)
            {
                await this.dataAccess.Delete(id);
                return player;
            }

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<Player> Get(int id)
        {
            Player player = null;
            PlayerEntity entity = await this.dataAccess.Find(id);

            if (entity != null)
            {
                player = await this.GetPlayerFromEntityAsync(entity);
            }

            return player;
        }

        public async Task<KeyValuePair<bool, Player>> UpdateElo(Player player)
        {
            if (player != null)
            {
                PlayerEntity entity = player.CreateEntity();
                if (player.ValidationService.Validate(entity))
                {
                    if (entity.Elo < 0)
                    {
                        entity.Elo = 0;
                    }

                    PlayerEntity playerInDb = await this.dataAccess.Find(entity.Id);
                    playerInDb.Elo = entity.Elo;

                    playerInDb = await this.dataAccess.Update(entity, entity.Id);
                    player = await this.GetPlayerFromEntityAsync(playerInDb);

                    return new KeyValuePair<bool, Player>(true, player);
                }
            }

            return new KeyValuePair<bool, Player>(false, player);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
                this.towerBusiness?.Dispose();
            }
        }

        private async Task<Player> GetPlayerFromEntityAsync(PlayerEntity entity)
        {
            Player player = new Player(entity, false);
            player.Towers = await this.towerBusiness.ListByPlayerId(player.Id);

            return player;
        }
    }
}
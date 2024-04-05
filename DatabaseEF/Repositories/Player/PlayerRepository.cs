
using EntityFramework.Entities.Player;
using EntityFramework.Repositories.ValidadeData;
using EntityFramework.Repositories.ValidateData;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Repositories.Player
{
    public class PlayerRepository : RepositoryBase<PlayerEntity>
    {
        private readonly MeuDbContext _dbContext;

        public override object GetPrimaryKey(PlayerEntity entity) => entity.Id;

        public PlayerRepository(MeuDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<PlayerEntity> GetPlayerByAccountId(int accountId)
        //{
        //    return await _dbContext.PlayerEntities.FirstOrDefaultAsync(p => p.Id == accountId);
        //}

        private async Task<bool> HasPlayerName(string nome)
        {
            // Verificar se o nome já existe na tabela de jogadores
            return await _dbContext.PlayerEntities.AnyAsync(p => p.Name.ToLower() == nome.ToLower());
        }

        private async Task<bool> HasPlayerSlot(int playerId)
        {
            // Buscar o jogador pelo ID e verificar se o nome dele é diferente de 'string.Empty'
            return await _dbContext.PlayerEntities.AnyAsync(p => p.Id == playerId && p.Name != string.Empty);
        }

        private async Task<PlayerEntity> GivePlayerByAccountId(int accountId)
        {
            return await _dbContext.PlayerEntities.FirstOrDefaultAsync(p => p.Id == accountId);
        }

        public async Task<OperationResult> AddNewPlayerAsync(string charName, int accountId)
        {
            var operationResult = new OperationResult();

            var validateName = InputValidator.IsValidName(charName);

            if (!validateName.Success)
            {
                operationResult.Success = false;
                operationResult.Message = validateName.Message;
                return operationResult;
            }

            // Verifique se o nome que o jogador escolheu já existe no banco de dados
            if (await HasPlayerName(charName))
            {
                operationResult.Success = false;
                operationResult.Message = $"Jogador com nome {charName} já existe.";
                return operationResult;
            }

            // Verificar se o jogador não está tentando criar um char onde já tem um.
            if (await HasPlayerSlot(accountId))
            {
                operationResult.Success = false;
                operationResult.Message = $"O Slot {accountId} já possui um personagem.";
                return operationResult;
            }

            var result = GivePlayerByAccountId(accountId);

            if (result == null)
            {
                operationResult.Success = false;
                operationResult.Message = "Erro ao obter o jogador.";
                return operationResult;
            }

            if (result.Result == null)
            {
                operationResult.Success = false;
                operationResult.Message = "Jogador não encontrado.";
                return operationResult;
            }

            result.Result.Name = charName;

            var refreshDb = await UpdateAsync();
            if (!refreshDb)
            {
                operationResult.Success = false;
                operationResult.Message = "Erro ao atualizar o banco de dados.";
            }

            return operationResult;
        }
    }
    // Adicione métodos específicos, se necessário
}

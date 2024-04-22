using EntityFramework.Entities.Interface;

namespace EntityFramework.Repositories.ValidadeData
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public IAccountEntity EntityType { get; set; }
    }
}

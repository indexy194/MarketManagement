namespace UseCases
{
    public interface IRecordTransactionUseCase
    {
        void Execute(string cashierName, string userId, int productId, int qty);
    }
}
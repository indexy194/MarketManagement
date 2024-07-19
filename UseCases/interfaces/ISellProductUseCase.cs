namespace UseCases
{
    public interface ISellProductUseCase
    {
        void Execute(string cashierName, string userId, int productId, int qtyToSell);
    }
}
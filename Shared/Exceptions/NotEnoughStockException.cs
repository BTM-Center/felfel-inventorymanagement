namespace Shared.Exceptions
{
    public class NotEnoughStockException : Exception
    {
        public NotEnoughStockException(string message) : base(message)
        {
        }
    }
}

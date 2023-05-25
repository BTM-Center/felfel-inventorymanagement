namespace Shared.Constants
{
    public static partial class Constants
    {
        public static class ApiRoutes
        {
            public const string ControllerName = "[controller]";

            public const string CreateCustomerDelivery = "CreateCustomerDelivery";

            public const string GetProductBatch = "GetProductBatch/{id}";
            public const string GetBatchesForProduct = "GetBatchesForProduct/{productId}";
            public const string GetAvailableProductBatchesWithFreshnessStatus = "GetAvailableProductBatchesWithFreshnessStatus";
            public const string GetProductBatchHistory = "GetProductBatchHistory/{id}";
            public const string UpdateProductBatch = "UpdateProductBatch";
            
            public const string GetOrder = "GetOrder/{id}";
            public const string CreateOrder = "CreateOrder";
        }
    }
}

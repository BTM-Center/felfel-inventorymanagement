using Dapper;
using InventoryManagement.Data.DataAccess.Interfaces;
using InventoryManagement.Data.Entities;
using InventoryManagement.Data.SqlClient.Interfaces;
using Shared.Enums;

namespace InventoryManagement.Data.DataAccess
{
    internal class ProductDataAccess : IProductDataAccess
    {
        private readonly ISqlClient _sqlClient;

        public ProductDataAccess(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public async Task<ProductBatch> GetProductBatchAsync(int id)
        {
            const string sql = @"
                SELECT PB.Id                AS 'Id',
                       PB.CheckInDate       AS 'CheckInDate',
                       PB.Units             AS 'Units',
                       ''                   AS 'Split1',
                       P.Id                 AS 'Id',
                       P.Name               AS 'Name',
                       P.Description        AS 'Description',
                       P.ExpiryDays         AS 'ExpiryDays',
                       ''                   AS 'Split2',
                       S.Id                 AS 'Id',
                       S.Name               AS 'Name',
                       S.Address            AS 'Address'
                FROM ProductBatch PB
                    INNER JOIN Product P
                        ON PB.ProductId = P.Id
                    INNER JOIN Supplier S
                        ON P.SupplierId = S.Id
                WHERE PB.Id = @id
            ";

            var parameters = new { id };

            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                var productBatchResult = await dbConnection.QueryAsync<ProductBatch, Product, Supplier, ProductBatch>(sql,
                    (productBatch, product, supplier) =>
                    {
                        productBatch.Product = product;
                        productBatch.Product.Supplier = supplier;

                        return productBatch;
                    }, parameters, splitOn: "Split1, Split2");


                return productBatchResult.FirstOrDefault();
            }
        }

        public async Task<IList<ProductBatch>> GetProductBatchesForProductAsync(int productId)
        {
            const string sql = @"
                SELECT PB.Id                AS 'Id',
                       PB.CheckInDate       AS 'CheckInDate',
                       PB.Units             AS 'Units',
                       ''                   AS 'Split1',
                       P.Id                 AS 'Id',
                       P.Name               AS 'Name',
                       P.Description        AS 'Description',
                       P.ExpiryDays         AS 'ExpiryDays',
                       ''                   AS 'Split2',
                       S.Id                 AS 'Id',
                       S.Name               AS 'Name',
                       S.Address            AS 'Address'
                FROM ProductBatch PB
                    INNER JOIN Product P
                        ON PB.ProductId = P.Id
                    INNER JOIN Supplier S
                        ON P.SupplierId = S.Id
                WHERE P.Id = @productId
                AND PB.Units > 0
            ";

            var parameters = new { productId };

            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                var productBatchResult = await dbConnection.QueryAsync<ProductBatch, Product, Supplier, ProductBatch>(sql,
                    (productBatch, product, supplier) =>
                    {
                        productBatch.Product = product;
                        productBatch.Product.Supplier = supplier;

                        return productBatch;
                    }, parameters, splitOn: "Split1, Split2");


                return productBatchResult.ToList();
            }
        }

        public async Task<IList<(ProductBatch, ProductBatchFreshnessStatus)>> GetAvailableProductBatchesWithFreshnessStatus()
        {
            const string sql = @"
                DECLARE @todaysDate DATE = CAST(SYSDATETIME() AS DATE)

                SELECT PB.Id                AS 'Id',
                       PB.CheckInDate       AS 'CheckInDate',
                       PB.Units             AS 'Units',
                       ''                   AS 'Split1',
                       P.Id                 AS 'Id',
                       P.Name               AS 'Name',
                       P.Description        AS 'Description',
                       P.ExpiryDays         AS 'ExpiryDays',
                       ''                   AS 'Split2',
                       S.Id                 AS 'Id',
                       S.Name               AS 'Name',
                       S.Address            AS 'Address',
                       ''                   AS 'Split3',
                       CASE 
                            WHEN (@todaysDate < (CAST(DATEADD(DAY, P.ExpiryDays, PB.CheckInDate) AS DATE))) THEN 0
                            WHEN (@todaysDate = (CAST(DATEADD(DAY, P.ExpiryDays, PB.CheckInDate) AS DATE))) THEN 1
                            ELSE 2
                       END                  AS 'ProductBatchFreshnessStatus'
                FROM ProductBatch PB
                    INNER JOIN Product P
                        ON PB.ProductId = P.Id
                    INNER JOIN Supplier S
                        ON P.SupplierId = S.Id
                WHERE PB.Units > 0
                AND CheckInDate IS NOT NULL
                ORDER BY ProductBatchFreshnessStatus DESC
            ";

            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                var productBatchResult = await dbConnection.QueryAsync<ProductBatch, Product, Supplier, ProductBatchFreshnessStatus, 
                                                                       (ProductBatch, ProductBatchFreshnessStatus)> (sql,
                    (productBatch, product, supplier, status) =>
                    {
                        productBatch.Product = product;
                        productBatch.Product.Supplier = supplier;

                        return (productBatch, status);
                    }, splitOn: "Split1, Split2, ProductBatchFreshnessStatus");


                return productBatchResult.ToList();
            }
        }

        public async Task<int> CreateProductBatchAsync(ProductBatch productBatch)
        {
            const string sql = @"
                INSERT INTO ProductBatch (ProductId, CheckInDate, Units)
                VALUES (@ProductId, @CheckInDate, @Units)

                SELECT SCOPE_IDENTITY()  
            ";

            var parameters = new
            {
                ProductId = productBatch.Product.Id,
                productBatch.CheckInDate,
                productBatch.Units
            };


            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                return await dbConnection.ExecuteScalarAsync<int>(sql, parameters);
            }
        }

        public async Task<bool> UpdateProductBatchAsync(ProductBatch productBatch)
        {
            const string sql = @"
                UPDATE ProductBatch
                SET CheckInDate = @CheckInDate,
                    Units = @Units
                WHERE Id = @Id
            ";

            var parameters = new
            {
                productBatch.CheckInDate,
                productBatch.Units,
                productBatch.Id
            };

            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                return await dbConnection.ExecuteAsync(sql, parameters) != 0;
            }
        }
    }
}

using Dapper;
using InventoryManagement.Data.DataAccess.Interfaces;
using InventoryManagement.Data.Entities;
using InventoryManagement.Data.SqlClient.Interfaces;

namespace InventoryManagement.Data.DataAccess
{
    internal class CustomerDataAccess : ICustomerDataAccess
    {
        private readonly ISqlClient _sqlClient;

        public CustomerDataAccess(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public async Task<IList<CustomerDelivery>> GetCustomerDeliveriesForProductBatchAsync(int productBatchId)
        {
            const string sql = @"
                SELECT CD.Id                AS 'Id',
                       CD.DeliveryDate      AS 'DeliveryDate',
                       CD.Units             AS 'Units',
                       ''                   AS 'Split1',
                       C.Id                 AS 'Id',
                       C.Name               AS 'Name',
                       C.Address            AS 'Address',
                       ''                   AS 'Split2',
                       PB.Id                AS 'Id',
                       PB.CheckInDate       AS 'CheckInDate',
                       PB.Units             AS 'Units',
                       ''                   AS 'Split3',
                       P.Id                 AS 'Id',
                       P.Name               AS 'Name',
                       P.Description        AS 'Description',
                       P.ExpiryDays         AS 'ExpiryDays',
                       ''                   AS 'Split4',
                       S.Id                 AS 'Id',
                       S.Name               AS 'Name',
                       S.Address            AS 'Address'
                FROM CustomerDelivery CD
                    INNER JOIN Customer C
                        ON CD.customerId = C.Id
                    INNER JOIN ProductBatch PB
                        ON CD.ProductBatchId = PB.Id
                    INNER JOIN Product P
                        ON PB.ProductId = P.Id
                    INNER JOIN Supplier S
                        ON P.SupplierId = S.Id
                WHERE PB.Id = @productBatchId
            ";

            var parameters = new { productBatchId };

            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                var customerDeliveryResult = await dbConnection.QueryAsync<CustomerDelivery, Customer, ProductBatch, Product, Supplier, CustomerDelivery>(sql,
                    (customerDelivery, customer, productBatch, product, supplier) =>
                    {
                        customerDelivery.Customer = customer;
                        customerDelivery.ProductBatch = productBatch;
                        customerDelivery.ProductBatch.Product = product;
                        customerDelivery.ProductBatch.Product.Supplier = supplier;

                        return customerDelivery;
                    }, parameters, splitOn: "Split1, Split2, Split3, Split4");

                return customerDeliveryResult.ToList();
            }
        }

        public async Task<bool> CreateCustomerDeliveryAsync(CustomerDelivery customerDelivery)
        {
            const string sql = @"
                INSERT INTO CustomerDelivery (CustomerId, ProductBatchId, DeliveryDate, Units)
                VALUES (@CustomerId, @ProductBatchId, @DeliveryDate, @Units)
            ";

            var parameters = new
            {
                CustomerId = customerDelivery.Customer.Id,
                ProductBatchId = customerDelivery.ProductBatch.Id,
                customerDelivery.DeliveryDate,
                customerDelivery.Units
            };

            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                return await dbConnection.ExecuteAsync(sql, parameters) != 0;
            }
        }
    }
}

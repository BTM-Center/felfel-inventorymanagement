using Dapper;
using InventoryManagement.Data.DataAccess.Interfaces;
using InventoryManagement.Data.Entities;
using InventoryManagement.Data.SqlClient.Interfaces;

namespace InventoryManagement.Data.DataAccess
{
    internal class SupplierDataAccess : ISupplierDataAccess
    {
        private readonly ISqlClient _sqlClient;

        public SupplierDataAccess(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            const string sql = @"
                SELECT O.Id                 AS 'Id',
                       O.CreatedDateTime    AS 'CreatedDateTime',
                       ''                   AS 'Split1',
                       S.Id                 AS 'Id',
                       S.Name               AS 'Name',
                       S.Address            AS 'Address'
                FROM [Order] O
                    INNER JOIN Supplier S
                        ON O.SupplierId = S.Id
                WHERE O.Id = @id

                SELECT OL.Id                AS 'Id',
                       OL.DeliveryDate      AS 'DeliveryDate',
                       OL.Units             AS 'Units',
                       ''                   AS 'Split1',
                       P.Id                 AS 'Id',
                       P.Name               AS 'Name',
                       P.Description        AS 'Description',
                       P.ExpiryDays         AS 'ExpiryDays'
                FROM OrderLines OL
                    INNER JOIN Product P
                        ON OL.ProductId = P.Id
                WHERE OL.OrderId = @id
            ";

            var parameters = new { id };

            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                var gridReader = await dbConnection.QueryMultipleAsync(sql, parameters);
                
                var orderResult = gridReader.Read<Order, Supplier, Order>(
                    (order, supplier) =>
                    {
                        order.Supplier = supplier;
                        return order;
                    }, splitOn: "Split1").FirstOrDefault();

                var orderLinesResult = gridReader.Read<OrderLine, Product, OrderLine>(
                    (orderLine, product) =>
                    {
                        orderLine.Product = product;
                        return orderLine;
                    }, splitOn: "Split1");

                orderResult.OrderLines = orderLinesResult.ToList();
                return orderResult;
            }
        }

        public async Task<Order> GetOrderForProductBatchAsync(int productBatchId)
        {
            const string sql = @"
                DECLARE @orderId INT
        
                SELECT @orderId = OL.OrderId
                FROM OrderLines OL   
                WHERE OL.ProductBatchId = @productBatchId

                SELECT O.Id                 AS 'Id',
                       O.CreatedDateTime    AS 'CreatedDateTime',
                       ''                   AS 'Split1',
                       S.Id                 AS 'Id',
                       S.Name               AS 'Name',
                       S.Address            AS 'Address'
                FROM [Order] O
                    INNER JOIN Supplier S
                        ON O.SupplierId = S.Id
                WHERE O.Id = @orderId

                SELECT OL.Id                AS 'Id',
                       OL.DeliveryDate      AS 'DeliveryDate',
                       OL.Units             AS 'Units',
                       ''                   AS 'Split1',
                       P.Id                 AS 'Id',
                       P.Name               AS 'Name',
                       P.Description        AS 'Description',
                       P.ExpiryDays         AS 'ExpiryDays'
                FROM OrderLines OL
                    INNER JOIN Product P
                        ON OL.ProductId = P.Id
                WHERE OL.OrderId = @orderId
            ";

            var parameters = new { productBatchId };

            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                var gridReader = await dbConnection.QueryMultipleAsync(sql, parameters);

                var orderResult = gridReader.Read<Order, Supplier, Order>(
                    (order, supplier) =>
                    {
                        order.Supplier = supplier;
                        return order;
                    }, splitOn: "Split1").FirstOrDefault();

                var orderLinesResult = gridReader.Read<OrderLine, Product, OrderLine>(
                    (orderLine, product) =>
                    {
                        orderLine.Product = product;
                        return orderLine;
                    }, splitOn: "Split1");

                orderResult.OrderLines = orderLinesResult.ToList();
                return orderResult;
            }
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            const string sql = @"
                INSERT INTO [Order] (SupplierId, CreatedDateTime)
                VALUES (@SupplierId, @CreatedDateTime)

                SELECT SCOPE_IDENTITY()  
            ";

            var parameters = new 
            { 
               SupplierId = order.Supplier.Id, 
               CreatedDateTime = DateTime.Now
            };


            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                return await dbConnection.ExecuteScalarAsync<int>(sql, parameters);
            }
        }

        public async Task CreateOrderLineAsync(OrderLine orderLine, int orderId)
        {
            const string sql = @"
                INSERT INTO OrderLines (OrderId, ProductId, ProductBatchId, DeliveryDate, Units)
                VALUES (@OrderId, @ProductId, @ProductBatchId, @DeliveryDate, @Units)
            ";

            var parameters = new
            {
                OrderId = orderId,
                ProductId = orderLine.Product.Id,
                ProductBatchId = orderLine.ProductBatch.Id,
                orderLine.DeliveryDate,
                orderLine.Units
            };


            using (var dbConnection = await _sqlClient.GetDbConnectionAsync())
            {
                await dbConnection.ExecuteAsync(sql, parameters);
            }
        }
    }
}

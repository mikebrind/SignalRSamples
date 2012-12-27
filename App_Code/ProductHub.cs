using Microsoft.AspNet.SignalR.Hubs;
using WebMatrix.Data;

public class ProductHub : Hub
{
    public void BuyProduct(int productId) {
        using (var db = Database.Open("Northwind")) {
            var unitsInStock = db.QueryValue("SELECT UnitsInStock FROM Products WHERE ProductId = @0", productId);
            if (unitsInStock > 0) {
                db.Execute("UPDATE Products Set UnitsInStock = UnitsInStock - 1 WHERE ProductId = @0", productId);
                unitsInStock -= 1;
            }
            Clients.All.updateAvailableStock(unitsInStock, productId);
        }
    }

    public void ReorderProduct(int quantity, int productId){
        using (var db = Database.Open("Northwind")) {
            db.Execute("UPDATE Products Set UnitsInStock = UnitsInStock + @0 WHERE ProductId = @1", quantity, productId);
            var unitsInStock = db.QueryValue("SELECT UnitsInStock FROM Products WHERE ProductId = @0", productId);
            Clients.All.updateAvailableStock(unitsInStock, productId);
        }
    }
}
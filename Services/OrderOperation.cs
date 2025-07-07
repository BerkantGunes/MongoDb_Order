using MongoDB.Bson;
using MongoDB.Driver;
using Project9_MongoDbOrder.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project9_MongoDbOrder.Services
{
    public class OrderOperation
    {
        public void AddOrder(Order order)
        {
            var connection = new MongoDbConnection(); // Bağlantının olduğu adrese git
            var orderCollection = connection.GetOrdersCollection(); // Sonra koleksiyona git.

            var document = new BsonDocument
            {
                {"CustomerName", order.CustomerName },
                {"Region", order.Region },
                {"City", order.City },
                {"TotalPrice", order.TotalPrice }
            };

            orderCollection.InsertOne(document); // ekleme işleminin gerçekleştiği kısım.
        }

        public List<Order> GetAllOrders() // Tüm siparisleri Getir
        {
            var connection = new MongoDbConnection(); // Baglantımızı actık
            var orderCollection = connection.GetOrdersCollection();

            var orders = orderCollection.Find(new BsonDocument()).ToList(); // Herhangi bir şart olmadan bütün verileri getirmesi için orders degiskeni tanımladık. Bu değişkeni prop larla eşleştirmeliyiz.

            List<Order> orderList = new List<Order>(); // Şuanda içi boş olan bi orderList miz oldu.

            foreach(var order in orders) // orders degiskeniyle hafızada tuttugumuz verileri orderListe aktarır
            {
                orderList.Add(new Order // Her defasında yeni bir liste ekleriz.
                {
                    City = order["City"].ToString(), // Order tablosundan gelen değerleri stringe çevirdik.
                    CustomerName = order["CustomerName"].ToString(),
                    Region = order["Region"].ToString(),
                    OrderId = order["_id"].ToString(),
                    TotalPrice = decimal.Parse(order["TotalPrice"].ToString())
                });
            }
            return orderList;
        }

        public void DeleteOrder(string orderId)
        {
            var connection = new MongoDbConnection();
            var orderCollection = connection.GetOrdersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(orderId));
            orderCollection.DeleteOne(filter);
        }

        public void UpdateOrder(Order order)
        {
            var connection = new MongoDbConnection();
            var orderCollection = connection.GetOrdersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",ObjectId.Parse(order.OrderId));
            var updatedValue = Builders<BsonDocument>.Update
                .Set("CustomerName", order.CustomerName)
                .Set("Region", order.Region)
                .Set("City", order.City)
                .Set("TotalPrice", order.TotalPrice);
            orderCollection.UpdateOne(filter, updatedValue);
        }

        public Order GetOrderById(string orderId)
        {
            var connection = new MongoDbConnection();
            var orderCollection = connection.GetOrdersCollection();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(orderId));
            var result = orderCollection.Find(filter).FirstOrDefault();
            if (result != null)
            {
                return new Order
                {
                    OrderId = orderId,
                    City = result["City"].ToString(),
                    Region = result["Region"].ToString(),
                    CustomerName = result["CustomerName"].ToString(),
                    TotalPrice = decimal.Parse(result["TotalPrice"].ToString()),

                };
            }
            else
            {
                return null;
            }
        }
    }
}

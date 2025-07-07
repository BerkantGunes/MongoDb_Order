using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project9_MongoDbOrder.Entities
{
    public class Order
    {
        [BsonId] // Bu prop un ID oldugunu programa tanıttık
        [BsonRepresentation(BsonType.ObjectId)] // GUID degeri alması saglayan Attribute. Yani otomatik artısı saglar. Otomatik bir ID ye dönüşeceği formatı belirler.
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

using Project9_MongoDbOrder.Entities;
using Project9_MongoDbOrder.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project9_MongoDbOrder
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OrderOperation orderOperation = new OrderOperation();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var order = new Order // atamalarımızı burada yaparız.
            {
                City = txtCity.Text,
                OrderId = txtOrderId.Text,
                Region = txtRegion.Text,
                CustomerName = txtCustomer.Text,
                TotalPrice = decimal.Parse(txtTotalPrice.Text)

            };

            orderOperation.AddOrder(order);
            MessageBox.Show("Order Added Successfully");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List<Order> order = orderOperation.GetAllOrders(); // liste türünde değişken oluşturup GetAllOrders metotunu çağırdık.
            DGVOrders.DataSource = order;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string orderId = txtOrderId.Text;
            orderOperation.DeleteOrder(orderId);
            MessageBox.Show("Order Deleted Successfully!");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtOrderId.Text;
            var updateOrder = new Order
            {
                City = txtCity.Text,
                CustomerName = txtCustomer.Text,
                Region = txtRegion.Text,
                TotalPrice = decimal.Parse(txtTotalPrice.Text),
                OrderId = id
            };
            orderOperation.UpdateOrder(updateOrder);
            MessageBox.Show("Order Updated Successfully!");
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            string id = txtOrderId.Text;
            Order orders = orderOperation.GetOrderById(id);
            DGVOrders.DataSource = new List<Order> { orders };
        }
    }
}

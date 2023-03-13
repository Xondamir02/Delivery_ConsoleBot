
using Newtonsoft.Json;

namespace botTest.Models.Order
{
    public  class OrderServices
    {
        public List<Order> Orders { get; set; }
        public string OrderPath = "OrderData.json";

        public OrderServices() 
        {
            Orders= new List<Order>();
        }


        public void SaveOrderData()
        {
            var orderJson= JsonConvert.SerializeObject(Orders);
            File.WriteAllText(OrderPath, orderJson);
        }


        public void ReadOrderData()
        {
            if(File.Exists(OrderPath))
            {
                var orderData=File.ReadAllText(OrderPath);
                Orders = JsonConvert.DeserializeObject<List<Order>>(orderData);
            }
            else
            {
                Orders=new List<Order>();
            }
        }
    }
}

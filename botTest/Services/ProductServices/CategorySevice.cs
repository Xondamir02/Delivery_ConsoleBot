using botTest.Models;
using Newtonsoft.Json;

namespace botTest.Services.ProductServices
{
    partial class CategorySevice
    {
        public List<Category> Catigories { get; set; }
        private readonly string JsonFilePath = "Product.json";

        public CategorySevice() 
        {
            Catigories = new List<Category>();
            ReadCategories();
        }
       public void SaveCategories()
        {
            var json =JsonConvert.SerializeObject(Catigories);
            File.WriteAllText(JsonFilePath, json);
        }
        public void ReadCategories()
        {
            if (File.Exists(JsonFilePath))
            {
                var json = File.ReadAllText(JsonFilePath);
               Catigories= JsonConvert.DeserializeObject<List<Category>>(json);
            }
            else
               Catigories= new List<Category>();
        }
        
    }
}

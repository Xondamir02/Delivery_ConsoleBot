using botTest.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace botTest.Services.ProductServices
{
    partial class CategorySevice
    {

        public InlineKeyboardMarkup CreateButtonForCategory(List<Category> choiseType)
        {
            var inlineButtons = new List<List<InlineKeyboardButton>>();
            for (int i = 0; i < choiseType.Count; i++)
            {
                var button = new List<InlineKeyboardButton>()
        {
                    InlineKeyboardButton.WithCallbackData(choiseType[i].CategoryName, choiseType[i].CategoryId.ToString())

        };
                inlineButtons.Add(button);

            }
            var back=new List<InlineKeyboardButton>() 
            {
                InlineKeyboardButton.WithCallbackData("⬅️ Ortga", "back")
            };
            inlineButtons.Add(back);

            return new InlineKeyboardMarkup(inlineButtons);

        }

       public  InlineKeyboardMarkup CreateButtonForFood(List<Category> categories, int index)
        {
            var inlineButtons = new List<List<InlineKeyboardButton>>();
            foreach (var product in categories[index].Products)
            {
                var button = new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData(product.ProductName,product.ProductId.ToString())
        };
                inlineButtons.Add(button);
            }
            var back = new List<InlineKeyboardButton>()
            {
                InlineKeyboardButton.WithCallbackData("⬅️ Ortga", "back")
            };
            inlineButtons.Add(back);
            return new InlineKeyboardMarkup(inlineButtons);
        }


        public InlineKeyboardMarkup CreateButtonQuantity(int quantity)
        {
            var inlineButtons=new List<List<InlineKeyboardButton>>();
            var button=new List<InlineKeyboardButton>() 
            {
                InlineKeyboardButton.WithCallbackData("-", "-"),
                InlineKeyboardButton.WithCallbackData(quantity.ToString(), quantity.ToString()),
                InlineKeyboardButton.WithCallbackData("+", "+"),
            };
            var button2 = new List<InlineKeyboardButton>()
            {
                InlineKeyboardButton.WithCallbackData("📥 Savatga qo'shish", "savatga"),
            };
            var back = new List<InlineKeyboardButton>()
            {
                InlineKeyboardButton.WithCallbackData("⬅️ Ortga", "back")
            };
           
            inlineButtons.Add(button);
            inlineButtons.Add(button2);
            inlineButtons.Add(back);
            return new InlineKeyboardMarkup(inlineButtons);
        }


    }
}

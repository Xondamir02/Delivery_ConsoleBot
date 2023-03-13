
using botTest.Models;
using botTest.Models.Order;
using botTest.Models.User;
using botTest.Services.ProductServices;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = botTest.Models.User.User;

namespace botTest.Services
{
    partial class UserService
    {
      public void SendMenu(User user, ITelegramBotClient bot)
      {
            var buttons = new List<List<KeyboardButton>>()
            {
            new List<KeyboardButton>
         {
            new KeyboardButton("🍴 Menu"),
            new KeyboardButton("🛍 Mening buyurtmalarim"),
        },

         new List<KeyboardButton>
        {
            new KeyboardButton("✍️ Fikr bildirish"),
             new KeyboardButton("⚙️ Sozlamalar"),
        }


    };
            var keyboard = new ReplyKeyboardMarkup(buttons);
            keyboard.ResizeKeyboard = true;
            bot.SendTextMessageAsync(user.ChatId, "Quyidagilardan birini tanlang", replyMarkup: keyboard);

            UpdateUserStep(user, ENextMessage.InMenu,EBackStep.Default);
        }




        public void FoodMenu(User user, ITelegramBotClient bot,CategorySevice categorySevice)
        {
           
            bot.SendTextMessageAsync(
                user.ChatId, "Siz uchun kerakli bo'limni tanlang", replyMarkup: categorySevice.CreateButtonForCategory(categorySevice.Catigories));
            
            UpdateUserStep(user, ENextMessage.InChoice, EBackStep.InMenu);

            
        }



        public void InCategory(User user,ITelegramBotClient bot,CategorySevice categorySevice,string message)
        {
            if (CheckNumber(message))
            {
                int index = Convert.ToInt32(message) - 1;
                if (index < categorySevice.Catigories.Count)
                {
                    string path = $@"C:\Users\hp\source\repos\botTest\botTest\bin\Debug\net6.0\Photos\{categorySevice.Catigories[index].Image}";
                    using (FileStream stream = System.IO.File.OpenRead(path))
                    {
                        bot.SendPhotoAsync(user.ChatId, photo: stream, caption: $"{categorySevice.Catigories[index].CategoryName} bo'limi! \n\n" +
                            $"⬇ Quyidagilardan birini tanlang!",
                            replyMarkup: categorySevice.CreateButtonForFood(categorySevice.Catigories, index));

                    }
                    UpdateUserStep(user, ENextMessage.ChoosingQuantity, EBackStep.InChoice);

                }
                else
                {
                    bot.SendTextMessageAsync(user.ChatId, "Birodar berilgan tugmalardan birini tanlasangiz bo'lmaydimi 🙄 ..?");
                }
            }
            else if (message == "back")
            {
                
                SendMenu(user, bot);

            }
            

        }




        public void ChooseQuantity(User user,ITelegramBotClient bot,CategorySevice categorySevice,string message,int messageId,ref int quantity,Update update,OrderServices orderServices)
        {
            if (CheckNumber(message))
            {
                var productId = Convert.ToInt32(message);
                var categoryIndex = int.Parse(message.Substring(0, 1)) - 1;
                var product = categorySevice.Catigories[categoryIndex].Products.FirstOrDefault(u => u.ProductId == productId);
                user.UserProduct = product;
                if (product != null)
                {
                    string path = $@"C:\Users\hp\source\repos\botTest\botTest\bin\Debug\net6.0\Photos\{product.Image}";
                    using (FileStream stream = System.IO.File.OpenRead(path))
                    {
                        bot.SendPhotoAsync(user.ChatId, photo: stream, caption: $"{product.ProductName} \n\n{product.Description} \n\n💲 " +
                            $"Narxi: {product.Price} \n\n" +
                            $"⬇ Taom sonini tanlang!", replyMarkup: categorySevice.CreateButtonQuantity(quantity));

                        //UpdateUserStep(user, ENextMessage.Choosing);
                    }
                }
                else
                {
                    bot.SendTextMessageAsync(user.ChatId, "Noma'lum ma'lum buyruq.....");
                }
            }
            else if (message == "back")
            {
                FoodMenu(user,bot,categorySevice);
            }
            else if (message == "+")
            {
                quantity++;
                bot.EditMessageReplyMarkupAsync(user.ChatId, messageId,replyMarkup: categorySevice.CreateButtonQuantity(quantity));
            }
            else if (message == "-")
            {
                quantity--;
                if (quantity > 0)
                {
                    bot.EditMessageReplyMarkupAsync(user.ChatId, messageId, replyMarkup: categorySevice.CreateButtonQuantity(quantity));
                }
                else
                {
                    quantity = 1;
                    return;
                }
            }
            else if(message == "savatga")
            {
                //savatg qo'shish funksiyasi bor..
                Order order = new Order
                {
                    UserChatId = user.ChatId,
                    OrderId = (Guid.NewGuid().ToString()),
                    CreatedDate = DateTime.Now
                };

                Product product= new Product();
                product.ProductName = user.UserProduct!.ProductName;
                product.ProductId = user.UserProduct.ProductId;

                order.Products!.Add(product);
                orderServices.Orders.Add(order);
                orderServices.SaveOrderData();
                bot.SendTextMessageAsync(user.ChatId, "Maxsulot Savatga muvaffaqqiyatli qo'shildi");
                //UpdateUserStep(user, ENextMessage.InMenu);


            }
            else
            {
                bot.SendTextMessageAsync(user.ChatId, "Birodar berilgan tugmalardan birini tanlasangiz bo'lmaydimi 🙄 ..?");
            }

        }
        
    }
}

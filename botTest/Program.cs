using botTest.Models;
using botTest.Models.Order;
using botTest.Models.User;
using botTest.Services;
using botTest.Services.ProductServices;
using JFA.Telegram.Console;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using User = botTest.Models.User.User;

var botManager = new TelegramBotManager();

var bot = botManager.Create("TOKEN");
var botDetails = await bot.GetMeAsync();
Console.WriteLine($"{botDetails.FirstName}  is working..");

var userService = new UserService();
var categoryService = new CategorySevice();
OrderServices orderServices= new OrderServices();
int quantity = 1;

botManager.Start(OnUpdate);



void OnUpdate(Update update)
{
    var (chatId, message, name, isSucess) = GetMessage(update);

    var user=userService.AddUser(chatId,name,bot,update,botDetails);
    if (!isSucess)
        return;

    if (message == "/start")
    {

        user.Step = ENextMessage.Default;

    }
    var messageId = update.CallbackQuery?.Message?.MessageId ?? update.Message!.MessageId;

    switch (user.Step) 
    {
        case ENextMessage.Default:userService.SendMenu(user,bot);break;
        case ENextMessage.InMenu:ChooseMenu(user,message);break;
        case ENextMessage.InChoice:userService.InCategory(user,bot,categoryService,message);break;
        case ENextMessage.ChoosingQuantity:userService.ChooseQuantity(user,bot,categoryService,message,messageId,ref quantity,update,orderServices);break;
    }
}


void ChooseMenu(User user,string message)
{
    switch (message) 
    {
        case "🍴 Menu":userService.FoodMenu(user, bot,categoryService); break;
        case "🛍 Mening buyurtmalarim":break;
        case "✍️ Fikr bildirish":break;
        case "⚙️Sozlamalar":break;


    }
}



Tuple<long, string?, string?, bool> GetMessage(Update update)
{
    if (update.Type == UpdateType.Message && update.Message != null && update.Message.Text != null)
    {
        return new(update.Message!.From!.Id, update.Message.Text, update.Message!.From.FirstName, true);

    }
    if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery != null && update.CallbackQuery.Data != null)
    {
        return new(update.CallbackQuery!.From.Id, update.CallbackQuery!.Data, update.CallbackQuery!.From!.FirstName, true);
    }

    return new(default, default, default, false);

}

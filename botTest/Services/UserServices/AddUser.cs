using Telegram.Bot;
using Telegram.Bot.Types;
using User = botTest.Models.User.User;

namespace botTest.Services
{
    partial class UserService
    {
        public User AddUser(long chatId, string name, ITelegramBotClient bot, Update update, Telegram.Bot.Types.User botDetails)
        {
            if (_users.Any(u => u.ChatId == chatId))
            {
                return _users.First(u => u.ChatId == chatId);
            }
            else
            {
                var user = new User
                {
                    ChatId = chatId,
                    Name = name
                };
                _users.Add(user);
                SaveUsersJson();
                bot.SendTextMessageAsync(user.ChatId, $"Assalomu alaykum hurmatli " +
                    $"{update.Message.From.FirstName ?? update.Message.From.LastName ?? "foydalanuvchi"}" +
                    $"\n\"{botDetails.FirstName}\" botimizga Xush kelibsiz! ");
                return user;
            }
        }

    }
}

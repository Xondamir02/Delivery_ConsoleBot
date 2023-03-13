
using botTest.Models.User;
using Newtonsoft.Json;

namespace botTest.Services
{
    partial class UserService
    {
        public List<User> _users;
        private const string UserJsonFilePath = "users.json";
        public void UpdateUserStep(User user, ENextMessage step, EBackStep backStep)
        {
            user.Step = step;
            user.BackStep = backStep;
            SaveUsersJson();
        }

        public UserService()
        {
            ReadUsersJson();
        }
        public void SaveUsersJson()
        {
            var json = JsonConvert.SerializeObject(_users);
            File.WriteAllText(UserJsonFilePath, json);
        }

        private void ReadUsersJson()
        {
            if (File.Exists(UserJsonFilePath))
            {
                var json = File.ReadAllText(UserJsonFilePath);
                _users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            }
            else
            {
                _users = new List<User>();
            }
        }

       public bool CheckNumber(string? message)
        {
            if (message == null)
                return false;

            string result = "";

            foreach (var element in message)
            {
                if (char.IsDigit(element))
                    result += element;
            }

            return result.Length == message.Length;
        }
    }
}

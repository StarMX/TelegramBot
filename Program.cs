using MihaZupan;
using System;
using System.Linq;
using Telegram.Bot;

namespace StarZ.TelegramBot
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            String apiKey = "1310562522:AAEA11dOaqKCHOtA0g5rGjCAXnKjPiAf6tc";
           // var proxy = new HttpToSocks5Proxy("10.244.10.24", 2080);

            var bot = new TelegramBotBase.BotBase<forms.SimpleForm>(apiKey/*, new TelegramBotClient(apiKey, proxy)*/);

            //Add Systemcommands if you like, you could catch them later
            bot.BotCommands.Add(new Telegram.Bot.Types.BotCommand() { Command = "start", Description = "Starts the bot" });
            bot.BotCommands.Add(new Telegram.Bot.Types.BotCommand() { Command = "params", Description = "Returns all send parameters as a message." });
            bot.BotCommands.Add(new Telegram.Bot.Types.BotCommand() { Command = "help", Description = "帮助说明" });
            bot.BotCommand += async (s, en) =>
            {
                switch (en.Command)
                {
                    case "/params":

                        String m = en.Parameters.DefaultIfEmpty("").Aggregate((a, b) => a + " and " + b);

                        await en.Device.Send("Your parameters are " + m, replyTo: en.Device.LastMessageId);

                        break;
                    case "/help":
                        var btn = new TelegramBotBase.Form.ButtonForm();
                        btn.AddButtonRow(new TelegramBotBase.Form.ButtonBase("Google.com", "google", "https://www.google.com"), new TelegramBotBase.Form.ButtonBase("Blogs", "blogs", "https://zhangming.xin/"));
                        await en.Device.Send("帮助说明", btn);
                        break;
                    default:
                        await en.Device.Send($"Your Command {en.Command}");

                        break;
                }
            };
            
            //Update bot commands to botfather
            await bot.UploadBotCommands();
            await bot.Client.TelegramClient.SendTextMessageAsync(511550148, "TelegramBot Start");
            Console.WriteLine("TelegramBot Start");
            bot.Start();

            Console.ReadLine();
            bot.Stop();
        }
    }
}

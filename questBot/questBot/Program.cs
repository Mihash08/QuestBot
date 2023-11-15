using System;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bots.Http;
using Telegram.Bot.Types.Enums;
using System.Data.Common;
using Npgsql;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types.ReplyMarkups;

namespace questBot
{
    

    class Program
    {
        //TODO: remove private api key
        public static TelegramBotClient Bot;

        //static Dictionary<long, List<Step>> userStates = new Dictionary<long, List<Step>>();

        static void Main(string[] args)
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, "..\\..\\..\\.env");
            DotEnv.Load(dotenv);

            var botApi = Environment.GetEnvironmentVariable("TELEGRAM_API");
            if (botApi == null)
            {
                Console.WriteLine("Enviroment Variables getting error");
                return;
            }
            Bot = new TelegramBotClient
            (botApi);
            Console.WriteLine();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DBService.Connect();
            //TODO: remove this!!!
            //DBService.InitCreate();

            var recieverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage
                }
            };

            Bot.StartReceiving(UpdateHandler, ErrorHandler, recieverOptions);
            Console.WriteLine("Bot start");
            Console.ReadLine();
            
            DBService.Disconnect();
            Console.WriteLine("Bot finish");
        }


        private static Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
            if (exception.InnerException != null)
            {
                Console.WriteLine(exception.InnerException.Message);
            }
            return Task.CompletedTask;
        }

        private static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken token)
        {
            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Type == MessageType.Text)
                {

                    var recievedText = update.Message.Text;
                    var chatId = update.Message.Chat.Id;
                    var username = update.Message.Chat.Username;
                    Console.WriteLine($"{recievedText} from chat {chatId} with user {username}");
                    var stepId = DBService.GetChatStep(chatId);
                    if (stepId == -1)
                    {
                        Console.WriteLine("INSERTING NEW USER");
                        DBService.AddChat(chatId);
                        stepId = 0;
                        await Bot.SendTextMessageAsync(chatId, "Приветствую!\nЧтобы начать игру, тебе нужно написать мне секретный код" +
                            ", который можно получить у @Paper_hesus");
                    }
                    else
                    {
                        try
                        {
                            var currStep = Steps.steps[stepId];
                            var resp = currStep.action(recievedText);
                            if (resp.GetType() == typeof(SetTimerResponse))
                            {
                                DBService.SetStartTime(chatId);
                            }
                            if (resp.GetType() == typeof(StopTimerResponse))
                            {
                                var startTime = DBService.GetStartTime(chatId);
                                var speedPoints = (5 * 60 - (DateTime.Now - startTime).Minutes) * 2;
                                DBService.AddPoints(chatId, speedPoints);
                                var points = DBService.GetPoints(chatId);
                                await Bot.SendTextMessageAsync(chatId, $"Вы закончили квест за " +
                                    $"{(DateTime.Now - startTime).Hours} часов {(DateTime.Now - startTime).Minutes % 60} минут" +
                                    $"\nЗа скорость вы заработали {speedPoints} баллов", replyMarkup: GetButtons(new()));
                                await Bot.SendTextMessageAsync(chatId, $"В итоге вы имеете {points} баллов", replyMarkup: GetButtons(new()));
                            }
                            if (resp.GetType() == typeof(ResetPoints))
                            {
                                DBService.SetPoints(chatId, 0);
                                Console.WriteLine("Reset points");
                                await Bot.SendTextMessageAsync(chatId, $"У вас 0 баллов", replyMarkup: GetButtons(new()));
                            }
                            if (resp.GetType() == typeof(AddPointsRespone))
                            {
                                DBService.AddPoints(chatId, ((AddPointsRespone)resp).points);
                                int points = DBService.GetPoints(chatId);
                                await Bot.SendTextMessageAsync(chatId, $"Плюс {((AddPointsRespone)resp).points} баллов\nУ вас {points} баллов", replyMarkup: GetButtons(new()));
                            }
                            if (resp.GetType() == typeof(RemovePointsResponse))
                            {
                                DBService.RemovePoints(chatId, ((RemovePointsResponse)resp).points);
                                int points = DBService.GetPoints(chatId);
                                await Bot.SendTextMessageAsync(chatId, $"Минус {((RemovePointsResponse)resp).points} баллов\n" +
                                    $"У вас {points} баллов", replyMarkup: GetButtons(new()));
                            }
                            if (resp.nextStep != -1) {
                                DBService.SetStep(chatId, resp.nextStep);
                            }

                            await Bot.SendTextMessageAsync(chatId, resp.text, replyMarkup: GetButtons(resp.buttons));
                        } catch (Exception ex)
                        {
                            await Console.Out.WriteLineAsync("Bot send message error");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
            }
        }

        private static IReplyMarkup GetButtons(List<string> texts)
        {
            if (texts.Count == 0)
            {
                return new ReplyKeyboardRemove();
            }
            var buttons = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>()
            };
            
            foreach (var text in texts) {
                buttons[0].Add(new KeyboardButton(text));
            }
            return new ReplyKeyboardMarkup(
                buttons
            );
        }
    }
}
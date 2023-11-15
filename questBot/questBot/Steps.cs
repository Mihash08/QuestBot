using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Algorithms;   

namespace questBot
{
    public static class Steps
    {
        public static Dictionary<int, Step> steps = new Dictionary<int, Step>
        {
            {0, new Step(CheckCode) },
            {1, new Step(ChooseQuest)},
            {2, new Step(StartQuestActionCreator(Info.quests["Квест №1: Спасание принцессы Бич"]))},
            {3, new Step(QuestionActionCreator(Info.questions[1], 150))},
            {4, new Step(DisplayMessage("ФАН ФАКТ ПРО ЮГОСЛАВ ФИЛЬМ", 5))},
            {5, new Step(QuestionActionCreator(Info.questions[2], 25))},
            {6, new Step(QuestionActionCreator(Info.questions[3], 50))},
            {7, new Step(QuestionActionCreator(Info.questions[4], 50))},
            {8, new Step(QuestionActionCreator(Info.questions[5], 50))},
            {9, new Step(DisplayMessage("ФАН ФАКТ ПРО ZAPPA BAR", 10))},
            {10, new Step(QuestionActionCreator(Info.questions[6], 75))},
            {11, new Step(QuestionActionCreator(Info.questions[7], 100))},
            {12, new Step(DisplayMessage("Фан инфа: Студентческий парк расположен на полпути между площадью " +
                "Республики (на востоке) и Белградской крепостью (на западе). Он полностью окружен Студенческой" +
                " площадью, которая фактически превращается в четыре улицы вокруг парка. \r\n\r\nОдин из старейших " +
                "парков Белграда – Студенческий парк – стал местом встречи наркоманов и алкоголиков, бездомных, бомжей, " +
                "и, к сожалению, несовершеннолетних детей, оторванных от родительской опеки и внимания.", 13))},
            {13, new Step(QuestionActionCreator(Info.questions[8], 100))},
            {14, new Step(QuestionActionCreator(Info.questions[9], 25))},
            {15, new Step(QuestionActionCreator(Info.questions[10], 75))},
            {16, new Step(QuestionActionCreator(Info.questions[11], 25))},
            {17, new Step(QuestionActionCreator(Info.questions[12], 100))},
            {18, new Step(DisplayMessage("ФАН ФАКТ ПРО М10 БАР", 19))},
            {19, new Step(QuestionActionCreator(Info.questions[13], 50))},
            {20, new Step(QuestionActionCreator(Info.questions[14], 100))},
            {21, new Step(QuestionActionCreator(Info.questions[15], 150))},
            {26, new Step(DisplayMessage("Одно из самых популярных мест Белграда — именно тут устанавливают " +
                "главную городскую ёлку, здесь проходят митинги и праздничные концерты.\r\n\r\nКогда-то в этой " +
                "части столицы Сербии стояли большие ворота, окружённые рвом. Перед ними османы проводили жуткие" +
                " казни — провинившихся сажали на кол. Обретя независимость, жители Белграда снесли эту мрачную " +
                "«достопримечательность» и вскоре здесь появилось элегантное здание Национального театра (Французская улица, 3).", 22))},
            {22, new Step(QuestionActionCreator(Info.questions[16], 150))},
            {23, new Step(QuestionActionCreator(Info.questions[17], 100))},
            {24, new Step(QuestionActionCreator(Info.questions[18], 50))},
            {25, new Step(QuestionActionCreator(Info.questions[19], 25))},
            {27, new Step(DisplayMessage("Место в целом прикольное. Хуй найдешь. Но атмосфера тут огонь!", 28))},
            {28, new Step(QuestionActionCreator(Info.questions[20], 75))},
            {29, new Step(QuestionActionCreator(Info.questions[21], 50))},
            {30, new Step(DisplayMessage("Фан факт: Сквер с топ видом. Насладитесь им!\r\n\r\nГостиница " +
                "«Москва» в Белграде. Гостиница «Москва» — роскошное пятиэтажное здание в стиле модерн в самом " +
                "центре Белграда. За более чем 100-летнюю историю в ней останавливались больше 40 млн гостей. " +
                "Среди них — знаменитые на весь мир актеры, поэты, режиссеры, государственные деятели, чьи " +
                "портреты развешены по стенам в коридорах. В одной компании здесь Леонид Брежнев и Брэд Питт," +
                " Мила Йовович и Сергей Бондарчук, Валентина Терешкова и" +
                " Роберт де Ниро, Юрий Гагарин и Джек Николсон.", 31))},
            {31, new Step(QuestionActionCreator(Info.questions[22], 50))},
            {32, new Step(DisplayMessage("Фан Факт: Вообще место приятное. Куча движух тут всяких.\r\n\r\n" +
                "GRAD – Европейский центр культуры и дебатов, также известный как KC GRAD, был открыт в 2009 " +
                "году. Это продукт совместной инициативы Культурного фронта в Белграде и Фонда Феликса Меритиса, б" +
                "азирующегося в Амстердаме. КС Град расположен в старом складе, расположенном в старом, заброшенном" +
                " и ветхом промышленном районе в самом центре Белграда, на берегу реки Сава.", 33))},
            {33, new Step(QuestionActionCreator(Info.questions[23], 75))},
            {34, new Step(QuestionActionCreator(Info.questions[24], 150))},
            {35, new Step(DisplayMessage("ФАН ФАКТ ПРО BETON HALA", 36))},
            {36, new Step(EndingActionCreator("Ааааа и мы не нашли Бич! Но думаю  повеселились. " +
                "\r\n\r\nНо так и бывает и в жизни. Но все будет круто))" +
                "Хз как вы чувствуете себя в финале этого трипа, но хочу думать что вы остались" +
                " довольны и узнали много нового. \r\n\r\nНо пить это зло. Жить это круто.  Эта " +
                "игра спонсированна и созданна моим безумным, но веселым мозгом, антидеприсантами и" +
                " 3 месяцами трезвости.\r\n\r\nХотите играть дальше, наслажадаться жизнью и делать " +
                "пободные вещи - мой совет - бросайте пить \r\n\r\nА теперь я хочу услышать ваши отзывы." +
                " И задать пару вопросов.  И да это наеб. Принцессу никто не воровал. Вы че ебнулись? " +
                "Я просто случайно придумал дикий сюжет исходя из ЦА. \r\n\r\nНо если вам зашло, то я" +
                " рад  как бы вы это оценили?  Что понравилось и что нет?  \r\nСколько бы вы за это " +
                "заплатили? \r\nИ если хотите можете оставить донат) \r\n Хотели бы поиграть в такое в" +
                " будущем? \r\nТогда рассказывайте друзьям, подписывайтесь на канал и жду вас через какое-то время)))", 37))},
            {37, new Step(DisplayMessage("Если хотите снова сыграть, мне понадобится новый код", 0))},

        };


        
        static public Response CheckCode(string code)
        {   
            Console.WriteLine("Code Check");
            //TODO: figure out new code system
            if (code == "123code")
            {
                return new("Рад, что вы добрались\nВот пара правил:\r\n\r\n" +
                    "1) Повеселитесь\r\n" +
                    "2) Использовать можно любые способы (интернет, мозг, звонок другу...)\r\n" +
                    "3) Это игра, так что играйте\r\n" +
                    "4) Покайфуйте\r\n" +
                    "5) Если будут траблы или тяжко или еще что-то, " +
                    "свободно спросите автора @Paper_hesus\r\n\r\n" +
                    "Да! Поздравляю с Алкогольными играми! И пусть удача всегда будет на вашей стороне!" +
                    "\nОсталось только выбрать квест",
                    Info.quests.Select(q => q.Value.name).ToList(), 1);
                
            } else
            {
                return new("Код какой-то не тот...",
                    new() { }, 0);
            }
        }

        static public Response ChooseQuest(string input)
        {
            if (Info.quests.ContainsKey(input))
            {
                var quest = Info.quests[input];
                Console.WriteLine($"Choosing quest: {input}");
                return new(quest.welcomeMessage,
                    new() {"Поехали!", "Нет, хочу другой квест" }, quest.firstStepId);
            } else
            {
                Console.WriteLine($"Quest with name {input} not found");
                return new("Надо выбрать квест нажатием соответствуещей кнопки",
                    Info.quests.Select(q => q.Value.name).ToList(), 1);
            }
        }

        private static Func<string, Response> StartQuestActionCreator(Quest quest)
        {
            return (string input) =>
            {
                if (input == "Поехали!")
                {
                    return new SetTimerResponse(quest.firstQuestion.body, new() { "Подсказку! (-50 баллов)", "Пропустить! (-100 баллов)" }, quest.firstQuestionStep);
                }
                else
                {
                    return new("Выбери новый квест", Info.quests.Select(q => q.Value.name).ToList(), 1);
                }
            };
        }

        private static Func<string, Response> QuestionActionCreator(Question question, int price)
        {
            return (string input) =>
            {
                if (input == "Далее")
                {
                    return new(question.body, new() { "Подсказку! (-50 баллов)", "Пропустить! (-100 баллов)" }, -1);
                }
                if (input == "Подсказку! (-50 баллов)")
                {
                    return new RemovePointsResponse(question.hint, new() { "Подсказку! (-50 баллов)", "Пропустить! (-100 баллов)" }, -1, 50);
                }
                if (input == "Пропустить! (-100 баллов)")
                {
                    if (question.isQuick)
                    {
                        return new RemovePointsResponse(question.congratsText, new() { "Подсказку! (-50 баллов)", "Пропустить! (-100 баллов)" }, question.answerStepId, 100);
                    }
                    return new RemovePointsResponse(question.congratsText, new() { "Фан факт" }, question.answerStepId, 100);
                }
                if (StringNormalizer.Normalize(input) == question.answer.ToLower() || 
                    ( LevenshteinDistance.Calculate(StringNormalizer.Normalize(input), question.answer.ToLower()) < 2 && StringNormalizer.Normalize(input).Length > 4))
                {
                    if (question.isQuick)
                    {
                        return new AddPointsRespone(question.congratsText, new() { "Подсказку! (-50 баллов)", "Пропустить! (-100 баллов)" }, question.answerStepId, price);
                    }
                    return new AddPointsRespone(question.congratsText, new() { "Фан факт" }, question.answerStepId, price);
                }
                if (LevenshteinDistance.Calculate(StringNormalizer.Normalize(input), question.answer.ToLower()) <= question.answer.Length / 4)
                {
                    return new("Ответ очень близок, проверьте ответ на опечатки", new() { "Подсказку! (-50 баллов)", "Пропустить! (-100 баллов)" }, -1);
                }
                else
                {
                    return new("Ответ неверный, попробуйте еще раз", new() { "Подсказку! (-50 баллов)", "Пропустить! (-100 баллов)" }, -1);
                }
            };
        }

        private static Func<string, Response> DisplayMessage(string text, int nextStep)
        {
            return (string input) =>
            {
                return new(text, new() { "Далее" }, nextStep);
            };
        }
        static public Func<string, Response> EndingActionCreator(string text, int nextStep)
        {
            Console.WriteLine("Finished quest");
            return (string input) =>
            {
                return new StopTimerResponse(text, new() { "Далее" }, nextStep);
            };
        }
    }

    public class Response
    {
        public string text;
        public List<string> buttons;
        public int nextStep;
        public Response(string text, List<string> buttons, int nextStep)
        {
            this.text = text;
            this.buttons = buttons;
            this.nextStep = nextStep;
        }
    }
    public class SetTimerResponse : Response
    {
        public SetTimerResponse(string text, List<string> buttons, int nextStep) : base(text, buttons, nextStep)
        {
        }
    }
    public class ResetPoints : Response
    {
        public ResetPoints(string text, List<string> buttons, int nextStep) : base(text, buttons, nextStep)
        {
        }
    }
    public class StopTimerResponse : Response
    {
        public StopTimerResponse(string text, List<string> buttons, int nextStep) : base(text, buttons, nextStep)
        {
        }
    }
    public class AddPointsRespone : Response
    {
        public int points;
        public AddPointsRespone(string text, List<string> buttons, int nextStep, int points) : base(text, buttons, nextStep)
        {
            this.points = points;
        }
    }
    public class RemovePointsResponse : Response
    {
        public int points;
        public RemovePointsResponse(string text, List<string> buttons, int nextStep, int points) : base(text, buttons, nextStep)
        {
            this.points = points;
        }
    }

    public class Step
    {
        public Func<string, Response> action;
        public Step(Func<string, Response> action)
        {
            this.action = action;
        }
    }
}

using Homework7_Quiz_;
using System.Security.Cryptography;
using Newtonsoft.Json;

public class Program
{
    private static TestBase testsBase = new TestBase();
    private static ScoreManager scoreManager = new ScoreManager();
    public static void Main(string[] args)
    {
        DateTime startTime = DateTime.Now;
        
        InitializeTest();

        ShowText("Выберите тему квиза. Напишите номер темы:", bottom:true);

        ShowTestList(0, 3);

        int selectTopic = Convert.ToInt32(Console.ReadLine());
        Test currentTest = testsBase.GetTestById(selectTopic);
        if (currentTest == null)
        {
            ShowText("Вы выбрали несуществующий тест", true, true);
            return;
        }
        Random random = new Random();
        for (int i = 0; i < currentTest.questions.Length; i++)
        {
            string[] answers = currentTest.questions[i].options;
            Console.WriteLine($"{i + 1}. {currentTest.questions[i].question} ");
            int variantCounter = 1;
            string[] trueAnswerTemp = currentTest.questions[i].correctAnswers;
            string[] randomVariants = answers;
            for (int j = 0; j < answers.Length; j++)
            {
                int randomNumber = random.Next(j, randomVariants.Length);
                string answerTemp = randomVariants[j];
                answers[j] = randomVariants[randomNumber];
                randomVariants[randomNumber] = answerTemp;
            }
            foreach (string variant in randomVariants)
            {
                Console.WriteLine($"{variantCounter}) {variant}");
                variantCounter++;
            }
            string userAnswer = Console.ReadLine();
            string[] parts = userAnswer.Split(',');
            if (string.IsNullOrEmpty(userAnswer))
            {
                ShowText("Ответ пустой, попробуйте снова");
                return;
            }
            if (parts.Length == 0)
            {
                ShowText("Ни одного ответа не было введено, попробуйте снова");
                return;
            }

            int[] userAnswers = new int[parts.Length];
            for (int j = 0; j < parts.Length; j++)
            {
                if (int.TryParse(parts[j].Trim(), out userAnswers[j]) == false)
                {
                    ShowText($"{parts[j]} - является некорректным вводом");
                    return;
                }
            }
            foreach (int answer in userAnswers)
            {
                if (answer > currentTest.questions[i].options.Length + 1)
                {
                    ShowText("Нет такого варианта ответа");
                    return;
                }
            }
            int trueAnswerCount = 0;
            foreach (string correcttAnswer in trueAnswerTemp)
            {
                foreach (int answer in userAnswers)
                {
                    if (correcttAnswer.Equals(randomVariants[answer - 1]))
                    {
                        
                        trueAnswerCount++;
                        
                    }
                }
            }

            string trueAnswers = "";
            foreach (string trueAnswer in trueAnswerTemp)
            {
                trueAnswers += trueAnswer + ", ";
            }

            if (trueAnswerCount == 0)
            {
                ShowText($"Нет правильных ответов! Правильные ответы: {trueAnswers}", true, true);
            }
            else if(trueAnswerCount != trueAnswerTemp.Length)
            {
                int points = currentTest.questions[i].points / 2;
                scoreManager.AddScore(points);
                ShowText($"Не указаны все правильные ответы! Правильные ответы: {trueAnswers}", true, true);
            }
            else
            {
                scoreManager.AddScore(currentTest.questions[i].points);
                ShowText("Правильный ответ!", true, true);
            }
            
            
        }
        int maxPoints = 0;
        foreach (Question question in currentTest.questions)
        {
            maxPoints += question.points;
        }

        ShowText($"{scoreManager.Score}/{maxPoints}", true, true);
        DateTime endTime = DateTime.Now;
        TimeSpan testTimer = endTime - startTime;
        string formatedStartTime = startTime.ToShortTimeString();
        string formatEndTime = endTime.ToShortTimeString();
        string formatTime = string.Format("{0:D2}:{1:D2}:{2:D2}", testTimer.Hours, testTimer.Minutes, testTimer.Seconds);
        ShowText($"Тестирование начато в {formatedStartTime} и завершено в {formatEndTime}. Общее время на тестирование: {formatTime}", true, false);
        
    }

    public static void ShowText(string text, bool upper = false, bool bottom = false)
    {
        if (upper) Console.WriteLine();
        Console.WriteLine(text);
        if (bottom) Console.WriteLine();
    }

    private static void ShowTestList(int from, int to)
    {
        var testList = testsBase.GetTestList();

        int fromClamped = Math.Clamp(from, 0, testList.Count);
        int toClamped = Math.Clamp(to, from, testList.Count);

        for (int k = fromClamped; k < toClamped; k++)
        {
            Console.WriteLine($"{k + 1}) {testList[k].name}");
        }
    }

    public static void InitializeTest()
    {
        TestList testList = AwaitGetTestData().GetAwaiter().GetResult();

        for (int i = 0; i < testList.tests.Length; i++)
        {
            Question[] questions = new Question[testList.tests[i].questions.Length];
            for (int j = 0; j < testList.tests[i].questions.Length; j++)
            {
                questions[j] = new Question(testList.tests[i].questions[j].question, testList.tests[i].questions[j].correct_answers, testList.tests[i].questions[j].options, testList.tests[i].questions[j].points);
            }
            Test newTest = new Test(testList.tests[i].theme, "", questions);
            testsBase.AddTest(newTest);
        }
        

    }

    public static async Task<TestList> AwaitGetTestData()
    {
        TestInformation testInformation = new TestInformation();
        string jsonData = await testInformation.GetTestData();
        TestList testData = JsonConvert.DeserializeObject<TestList>(jsonData);
        return testData;
    }
} 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public static TestManager instance = null;

    public TMP_Text questionText;
    public TMP_Text[] answerButtonTexts;
    public GameObject[] answerButtons;

    public int questionNumber = 5;
    public Question[] questions;
    string theme;
    int questionCounter = 0;
    public int correctAnswerCounter = 0;
    public double score;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);

        questions = GetQuestions(IntersceneMemory.instance.themeIndex);

        ShowQuestion(questions[questionCounter]);
    }

    void ShowQuestion(Question activeQuestion)
    {
        questionText.text = activeQuestion.question;
        answerButtonTexts[0].text = activeQuestion.answers[0].answerText;
        answerButtonTexts[1].text = activeQuestion.answers[1].answerText;

        for (int i = 2; i <= 5; i++)
        {
            if (activeQuestion.answers.Length >= i + 1)
            {
                answerButtons[i].SetActive(true);
                answerButtonTexts[i].text = activeQuestion.answers[i].answerText;
            }
            else
            {
                answerButtons[i].SetActive(false);
            }
        }
    }

    public void FinishQuestion()
    {
        double addToScore = 0;

        double correctAnswersChosen = 0;
        double correctAnswersNotChosen = 0;
        double incorrectAnswersChosen = 0;
        double incorrectAnswersNotChosen = 0;

        for (int i = 0; i < questions[questionCounter].answers.Length; i++)
        {
            if (questions[questionCounter].answers[i].isCorrect && answerButtons[i].GetComponent<ButtonTestAnswerScript>().isPressed)
            {
                correctAnswersChosen++;
            }
            if (questions[questionCounter].answers[i].isCorrect && !answerButtons[i].GetComponent<ButtonTestAnswerScript>().isPressed)
            {
                correctAnswersNotChosen++;
            }
            if (!questions[questionCounter].answers[i].isCorrect && answerButtons[i].GetComponent<ButtonTestAnswerScript>().isPressed)
            {
                incorrectAnswersChosen++;
            }
            if (!questions[questionCounter].answers[i].isCorrect && !answerButtons[i].GetComponent<ButtonTestAnswerScript>().isPressed)
            {
                incorrectAnswersNotChosen++;
            }
        }

        addToScore += (correctAnswersChosen - incorrectAnswersChosen) /
            (correctAnswersChosen + correctAnswersNotChosen);
        if (addToScore < 0)
        {
            addToScore = 0;
        }
        score += addToScore;

        if (addToScore == 1)
        {
            correctAnswerCounter++;
        }

        NextQuestion();
    }

    void NextQuestion()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<ButtonTestAnswerScript>().UnpressButton();
        }

        if (questionCounter + 1 < questions.Length)
        {
            questionCounter++;
            ShowQuestion(questions[questionCounter]);
        }
        else
        {
            SceneManager.LoadScene("TestResults");
        }
    }


    static int[] NumberShuffle(int shuffledLength, int originalLength)
    //принимает длину нужного массива и длину оригинального массива. Выдает индексы в случайном порядке, перетасованные
    {
        int[] originalArray = new int[originalLength];
        int[] shuffledArray = new int[shuffledLength];

        for (int i = 0; i < originalArray.Length; i++)
        {
            originalArray[i] = i;
        }

        int index = 0;
        for (int i = 0; i < shuffledArray.Length; i++)
        {
            index += Random.Range(0, originalArray.Length - 1);
            if (index >= originalArray.Length)
            {
                index -= originalArray.Length;
            }

            while (originalArray[index] == -1)
            {
                index++;
                if (index >= originalArray.Length)
                {
                    index -= originalArray.Length;
                }
            }

            shuffledArray[i] = originalArray[index];
            originalArray[index] = -1;
        }

        return shuffledArray;
    }

    static Answer[] AnswerShuffle(Answer[] inputAnswerArray)
    //принимает массив ответов. Выдает его же перетасованным
    {
        int[] indexes = NumberShuffle(inputAnswerArray.Length, inputAnswerArray.Length);
        Answer[] shuffledAnswerArray = new Answer[inputAnswerArray.Length];

        for (int i = 0; i < inputAnswerArray.Length; i++)
        {
            shuffledAnswerArray[i] = inputAnswerArray[indexes[i]];
        }

        return shuffledAnswerArray;
    }

    //ниже идут списки тем, вопросов, ответов. лучше их не мешать с прочими методами.

    public Question[] GetQuestions(int themeIndex)
    {
        int[] indexes = NumberShuffle(questionNumber, allQuestions[themeIndex].Length);

        Question[] shuffledQuestions = new Question[questionNumber];

        for (int i = 0; i < shuffledQuestions.Length; i++)
        {
            shuffledQuestions[i] = allQuestions[themeIndex][indexes[i]];
        }

        //нужно сгенерировать массив массивов, содержащий перетасованные индексы для каждого ответа на каждый вопрос, 
        //одновременно. а после этого уже в соответствии с индексами переставить все ответы в нужно порядке.

        for (int i = 0; i < shuffledQuestions.Length; i++)
        {
            shuffledQuestions[i].answers = AnswerShuffle(shuffledQuestions[i].answers);
        }

        return shuffledQuestions;
    }

    Question[][] allQuestions = new Question[][]
    {
        new Question[]
        {
            new Question("Пожар можно охарактеризовать как:",
            "неконтролируемое горение, причиняющее различного рода ущерб.", true,
            "контролируемое горение, причиняющее различного рода ущерб.", false,
            "неконтролируемое горение, не причиняющее ущерба.", false,
            "контролируемое горения, не причиняющее ущерба.", false),

            new Question("Процесс горения можно охарактеризовать как:",
            "экзотермическую химическую реакцию, прогрессирующе самоускоряющуюся.", true,
            "эндотермическую химическую реакцию, прогрессирующе самоускоряющуюся.", false,
            "экзотермическую химическую реакцию, прогрессирующе самозамедляющуюся.", false,
            "эндотермическую химическую реакцию, прогрессирующе самозамедляющуюся.", false,
            "экзотермическую химическую реакцию, протекающую с неизменной скоростью.", false,
            "эндотермическую химическую реакцию, протекающую с неизменной скоростью.", false),

            new Question("Горючая среда может быть охарактиризована как:",
            "среда, способная воспламеняться при воздействии источника зажигания.", true,
            "средство энергетического воздействия, инициирующее возникновение горения.", false,
            "неконтролируемое горение вне специального очага без нанесения ущерба.", false),

            new Question("Источник зажигания может быть охарактиризован как:",
            "средство энергетического воздействия, инициирующее возникновение горения.", true,
            "среда, способная воспламеняться при воздействии источника зажигания.", false,
            "неконтролируемое горение вне специального очага без нанесения ущерба.", false),

            new Question("Загорание может быть охарактеризовано как:",
            "неконтролируемое горение вне специального очага без нанесения ущерба.", true,
            "среда, способная воспламеняться при воздействии источника зажигания.", false,
            "средство энергетического воздействия, инициирующее возникновение горения.", false),

            new Question("К опасным факторам пожара, воздействующим на людей и имущество, относятся:",
            "пламя и искры.", true,
            "тепловой поток.", true,
            "пониженная температура окружающей среды.", false,
            "повышенная концентрация токсичных продуктов горения и термического разложения.", true,
            "повышенная концентрация кислорода.", false,
            "снижение видимости в дыму.", true),

            new Question("Каждый гражданин при обнаружении пожара или признаков горения должен:",
            "сообщить об этом по телефону в пожарную охрану.", true,
            "принять по возможности меры по эвакуации людей, тушению пожара и сохранности материальных ценностей.", true,
            "немедленно эвакуироваться лично.", false,
            "проигнорировать его в целях недопущения паники.", false),

        },
        new Question[]
        {
            new Question("Первичные средства пожаротушения подразделяются на следующие типы:",
            "переносные и передвижные огнетушители.", true,
            "пожарные краны и средства обеспечения их использования.", true,
            "покрывала для изоляции очага возгорания.", true,
            "противопожарные двери.", false,
            "водопроводные трубы.", false),

            new Question("Пожарный кран (ПК) – это комплект, состоящий из: ",
            "клапана, установленного на пожарном трубопроводе и оборудованного пожарной соединительной головкой.", true,
            "пожарного рукава с ручным стволом.", true,
            "водопроводной трубы.", false,
            "огнетушителя.", false,
            "пожарного топора.", false),

            new Question("Огнетушители предназначены для: ",
            "тушения пожара на начальной стадии его развития.", true,
            "тушения пожара на поздней стадии его развития.", false,
            "использования после окончания пожара.", false,
            "не предназначены для тушения пожара.", false),

            new Question("Подход к очагу пожара нужно тушить: ",
            "с наветренной стороны, начиная с его переднего края и постепенно перемещаясь вглубь.", true,
            "с подветренной стороны, начиная с его переднего края и постепенно перемещаясь вглубь.", false,
            "с наветренной стороны, начиная с его дальнего края и постепенно перемещаясь вглубь.", false,
            "с подветренной стороны, начиная с его дальнего края и постепенно перемещаясь вглубь.", false),

            new Question("Льющуюся с высоты горящую жидкость нужно тушить: ",
            "сверху вниз.", true,
            "снизу вверх.", false,
            "слева направо.", false,
            "справа налево.", false),

            new Question("Горящую вертикальную поверхность надо тушить: ",
            "снизу вверх.", true,
            "сверху вниз.", false,
            "слева направо.", false,
            "справа налево.", false),

            new Question("При наличии нескольких огнетушителей: ",
            "необходимо применять их одновременно.", true,
            "нельзя применять их одновременно.", false,
            "необходимо применять их поперменно.", false,
            "необходимо оставить второй на пожарном щите.", false),
        },
        new Question[]
        {
            new Question("Эвакуация осуществляется:",
            "эвакуационными путями через эвакуационные выходы, указанными в плане эвакуации при пожаре.", true,
            "через окна.", false,
            "способом индивидуальным для каждого эвакуирующегося.", false,
            "строго через главный вход.", false),

            new Question("При пожаре эвакуировать людей следует:",
            "наружу", true,
            "в безопасные зоны", true,
            "как можно глубже", false,
            "как можно выше", false),

            new Question("Объемно-планировочные решения и конструктивные исполнения эвакуационных путей",
            "необходимы каждому зданию и сооружению", true,
            "необходимы местам массового пребывания", false,
            "не требуются обычно", false,
            "нужны в отдельных случаях", false),

            new Question("Руководители тушения пожара, а также лица, проводящие спасательные работы должны:",
            "организовать и провести эвакуацию", true,
            "принять меры по предотвращению паники", true,
            "лично потушить пожар", false,
            "эвакуироваться самостоятельно в первую очередь", false),

            new Question("В случае реальной угрозы людям необходимо:",
            "ввести все основные силы и средства для защиты путей эвакуации людей и спасательных работ", true,
            "сосредоточиться на тушении пожара", false,
            "порекомендовать людям спасаться в индивидуальном порядке", false,
            "ускорить эвакуацию", false),

            new Question("Основные и запасные пути эвакуации могут быть использованы: ",
            "для введения сил и средств пожарной охраны на тушение при отсутствии людей в помещениях.", true,
            "только для эвакуации", false,
            "для для введения сил и средств пожарной охраны на тушение в любой момент", false,
            "для любых целей", false),

            new Question("В целях пресечения паники используют: ",
            "электромегафоны и другие средства звуковой связи", true,
            "подачу пожарных стволов на тушение видимых людям очагов пожара", true,
            "громкие крики", false,
            "отрицание наличия пожара", false,
            "разрешение эвакуироваться в индивидуальном порядке", false),
        },
        new Question[]
        {
            new Question("Первым делом пострадавшему при пожаре необходимо:",
            "прекратить контакт с отравляющими веществами", true,
            "лечь как можно быстрее", false,
            "обработать ожоги", false,
            "облиться водой", false),

            new Question("Вынесенного на свежий воздух пострадавшего необходимо:",
            "избавить от стесняющей дыхание одежды и всех украшений на шее", true,
            "закутать как можно крепче", false,
            "облить свежей водой", false,
            "оставить лежать на свежем воздухе до прибытия врачей", false),

            new Question("При ожогах кистей следует:",
            "как можно быстрее снять кольца с пальцев", true,
            "закрыть их перчатками", false,
            "положить их поверх тела", false,
            "опустить их в воду и не вынимать", false),

            new Question("В случае горения одежды необходимо:",
            "сбросить ее или погасить пламя", true,
            "в обожженных местах разрезать и сбросить по частях", true,
            "сорвать как можно быстрее", false,
            "просто положить пострадавшего на землю", false),

            new Question("При ожогах дыхательных путей необходимо:",
            "запретить пострадавшему пить, есть, говорить", true,
            "напоить пострадавшего большим количеством воды", false,
            "запретить пострадавшему дышать", false,
            "обработать ожоги через ротовое отверствие", false),

            new Question("Обгорание волос около носа и в ноздрях, и следы копоти на языке:",
            "являются признаками ожога дыхательных путей", true,
            "не являются тревожным признаком", false,
            "являются признаком внешних ожогов", false,
            "являются признаком отравления", false),

            new Question("На поверхность раны имеет смысл:",
            "наложить стерильную повязку", true,
            "наложить чистую сухую ткань", true,
            "наложить пропитанную водой ткань", false,
            "не накладывать ничего, оставить ее заживать свободно", false),
        }
    };
}

public class Question
{
    public string question;
    public Answer[] answers;

    public Question(string inputQuestion, Answer[] inputAnswers)
    {
        question = inputQuestion;
        answers = inputAnswers;
    }

    public Question(string inputQuestion, string answer1, bool isCorrect1, string answer2, bool isCorrect2)
    {
        question = inputQuestion;
        answers = new Answer[] { new Answer(answer1, isCorrect1), new Answer(answer2, isCorrect2)};
    }

    public Question(string inputQuestion, string answer1, bool isCorrect1, string answer2, bool isCorrect2,
        string answer3, bool isCorrect3)
    {
        question = inputQuestion;
        answers = new Answer[] { new Answer(answer1, isCorrect1), new Answer(answer2, isCorrect2),
        new Answer(answer3, isCorrect3)};
    }

    public Question(string inputQuestion, string answer1, bool isCorrect1, string answer2, bool isCorrect2,
        string answer3, bool isCorrect3, string answer4, bool isCorrect4)
    {
        question = inputQuestion;
        answers = new Answer[] { new Answer(answer1, isCorrect1), new Answer(answer2, isCorrect2),
        new Answer(answer3, isCorrect3), new Answer(answer4, isCorrect4) };
    }

    public Question(string inputQuestion, string answer1, bool isCorrect1, string answer2, bool isCorrect2,
        string answer3, bool isCorrect3, string answer4, bool isCorrect4, string answer5, bool isCorrect5)
    {
        question = inputQuestion;
        answers = new Answer[] { new Answer(answer1, isCorrect1), new Answer(answer2, isCorrect2),
        new Answer(answer3, isCorrect3), new Answer(answer4, isCorrect4), new Answer(answer5, isCorrect5) };
    }

    public Question(string inputQuestion, string answer1, bool isCorrect1, string answer2, bool isCorrect2,
        string answer3, bool isCorrect3, string answer4, bool isCorrect4, string answer5, bool isCorrect5,
        string answer6, bool isCorrect6)
    {
        question = inputQuestion;
        answers = new Answer[] { new Answer(answer1, isCorrect1), new Answer(answer2, isCorrect2),
        new Answer(answer3, isCorrect3), new Answer(answer4, isCorrect4), new Answer(answer5, isCorrect5),
        new Answer(answer6, isCorrect6) };
    }
}

public class Answer
    {
        public string answerText;
        public bool isCorrect;

        public Answer(string inputAnswerText, bool inputIsCorrect)
        {
            answerText = inputAnswerText;
            isCorrect = inputIsCorrect;
        }
    }
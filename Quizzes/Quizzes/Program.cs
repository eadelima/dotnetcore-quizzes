using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Quizzes
{
    class Program
    {
        static QuizDataProvider data = QuizDataProvider.Instance;
        static void Main(string[] args)
        {
            Init();
        }
        static void Init()
        {
            Stopwatch watch = new Stopwatch();

            List<MultipleChoiceQuestion> multipleChoice = new List<MultipleChoiceQuestion>();

            var questions = data.GetDataQuestions();

            Quiz quiz = new Quiz();

            foreach (var question in questions)
            {
                multipleChoice.Add(new MultipleChoiceQuestion(question.Key, GetChoice(question.Value)));
            }

            for (int i = 0; i < multipleChoice.Count; i++)
            {
                quiz.Questions.Add(i + 1, multipleChoice[i]);
            }

            QuestionShuffle.Execute(quiz.Questions);

            //Record how much time
            watch.Start();

            foreach (var question in quiz.Questions)
            {
                Console.WriteLine($"{question.Key.ToString()}. {question.Value.Text}");

                foreach (var choice in question.Value.Choices)
                {
                    Console.WriteLine($"{choice.Key}. {choice.Value.Trim()}");
                }
                Console.WriteLine("");

                Console.WriteLine(question.Value.GetInstructions());

                var answer = Console.ReadLine(); ;
                quiz.SetUserAnswer(question.Key, answer);

                Console.WriteLine("");
            }
            //Stop Recording time
            watch.Stop();

            var results = quiz.GetResults();
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            float miliToSec = watch.ElapsedMilliseconds / 1000;
            Console.WriteLine("Total time: {0}s", miliToSec);
            Console.ReadLine();
        }
        static string sortChoice(string choices)
        {
            List<string> sortChoice = new List<string>();
            string[] choice = choices.Split(',');
            string sort = "";

            foreach (var item in choice)
            {
                sortChoice.Add(item);
            }
            QuestionShuffle.Execute(sortChoice);
            foreach (var item in sortChoice)
            {
                sort += $"{item}, ";
            }

            return sort.Substring(0, sort.Length - 2);

        }
        static string GetChoice(string correctChoice, int qnt = 3)
        {

            var answers = data.GetDataQuestions();
            string choices = $"{correctChoice}*";
            List<string> list = new List<string>();

            foreach (var item in answers)
            {
                list.Add(item.Value);
            }

            for (int i = 0; i < qnt; i++)
            {
                int randomChoice = QuestionShuffle.GetNumber(list.Count);

                if (!choices.Contains(list[randomChoice]) && choices.Count(c => c == ',') < (qnt - 1))
                {
                    choices += $", {list[randomChoice]}";
                }

            }
            return sortChoice(choices);
        }
    }
}

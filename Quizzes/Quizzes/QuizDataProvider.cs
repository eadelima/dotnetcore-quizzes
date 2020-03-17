using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Quizzes
{
    public class QuizDataProvider
    {
        private Dictionary<string, string> _questionsData = new Dictionary<string, string>();
        private QuizDataProvider()
        {
            var elements = File.ReadAllLines("Data/palavras.txt");

            for (int i = 0; i < elements.Length; i += 2)
            {
                _questionsData.Add(elements[i].Trim(), elements[i + 1].Trim());
            }
        }
        public Dictionary<string, string> GetDataQuestions()
        {
            return _questionsData;
        }

        private static Lazy<QuizDataProvider> instance = new Lazy<QuizDataProvider>(() => new QuizDataProvider());

        public static QuizDataProvider Instance => instance.Value;
    }
}

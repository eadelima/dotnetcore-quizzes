using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Quizzes
{
    public class Quiz
    {
        private Regex rgx = new Regex("[^a-zA-Z]");

        private Dictionary<int, string> userAnswers = new Dictionary<int, string>();
        public Dictionary<int, Question> Questions { get; set; } = new Dictionary<int, Question>();

        public void SetUserAnswer(int questionNumber, string answerLetters)
        {
            string answerText = "";
            answerLetters = rgx.Replace(answerLetters, "");

            foreach (var letter in answerLetters)
            {
                var choiceText = Questions[questionNumber].Choices[letter.ToString()];
                answerText += String.IsNullOrEmpty(answerText) ? choiceText : "," + choiceText;
                userAnswers[questionNumber] = answerText;
            }
        }
        public List<string> GetResults()
        {
            int correctAnswers = 0;
            var results = new List<string>();

            for (int i = 1; i <= userAnswers.Count; i++)
            {
                if (userAnswers[i] == Questions[i].CorrectAnswer)
                {
                    results.Add($"{i}. \"{userAnswers[i]}\" was correct!");
                    correctAnswers++;
                }
                else
                {
                    results.Add($"{i}. Sorry, \"{Questions[i].CorrectAnswer}\" was the correct answer.");
                }
            }

            results.Add($"You got {correctAnswers} of {Questions.Count} correct.");
            return results;
        }
    }
}

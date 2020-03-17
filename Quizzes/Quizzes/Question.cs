using System;
using System.Collections.Generic;
using System.Text;

namespace Quizzes
{
    public abstract class Question
    {
        protected const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

        public string Text { get; set; }
        public Dictionary<string, string> Choices { get; set; } = new Dictionary<string, string>();
        public string CorrectAnswer { get; set; }
        public Question(string text)
        {
            this.Text = text;
        }
        protected void SetChoices(string choices)
        {
            string[] choice = choices.Split(',');

            for (int i = 0; i < choice.Length; i++)
            {
                choice[i] = SetCorrectAnswer(choice[i]);
                var letter = Alphabet[i].ToString();
                Choices[letter] = choice[i];
            }

            if (String.IsNullOrEmpty(CorrectAnswer))
            {
                throw new ArgumentException("You did not provide a correct answer.  Please mark the correct answer with an apostrophe.");
            }
        }
        private string SetCorrectAnswer(string choice)
        {
            if (choice.Contains('*'))
            {
                choice = choice.Replace("*", "");
                CorrectAnswer += String.IsNullOrEmpty(CorrectAnswer) ? choice : "," + choice;
            }
            return choice;
        }
        public abstract string GetInstructions();
    }
}

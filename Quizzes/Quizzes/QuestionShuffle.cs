using System;
using System.Collections.Generic;
using System.Text;

namespace Quizzes
{
    public static class QuestionShuffle
    {
        public static void Execute<T>(this List<T> list)
        {
            Random random = new Random();
            int size = list.Count;
            while (size > 1)
            {
                size--;
                int aux = random.Next(size + 1);
                T value = list[aux];
                list[aux] = list[size];
                list[size] = value;
            }
        }
        public static void Execute<T>(this Dictionary<int, T> list)
        {
            Random random = new Random();
            int size = list.Count;
            while (size > 1)
            {
                size--;
                int aux = random.Next(1, size);
                T value = list[aux];
                list[aux] = list[size];
                list[size] = value;
            }
        }
        public static int GetNumber(int lim)
        {
            Random random = new Random();

            return random.Next(0, lim);

        }
    }
}

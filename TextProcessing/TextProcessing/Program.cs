using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            //Concordance concordance = new Concordance(text.parcedText);
            //concordance.AnalyzeText(10);
            //
            //
            //
            //
            bool escape = false;
            int length;

            while (!escape)
            {

                Console.Clear();
                Console.WriteLine("1. Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них.");
                Console.WriteLine("2. Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.");
                Console.WriteLine("3. Из текста удалить все слова заданной длины, начинающиеся на согласную букву.");
                Console.WriteLine("4. В некотором предложении текста слова заданной длины заменить указанной подстрокой, длина которой может не совпадать с длиной слова.");
                Console.WriteLine("5. Конкорданс.");
                Console.WriteLine("6. Выход");
                Console.Write("\n\nВыберите задание:");
                int.TryParse(Console.ReadLine(), out int taskNum);

                TextProcessing text = new TextProcessing();

                switch (taskNum)
                {
                    case 1:

                        text.SortSentences();
                        break;
                    case 2:
                        
                        Console.Write("Введите длину слова для поиска:" );
                        int.TryParse(Console.ReadLine(), out length);
                        text.VoprosSentences(length);
                        break;
                    case 3:
                        Console.Write("Введите длину слова для поиска:");
                        int.TryParse(Console.ReadLine(), out length);
                        text.RemoveWordStartedWithConsonant(length);
                        break;
                    case 4:
                        Console.Write("Введите номер предложения:");
                        int.TryParse(Console.ReadLine(), out int numOfSent);
                        Console.Write("Введите длину слова для поиска:");
                        int.TryParse(Console.ReadLine(), out length);
                        text.ReplaceWordInSentence(numOfSent, length, Console.ReadLine());
                        break;
                    case 5:
                        Console.Write("Введите количество строк в странице:");
                        int.TryParse(Console.ReadLine(), out length);
                        Concordance concordance = new Concordance(text.parcedText);
                        concordance.AnalyzeText(length);
                        break;
                    case 6:
                        escape = true;
                        break;
                    default:
                        Console.WriteLine("Такого действия нет. Выберите задание снова. ");
                        break;

                }
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                Console.ReadKey();
            }
        }

    }
}

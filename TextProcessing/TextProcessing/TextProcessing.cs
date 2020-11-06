using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

                //1. Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них.
                //2. Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.
                //3. Из текста удалить все слова заданной длины, начинающиеся на согласную букву.");
                //4. В некотором предложении текста слова заданной длины заменить указанной подстрокой, длина которой может не совпадать с длиной слова.

namespace TextProcessing
{
    class TextProcessing
    {
        public List<Sentence> parcedText { get; set; }
        const string fileText = "TextRus.txt";
        const string fileTextProcessing = "FileForTasks(1-4).txt";
        public string text { get; }
        public TextProcessing()
        {
            using (StreamReader sr = new StreamReader(fileText, Encoding.Default))
            {
                text = sr.ReadToEnd();
                text = ReplaceTabulationSpacesByOneSpace(text);
            }
            TextParcer();
        }


        void TextParcer()
        {
            List<Symbol> allSymbols = new List<Symbol>();
            List<Word> allWords = new List<Word>();
            List<Sentence> allSentences = new List<Sentence>();
            int startWordIndex = 0, startSentIndex = 0, countWords = 0, countSent=0;
            for (int i = 0; i < text.Length; i++)
            {
                Symbol symbol = new Symbol(text[i]);
                allSymbols.Add(symbol);

                if (symbol.punctuation)
                {
                    if (symbol.endOfSentence)
                    {
                        Word word = new Word(allSymbols, startWordIndex, i + 1);
                        startWordIndex = i + 1;
                        allWords.Add(word);
                        countWords++;
                        if (word.word.Count == 1)
                        {
                            if (word.word[0].symbol == '.'|| word.word[0].symbol == '\n'|| word.word[0].symbol == '\r'|| word.word[0].symbol == ' ')
                            {
                                allSentences[countSent-1].sentence.Add(word);
                                startSentIndex++;
                                countWords--;
                            }
                            else
                            {
                                Sentence sentence = new Sentence(allWords, startSentIndex, countWords);
                                countSent++;
                                startSentIndex = countWords + startSentIndex;
                                countWords = 0;
                                allSentences.Add(sentence);
                            }
                        }
                        else
                        {
                            Sentence sentence = new Sentence(allWords, startSentIndex, countWords);
                            countSent++;
                            startSentIndex = countWords + startSentIndex;
                            countWords = 0;
                            allSentences.Add(sentence);
                        }

                    }
                    else
                    {
                        Word word = new Word(allSymbols, startWordIndex, i+1);
                        startWordIndex = i+1;
                        allWords.Add(word);
                        countWords++;
                    }


                }
            }
            parcedText = new List<Sentence>(allSentences);
        }
        public void SortSentences()
        {
            List<Sentence> allSentences = new List<Sentence>(parcedText);
            int[] lengthOfSentences = new int[allSentences.Count];
            int[] numOfSentences = new int[allSentences.Count];
            for (int i=0; i<allSentences.Count; i++)
            {
                lengthOfSentences[i] = allSentences[i].Length();
                numOfSentences[i] = i;
            }

            for (int i = 0; i < allSentences.Count - 1; i++)
            {
                for (int j = i + 1; j < allSentences.Count; j++)
                {
                    if (lengthOfSentences[i] > lengthOfSentences[j])
                    {
                        Swap(ref lengthOfSentences[i], ref lengthOfSentences[j]);
                        Swap(ref numOfSentences[i], ref numOfSentences[j]);
                    }
                }
            }
            using (StreamWriter sw=new StreamWriter(File.Open(fileTextProcessing, FileMode.Create)))
            {
                for(int i=0; i<allSentences.Count; i++)
                {
                    string sent = allSentences[numOfSentences[i]].WriteSentence();
                    sw.Write(sent+"\r\n");
                }
                
            }
        }
        public void VoprosSentences(int length)
        {
            List<Sentence> voprosSentences = new List<Sentence>();
            foreach(Sentence sentence in parcedText)
            {
                string lastWord = sentence.sentence[sentence.sentence.Count-1].ToString();
                
                if (lastWord[lastWord.Length-1]=='?')
                {
                    foreach(Word word in sentence.sentence)
                    {
                        if (word.Length() == length)
                        {
                            foreach (Symbol symbol in new List<Symbol>(word.WordWitoutPunctuators()))
                            {
                                symbol.Print();
                            }
                            Console.WriteLine();
                        }
                    }
                }

            }
        }
        public void RemoveWordStartedWithConsonant(int length)
        {
            List<Sentence> allSentences = new List<Sentence>(parcedText);
            string consonants = "бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩbcdfghjklmnqrstvwxzBCDFGHJKLMNPQRSTVWXZ";

            for (int i = 0; i < allSentences.Count; i++)
            {
                for (int j = 0; j < allSentences[i].sentence.Count; j++)
                {
                    if (allSentences[i].sentence[j].Length() == length)
                    {
                        for (int k = 0; k < consonants.Length; k++)
                        {
                            if (allSentences[i].sentence[j].word[0].symbol == consonants[k])
                            {

                                parcedText[i].sentence[j].RemoveWord();

                                break;
                            }
                        }
                        
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(File.Open(fileTextProcessing, FileMode.Create)))
            {
                foreach(Sentence sentence in allSentences)
                {
                    sw.Write(sentence.WriteSentence());
                }

            }

        }
        public void ReplaceWordInSentence(int numOfSent, int length, string newWord)
        {
            List<Sentence> allSentences = new List<Sentence>(parcedText);


            for (int i = 0; i < allSentences[numOfSent].sentence.Count; i++)
            {
                if (allSentences[numOfSent].sentence[i].Length() == length)
                {
                    allSentences[numOfSent].sentence[i].ReplaceWord(newWord);
                }
            }
            using (StreamWriter sw = new StreamWriter(File.Open(fileTextProcessing, FileMode.Create)))
            {
                foreach (Sentence sentence in allSentences)
                {
                    sw.Write(sentence.WriteSentence());
                }
            }
        }

        void Swap(ref int item1, ref int item2)
        {
            var temp = item1;
            item1 = item2;
            item2 = temp;
        }
        string ReplaceTabulationSpacesByOneSpace(string tempo)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            tempo = regex.Replace(tempo, " ");
            return tempo;
        }
    }
}

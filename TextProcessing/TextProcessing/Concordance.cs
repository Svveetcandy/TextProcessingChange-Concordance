using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextProcessing
{
    class Concordance
    {
        const string fileConcordance = "Corcondance.txt";
        List<Sentence> parcedTextForConcordance;
        public Concordance(List<Sentence>text)
        {
            parcedTextForConcordance = new List<Sentence>(AllWordsWithoutPunctuatorsAndToLower(text));
        }
        public void AnalyzeText(int linesInPage)
        {
            List<string> allWords = new List<string>();
            List<string> wordsWithoutDublicates = new List<string>();
            List<int> locationOfAllWords=new List<int>();
            List<int> locationOfWord;
            int numOfLine=0, numOfPage=1, count=0;
            string word;
            for (int i = 0; i < parcedTextForConcordance.Count; i++)
            {
                for (int j = 0; j < parcedTextForConcordance[i].sentence.Count; j++) {
                    word = parcedTextForConcordance[i].sentence[j].ToString();
                    if (word != "")
                    {
                        allWords.Add(word);
                    }
                }
            }
            int qwercount = 0;
            for (int i=0; i<allWords.Count; i++)
            {
                if (allWords[i] == "\n")
                {
                    numOfLine++;
                    count = 0;
                    if (numOfLine == linesInPage)
                    {
                        numOfPage++;
                        numOfLine = 0;
                    }
                    allWords.RemoveAt(i);
                    i--;
                }
                else
                {
                    locationOfAllWords.Add(numOfPage);                                   //     Прикрепление к слову его местонахождения
                    count = count + allWords[i].Length;
                    if (count >= 1024)
                    {
                        numOfPage++;
                        count = 0;
                    }
                }

            }

            int bufNum;

            for (int i = 0; i < allWords.Count - 1; i++)
            {
                for (int j = i + 1; j < allWords.Count; j++)
                {

                    if (string.Compare(allWords[i], allWords[j]) == 1)//wordsConcordanceFinal[i] > wordsConcordanceFinal[j])
                    {

                        word = allWords[i];


                        allWords[i] = allWords[j];
                        allWords[j] = word;

                        bufNum = locationOfAllWords[i];
                        locationOfAllWords[i] = locationOfAllWords[j];
                        locationOfAllWords[j] = bufNum;
                    }
                }
            }


            char firstLetter = 'а';
            bool isNewLetter = false, isFirstEntry = true;
            while(allWords.Count!=0)
            {
                word = allWords[0];
                if (word == "я")
                {
                    qwercount++;
                }
                locationOfWord = new List<int>(SearchForDuplicateWords(word, allWords, locationOfAllWords));
                
                if (isFirstEntry)
                {
                    WriteConcordanceWord(word, false, locationOfWord, isFirstEntry);
                    isFirstEntry = false;
                }
                else
                {
                    if (word[0] != firstLetter)
                    {
                        isNewLetter = true;
                        firstLetter = word[0];
                    }
                    WriteConcordanceWord(word, isNewLetter, locationOfWord, isFirstEntry);
                    isNewLetter = false;
                }

            }


        }
        List<Sentence> AllWordsWithoutPunctuatorsAndToLower(List<Sentence> text)
        {
            List<Sentence> newText = new List<Sentence>();
            for(int i=0; i<text.Count; i++)
            {
                for(int j=0; j<text[i].sentence.Count; j++)
                {
                    if(text[i].sentence[j].Length()!= text[i].sentence[j].word.Count)
                    {
                        text[i].sentence[j].word=text[i].sentence[j].WordWitoutPunctuators();
                    }
                    text[i].sentence[j].ToLower();
                }
                newText.Add(text[i]);
            }
            return newText;
        }

        List<int> SearchForDuplicateWords(string word, List<string> listOfAllWords, List<int> location)
        {
            int index;
            List<int> numOfHitsThisWord = new List<int>();
            while (listOfAllWords.Contains(word))
            {
                index = listOfAllWords.IndexOf(word);
                numOfHitsThisWord.Add(location[index]);
                listOfAllWords.RemoveAt(index);
                location.RemoveAt(index);
            }
            return numOfHitsThisWord;
        }
        void WriteConcordanceWord(string word, bool isNewLetter, List<int> location, bool isFirstEntry)
        {
            
            if (isFirstEntry)
            {
                using (StreamWriter sw = new StreamWriter(File.Open(fileConcordance, FileMode.Create)))
                {
                    sw.Write(word[0].ToString().ToUpper()+"\r\n\r\n");
                    sw.Write(word + "...................................." + location.Count + ":" );
                    location = new List<int>(location.Distinct());
                    location.Sort();
                    for (int i = 0; i < location.Count; i++)
                    {
                        sw.Write(location[i] + " ");
                    }
                    sw.Write("\r\n\r\n");
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(File.Open(fileConcordance, FileMode.Append)))
                {

                    if (isNewLetter)
                    {
                        sw.Write(word[0].ToString().ToUpper() + "\r\n\r\n");
                    }
                    sw.Write(word + "...................................." + location.Count + ":");
                    location = new List<int>(location.Distinct());
                    location.Sort();
                    for (int i = 0; i < location.Count; i++)
                    {
                        sw.Write(location[i] + " ");
                    }
                    sw.Write("\r\n\r\n");
                }

            }
        }

    }
}

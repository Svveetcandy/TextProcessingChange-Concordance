using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    class Sentence
    {
        public List<Word> sentence { get; set; } = new List<Word>();
        public Sentence(List<Word> allWords, int startIndex, int count)
        {
            for (int i = startIndex; i < count+startIndex; i++)
            {
                sentence.Add(allWords[i]);
            }
        }
        public void Print()
        {
            for(int i=0; i< sentence.Count; i++)
            {
                sentence[i].Print();
            }
        }
        public int Length()
        {
            int count=0;
            foreach(Word word in sentence)
            {
                if (word.Length() > 0)
                {
                    count++;
                }
            }
            return count;
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for(int i=0; i<sentence.Count; i++)
            {
                str.Append(sentence[i].ToString());
            }
            return str.ToString();
        }
        public string WriteSentence()
        {
            string str = ToString();
            return str;
        }

    }
}

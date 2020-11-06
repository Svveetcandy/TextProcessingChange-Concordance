using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;

namespace TextProcessing
{
    class Word
    {
        public List<Symbol> word { get; set; } = new List<Symbol>();
        public bool endOfLine { get; } = false;
        public Word(List<Symbol> allSymbols, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                word.Add(allSymbols[i]);
                if (allSymbols[i].endOfLine) endOfLine = true;
            }
        }
        public void Print()
        {
            for (int i = 0; i < word.Count; i++)
            {
                word[i].Print();
            }
        }
        public int Length()
        {
            int count = 0;
            foreach (Symbol symbol in word)
            {
                if (!symbol.punctuation)
                {
                    count++;
                }
            }
            return count;
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < word.Count; i++)
            {
                str = str.Append(word[i].WriteSymbol());
            }
            return str.ToString();
        }
        public string WriteWord()
        {
            string wordToWrite = ToString();
            return wordToWrite;
        }
        public void RemoveWord()
        {
            for (int i = 0; i < word.Count; i++)
            {
                if (!word[i].punctuation)
                {
                    word.Remove(word[i]);
                    i--;
                }
            }
            return;
        }
        public void ReplaceWord(string newWord)
        {
            List<Symbol> ReplacedWord = new List<Symbol>();
            RemoveWord();
            foreach (char symbol in newWord)
            {
                ReplacedWord.Add(new Symbol(symbol));
            }
            foreach (Symbol symbol in word)
            {
                ReplacedWord.Add(symbol);
            }
            word.Clear();
            foreach (Symbol symbol in ReplacedWord)
            {
                word.Add(symbol);
            }
            return;
        }
        public List<Symbol> WordWitoutPunctuators()
        {
            List<Symbol> newWord = new List<Symbol>();
            foreach(Symbol symbol in word)
            {
                if (!symbol.punctuation || symbol.symbol=='\n')
                {
                    newWord.Add(symbol);
                }
            }
            return newWord;
        }
        public List<Symbol> ToLower()
        {
            //A-Z(65-90), А-Я(1040-1071)+Ё(1025)
            int intChar;
            List<Symbol> newWord = new List<Symbol>(word);
            foreach(Symbol symbol in newWord)
            {
                intChar = (int)symbol.symbol;
                if ((intChar >= 1040 && intChar <= 1071) || (intChar >= 65 && intChar <= 90))
                {
                    intChar = intChar + 32;
                }
                if (intChar == 1025)
                {
                    intChar = 1105;
                }
                symbol.symbol = (char)intChar;
            }
            return newWord;
        }
    }
}

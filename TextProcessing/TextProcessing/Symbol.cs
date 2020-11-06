using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Linq;

namespace TextProcessing
{
    class Symbol
    {
        public char symbol { get; set; }
        public bool punctuation { get; } = false;
        public bool endOfSentence { get; } = false;
        public bool endOfLine { get; } = false;
        public Symbol(char symb)
        {
            if (symb == '!' || symb == '?' || symb == '.' || symb == '(' || symb== ')' || symb== ',' || symb== ':' || symb== '—' || symb== ';' || symb== '"' || symb== ' ' || symb== '\n' || symb == '\r')
            {
                if (symb == '.' || symb == '!' || symb == '?' || symb == '\n' || symb == '\r')
                {
                    if (symb == '\n') endOfLine = true;
                    endOfSentence = true;
                }
                punctuation = true;
            }
            symbol = symb;
        }
        public void Print()
        {
            Console.Write(symbol);
        }
        public char WriteSymbol()
        {
            return symbol;
        }
    }
}
// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.IO;
using DosBox.Interfaces;

namespace DosBox.Console
{
    /// <summary>
    /// Implements the outputter interface that all text is sent to
    /// the console (System.out).
    /// </summary>
    public class ConsoleOutputter : IOutputter
    {
        private int numberOfPrintedCharacters = 0;

        #region IOutputter Members

        public virtual void NewLine()
        {
            System.Console.WriteLine(string.Empty);
        }

        public virtual void Print(string text)
        {
            System.Console.Write(text);
            AnalyzePrintedCharacters(text);
        }

        public virtual void PrintLine(string line)
        {
            System.Console.WriteLine(line);
            AnalyzePrintedCharacters(line);
        }

        public virtual char ReadSingleCharacter()
        {
            int input = 0;
            int readCharacter = 0;

            try
            {
                while (input != '\n')
                {
                    // do not consider \r and \n
                    if (input != '\n' && input != '\r')
                    {
                        readCharacter = input;
                    }

                    input = System.Console.Read();
                }
            }
            catch (IOException)
            {
                readCharacter = 0;
            }

            return (char) readCharacter;
        }

        public int NumberOfCharactersPrinted()
        {
            return numberOfPrintedCharacters;
        }

        public bool HasCharactersPrinted()
        {
            return numberOfPrintedCharacters > 0;
        }

        public void ResetStatistics()
        {
            numberOfPrintedCharacters = 0;
        }

        #endregion

        protected void AnalyzePrintedCharacters(string printedString)
        {
            string tempString = printedString.Trim();
            numberOfPrintedCharacters += tempString.Length;
        }
    }
}
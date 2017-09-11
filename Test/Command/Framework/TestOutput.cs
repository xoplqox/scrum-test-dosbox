// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.Text;
using DosBox.Console;

namespace DosBoxTest.Command.Framework
{
    /// <summary>
    /// Implements Outputter interface for testing purpose.
    /// Offers:
    /// <li/> buffering output for later investigation
    /// <li/> simulating reading characters from console
    /// </summary>
    public class TestOutput : ConsoleOutputter
    {
        private char characterThatIsRead = (char) 0;
        private StringBuilder output;

        public TestOutput()
        {
            output = new StringBuilder();
        }

        #region IOutputter Members

        public override void NewLine()
        {
            output.Append("\n");
        }

        public override void Print(string text)
        {
            output.Append(text);
            AnalyzePrintedCharacters(text);
        }

        public override void PrintLine(string line)
        {
            output.Append(line);
            NewLine();
            AnalyzePrintedCharacters(line);
        }

        /// <summary>
        /// Simulates reading a character from console.
        /// Returns 0 if setCharacterThatIsRead() is not called previously.
        /// 
        /// Usage in Unit Tests:
        /// TestOutput testOutput.setCharacterThatIsRead('Y');
        /// this.commandInvoker.ExecuteCommand("copy C:\\WinWord.exe C:\\ProgramFiles\\", testOutput);
        /// TestCase.assertTrue(testOutput.ToString().toLowerCase().contains("overwrite") == true);
        /// TestCase.assertTrue(testOutput.characterWasRead() == true);
        /// </summary>
        public override char ReadSingleCharacter()
        {
            return characterThatIsRead;
        }

        #endregion

        /// <summary>
        /// Empties the buffered output. Important to call before starting a new test.
        /// </summary>
        public void Empty()
        {
            output = new StringBuilder();
        }

        /// <summary>
        /// Returns the buffered output.
        /// </summary>
        public override string ToString()
        {
            return output.ToString();
        }

        /// <summary>
        /// Sets the character that is read when calling readSingleCharacter().
        /// </summary>
        /// <param name="character">Character that is read.</param>
        public void setCharacterThatIsRead(char character)
        {
            characterThatIsRead = character;
        }
    }
}
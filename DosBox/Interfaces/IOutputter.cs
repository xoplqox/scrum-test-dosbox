// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

namespace DosBox.Interfaces
{
    /// <summary>
    /// This interface needs to be implemented by a class that outputs the passed
    /// strings to either a console, a TCP/IP-Port, to a file, etc.
    /// Currently, output to a console is supported only.
    /// This interface is used to inverse the dependency from invoker to commands. With
    /// this interface, the commands do not need to know a particular invoker to output
    /// strings.
    /// </summary>
    public interface IOutputter
    {
        /// <summary>
        /// Outputs the string and adds a newline at the end.
        /// If a newline is already passed, two newlines are output.
        /// </summary>
        /// <param name="line">string to output. No ending newline character required.</param>
        void PrintLine(string line);

        /// <summary>
        /// Outputs the string. Does not add a newline at the end.
        /// </summary>
        /// <param name="text">text string to output.</param>
        void Print(string text);

        /// <summary>
        /// Outputs a newline.
        /// </summary>
        void NewLine();

        /// <summary>
        /// Reads a single character from the channel.
        /// May be used to ask the user Yes/No questions.
        /// Note: This function does not return until user entered a character.
        /// It may be that calling this function causes a deadlock!
        /// </summary>
        /// <returns>character read</returns>
        char ReadSingleCharacter();

        /// <summary>
        /// Returns the numbers of characters printed since last ResetStatistics().
        /// Newlines and spaces do not count.
        /// </summary>
        int NumberOfCharactersPrinted();

        /// <summary>
        /// Returns whether characters where printed since last ResetStatistics().
        /// Newlines and spaces are not taken into account.
        /// </summary>
        /// <returns></returns>
        bool HasCharactersPrinted();

        /// <summary>
        /// Resets the character counter.
        /// </summary>
        void ResetStatistics();
    }
}
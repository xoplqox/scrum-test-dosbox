// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using DosBox.Filesystem;
using DosBox.Interfaces;
using DosBox.Invoker;
using DosBoxTest.Command.Framework;

namespace DosBoxTest.Command.Library
{
    public abstract class CmdTest
    {
        protected CommandInvoker commandInvoker;
        protected IDrive drive;
        protected File file1InDir1;
        protected File file2InDir1;
        protected File fileInRoot1;
        protected File fileInRoot2;
        protected int numbersOfDirectoriesBeforeTest;
        protected int numbersOfFilesBeforeTest;
        protected Directory rootDir;
        protected Directory subDir1;
        protected Directory subDir2;
        protected TestOutput testOutput = new TestOutput();

        ///<summary>
        /// Sets up a directory structure as follows:
        /// C:\
        /// |---FileInRoot1 ("an entry")
        /// |---FileInRoot2 ("a long entry in a file")
        /// |---subDir1
        /// |   |---File1InDir1 (empty)
        /// |   |---File2InDir1 (empty)
        /// |---subdir2
        /// </summary>
        public void CreateTestFileStructure()
        {
            drive = new Drive("C");
            rootDir = drive.RootDirectory;
            fileInRoot1 = new File("FileInRoot1", "an entry");
            rootDir.Add(fileInRoot1);
            fileInRoot2 = new File("FileInRoot2", "a long entry in a file");
            rootDir.Add(fileInRoot2);

            subDir1 = new Directory("subDir1");
            rootDir.Add(subDir1);
            file1InDir1 = new File("File1InDir1", "");
            subDir1.Add(file1InDir1);
            file2InDir1 = new File("File2InDir1", "");
            subDir1.Add(file2InDir1);

            subDir2 = new Directory("subDir2");
            rootDir.Add(subDir2);

            commandInvoker = new CommandInvoker();
            testOutput = new TestOutput();

            numbersOfDirectoriesBeforeTest = drive.RootDirectory.GetNumberOfContainedDirectories();
            numbersOfFilesBeforeTest = drive.RootDirectory.GetNumberOfContainedFiles();
        }

        protected void ExecuteCommand(string commandLine)
        {
            if (this.commandInvoker == null)
                this.commandInvoker = new CommandInvoker();
            this.commandInvoker.ExecuteCommand(commandLine, testOutput);
        }
    }
}
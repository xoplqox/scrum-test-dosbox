// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using System.Collections.Generic;

namespace DosBox.Filesystem
{
    /// <summary>
    /// This class implements the behavior of concrete files. 
    /// Composite-Pattern: Leaf
    /// 
    /// Responsibilities:
    /// - represents leaf objects in the composition. A leaf has no children. 
    /// - defines behavior for primitive objects in the composition. 
    /// </summary>
    public class File : FileSystemItem
    {
        private readonly string fileContent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemName">A name for the file. Note that file names may not contain '\' '/' ':' ',' ';' and ' '.</param>
        /// <param name="fileContent">
        /// Any string which represents the content of the file.
        /// The content may not contain characters like ',' and ';'.
        /// </param>
        public File(string itemName, string fileContent)
            : base(itemName, null)
        {
            this.fileContent = fileContent;
        }

        public string FileContent
        {
            get { return fileContent; }
        }

        public override IEnumerable<FileSystemItem> Content
        {
            get { throw new InvalidOperationException("File.Content called. Check first with IsDirectory()"); }
        }

        public override bool IsDirectory()
        {
            return false;
        }

        public override int GetNumberOfContainedFiles()
        {
            return 0; // A file does not contain any other files
        }

        public override int GetNumberOfContainedDirectories()
        {
            return 0; // A file does not contain any sub-directories
        }

        public override int GetSize()
        {
            return fileContent.Length;
        }
    }
}
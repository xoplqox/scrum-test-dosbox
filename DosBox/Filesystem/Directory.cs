// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.Collections.Generic;
using System.Linq;

namespace DosBox.Filesystem
{
    /// <summary>
    /// This class implements the behavior of concrete directories.
    /// Composite-Pattern: Composite
    ///
    /// Responsibilities:
    /// - defines behavior for components (directories) having children. 
    /// - stores child components (files and subdirectories). 
    /// - implements child-related operations in the Component interface. These are:
    ///   - getContent()
    ///   - add(Directory), add(File)
    ///   - getNumberOfFiles(), getNumberOfDirectories()
    /// </summary>
    public class Directory : FileSystemItem
    {
        private readonly List<FileSystemItem> content;

        public Directory(string name)
            : base(name, null)
        {
            content = new List<FileSystemItem>();
        }

        public override IEnumerable<FileSystemItem> Content
        {
            get { return content; }
        }

        public override bool IsDirectory()
        {
            return true;
        }

        /// <summary>
        /// Adds a new subdirectory to this directory.
        /// If the directory to add is already part of another directory structure,
        /// it is removed from there.
        /// </summary>
        public void Add(Directory directoryToAdd)
        {
            content.Add(directoryToAdd);
            if (HasAnotherParent(directoryToAdd))
            {
                RemoveParent(directoryToAdd);
            }

            directoryToAdd.Parent = this;
        }

        public void Add(File fileToAdd)
        {
            content.Add(fileToAdd);
            if (fileToAdd.Parent != null)
            {
                fileToAdd.Parent.content.Remove(fileToAdd);
            }

            fileToAdd.Parent = this;
        }

        private static bool RemoveParent(FileSystemItem item)
        {
            return item.Parent.content.Remove(item);
        }

        private static bool HasAnotherParent(FileSystemItem item)
        {
            return item.Parent != null;
        }

        /// <summary>
        /// Removes a directory or a file from current directory.
        /// Sets the parent of the removed item to null, if was contained in this directory.
        /// </summary>
        public void Remove(FileSystemItem item)
        {
            if (content.Contains(item))
            {
                item.Parent = null;
                content.Remove(item);
            }
        }

        public override int GetNumberOfContainedFiles()
        {
            return content.Count(item => !item.IsDirectory());
        }

        public override int GetNumberOfContainedDirectories()
        {
            return content.Count(item => item.IsDirectory());
        }

        public override int GetSize()
        {
            return 0;
        }
    }
}
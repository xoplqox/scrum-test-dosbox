// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using System.Collections.Generic;

namespace DosBox.Filesystem
{
    /// <summary>
    /// This class abstracts File and Directory.
    /// Composite-Pattern: Component
    /// 
    /// Responsibilities:
    /// - declares the common interface for objects in the composition. This is:
    ///    - get/setName(), tostring()
    ///    - Path
    ///    - isDirectory()
    ///    - getNumberOfFiles(), getNumberOfDirectories()
    ///    - getSize()
    /// - implements default behavior for the interface common to all classes, as appropriate. 
    /// - declares an interface for accessing and managing its child components. This is
    ///    - getContent()
    /// - defines an interface for accessing a component's parent in the recursive structure,
    ///   and implements it if that's appropriate. This is
    ///    - getParent()
    /// </summary>
    public abstract class FileSystemItem
    {
        private const string ILLEGAL_ARGUMENT_TEXT = "Error: A file or directory name may not contain '/', '\', ',', ' ' or ':'";
        private string itemName;
        private Directory parent;

        protected FileSystemItem(string itemName, Directory parent)
        {
            if (!IsFileSystemValidName(itemName))
            {
                throw new ArgumentException(ILLEGAL_ARGUMENT_TEXT);
            }
            this.itemName = itemName;
            this.parent = parent;
        }

        /// <summary>
        /// Gets or sets the item's name.
        /// New name of the item. May not contain any ':', '/', ',', ' ' or '\' in the name. 
        /// Otherwise, the name is not changed and an ArgumentException is thrown. 
        /// </summary>
        /// <value>The name of the item.</value>
        public string Name
        {
            get { return itemName; }

            set
            {
                if (!IsFileSystemValidName(value))
                {
                    throw new ArgumentException(ILLEGAL_ARGUMENT_TEXT);
                }

                itemName = value;
            }
        }

        /// <summary>
        /// Gets the full path of the item.
        /// </summary>
        /// <returns>Full path, e.g. "C:\thisdir\thatdir\file.txt"</returns>
        public string Path
        {
            get
            {
                if (parent != null)
                {
                    return parent.Path + "\\" + itemName;
                }
                else
                {
                    // For root directory
                    return itemName;
                }
            }
        }

        /// <summary>
        /// Returns the content of the item.
        /// </summary>
        /// <returns>
        /// - the list of contained files and directories if isDirectory() == true
        /// - null if isDirectory() == false
        /// </returns>
        public abstract IEnumerable<FileSystemItem> Content { get; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public Directory Parent
        {
            get { return parent; }
            internal set { parent = value; }
        }

        protected bool IsFileSystemValidName(string nameToCheck)
        {
            return !nameToCheck.Contains("\\") && !nameToCheck.Contains("/") && !nameToCheck.Contains(",") &&
                   !nameToCheck.Contains(" ");
        }

        /// <summary>
        /// Returns the full path of the item.
        /// See Path
        /// </summary>
        public override string ToString()
        {
            return Path;
        }

        public abstract bool IsDirectory();

        public abstract int GetNumberOfContainedFiles();

        /// <summary>
        /// Returns the number of directories contained by this item
        /// </summary>
        /// <returns>
        /// - number of contained directories if isDirectory() == true
        /// - 0 if isDirectory() == false</returns>
        public abstract int GetNumberOfContainedDirectories();

        /// <summary>
        /// Returns the size of the item.
        /// </summary>
        /// <returns>
        /// - the size in bytes of the file (string length of the content) if isDirectory() == false
        /// - 0 if isDirectory() == true</returns>
        public abstract int GetSize();
    }
}
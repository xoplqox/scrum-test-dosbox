// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using System.Collections.Generic;
using System.Text;
using DosBox.Interfaces;

namespace DosBox.Filesystem
{
    /// <summary>
    /// This class implements the access to the composition.
    /// Composite-Pattern: Client
    /// 
    /// Responsibilities:
    /// <li/> manipulates objects in the composition through the Component interface.
    /// <li/> owns the root directory which is the top of the directory tree.
    /// <li/> knows the current directory on which most of the commands must be performed.
    /// </summary>
    public class Drive : IDrive
    {
        private readonly string driveLetter;
        private readonly Directory rootDir;
        private Directory currentDir;

        /// <summary>
        /// Initializes a new instance of the <see cref="Drive"/> class. Creates a new drive and a root directory.
        /// </summary>
        /// <param name="driveLetter">
        /// Name of the drive. May only contain a single uppercase letter.
        /// If longer name given, only the first character is taken.
        /// </param>
        public Drive(string driveLetter)
        {
            this.driveLetter = driveLetter.Substring(0, 1);
            this.driveLetter = this.driveLetter.ToUpper();
            Label = string.Empty;
            rootDir = new Directory(this.driveLetter + ":");
            currentDir = rootDir;
        }

        /// <summary>
        /// <see cref="IDrive.RootDirectory"/>
        /// </summary>
        public Directory RootDirectory
        {
            get { return rootDir; }
        }

        /// <summary>
        /// Gets the current directory.
        /// </summary>
        public Directory CurrentDirectory
        {
            get { return currentDir; }
        }

        /// <summary>
        /// Changes the current directory
        /// </summary>
        /// <param name="dir">new current directory</param>
        /// <returns>
        /// - true if succeeded
        /// - false otherwise
        /// </returns>
 
        public bool ChangeCurrentDirectory(Directory dir)
        {
            if (GetItemFromPath(dir.Path) == dir)
            {
                currentDir = dir;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the name of the drive.
        /// </summary>
        /// <returns>Returns the drive name with a ending ':'. E.g. "C:".</returns>
        public string DriveLetter
        {
            get { return driveLetter + ":"; }
        }

        public string Prompt
        {
            get { return currentDir.Path + "> "; }
        }

        public string Label { get; set; }

        public FileSystemItem GetItemFromPath(string givenItemPath)
        {
            // Remove any "/" with "\"
            string givenItemPathPatched = givenItemPath.Replace('/', '\\');

            // Remove ending "\"
            givenItemPathPatched = givenItemPathPatched.Trim();
            if (givenItemPathPatched[givenItemPathPatched.Length - 1] == '\\'
                && givenItemPathPatched.Length >= 2)
            {
                givenItemPathPatched = givenItemPathPatched.Substring(0, givenItemPathPatched.Length - 1);
            }

            // Test for special paths
            if (givenItemPathPatched.CompareTo("\\") == 0)
            {
                return RootDirectory;
            }

            if (givenItemPathPatched.CompareTo("..") == 0)
            {
                Directory parent = CurrentDirectory.Parent ?? RootDirectory;
                return parent;
            }

            if (givenItemPathPatched.CompareTo(".") == 0)
            {
                return CurrentDirectory;
            }

            // Check for .\
            if (givenItemPathPatched.Length >= 2)
            {
                if (givenItemPathPatched.Substring(0, 2).CompareTo(".\\") == 0)
                {
                    givenItemPathPatched = givenItemPathPatched.Substring(2, givenItemPathPatched.Length - 2);
                }
            }

            // Check for ..\
            if (givenItemPathPatched.Length >= 3)
            {
                if (givenItemPathPatched.Substring(0, 3).CompareTo("..\\") == 0)
                {
                    var temp = new StringBuilder();
                    temp.Append(CurrentDirectory.Parent.Path);
                    temp.Append("\\");
                    temp.Append(givenItemPathPatched.Substring(3, givenItemPathPatched.Length - 3));
                    givenItemPathPatched = temp.ToString();
                }
            }

            // Add drive name if path starts with "\"
            if (givenItemPathPatched[0] == '\\')
            {
                givenItemPathPatched = driveLetter + ":" + givenItemPathPatched;
            }

            // Make absolute path from relative paths
            if (givenItemPathPatched.Length == 1 || givenItemPathPatched[1] != ':')
            {
                givenItemPathPatched = CurrentDirectory + "\\" + givenItemPathPatched;
            }

            // Find more complex paths recursive
            if (givenItemPathPatched.CompareTo(rootDir.Path) == 0)
            {
                return rootDir;
            }

            return GetItemFromDirectory(givenItemPathPatched, rootDir);
        }

        private FileSystemItem GetItemFromDirectory(string givenItemName, Directory directoryToLookup)
        {
            IEnumerable<FileSystemItem> content = directoryToLookup.Content;

            foreach (FileSystemItem item in content)
            {
                string pathName = item.Path;
                if (pathName.Equals(givenItemName, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }

                if (item.IsDirectory())
                {
                    FileSystemItem retVal = GetItemFromDirectory(givenItemName, (Directory)item);
                    if (retVal != null)
                    {
                        return retVal;
                    }
                }
            }

            return null;
        }

        public void Save()
        {
            //TODO: Implement
        }

        public void Restore()
        {
            //TODO: Implement
        }

        public void CreateFromRealDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return DriveLetter;
        }
    }
}
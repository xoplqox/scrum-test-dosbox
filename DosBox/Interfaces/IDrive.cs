// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using DosBox.Filesystem;

namespace DosBox.Interfaces
{
    /// <summary>
    /// Abstracts the drive (e.g. C:\)
    /// Responsibilities:
    /// - provides access to data about the drive like label, drive letter, ...
    /// - owns the root directory which is the top of the directory tree.
    /// - knows the current directory on which most of the commands must be performed.
    /// - queries drive information from a path
    /// - stores drive data persistently
    /// </summary>    
    public interface IDrive
    {
        #region Responsibility provides access to drive data
        /// <summary>
        /// Drive label, as used in command VOL and LABEL
        /// </summary>
        string Label { set; get; }

        /// <summary>
        /// E.g. "C:"
        /// </summary>
        String DriveLetter { get; }

        /// <summary>
        /// Current Directory + ">"
        /// </summary>
        String Prompt { get; }
        #endregion

        #region Responsibility owns root directory
        /// <summary>
        /// Access to the drive's root directory.
        /// </summary>
        Directory RootDirectory { get; }
        #endregion

        #region Responsibility knows current directory
        /// <summary>
        /// Access to the currently active directory on this drive.
        /// </summary>
        Directory CurrentDirectory { get; }

        /// <summary>
        /// Changes the currently active directory. New directory must be within this drive,
        /// otherwise the current directory is not changed.
        /// </summary>
        /// <param name="newCurrentDirectory"></param>
        /// <returns></returns>
        bool ChangeCurrentDirectory(Directory newCurrentDirectory);
        #endregion

        #region Responsibility queries drive information
        /// <summary>
        /// Returns the object of a given path name.
        /// 
        /// Example:
        /// getItemFromPath("C:\\temp\\aFile.txt");
        /// Returns the FileSystemItem-object which abstracts aFile.txt in the temp directory.
        /// 
        /// Remarks:
        /// - Always use "\\" for backslashes since the backslash is used as escape character for Java strings.
        /// - This operation works for relative paths (temp\\aFile.txt) too. The lookup starts at the current directory.
        /// - This operation works for forward slashes '/' too.
        /// - ".." and "." are supported too.
        ///
        /// </summary>
        /// <param name="givenItemPath">Path for which the item shall be returned.</param>
        /// <returns>FileSystemObject or null if no path found.</returns>
        FileSystemItem GetItemFromPath(String givenItemPath);
        #endregion

        #region Responsibility stores drive content persistently
        /// <summary>
        /// Stores the current directory structure persistently.
        /// </summary>
        void Save();

        /// <summary>
        /// Creates a directory structure from the stored structure. The current directory structure is deleted.
        /// </summary>
        void Restore();

        /// <summary>
        /// Builds up a directory structure from the given path on a real drive.
        /// Subdirectories become directories and subdirectories
        /// Files in that directory and the subdirectories become files, content is set to
        /// full path, filename and size of that file.
        /// 
        /// Example:
        /// C:\temp
        /// +-- MyFile1.txt (size 112000 Bytes)
        /// +-- MyFile2.txt (50000)
        /// +-- SubDir1 (Dir)
        /// ....+-- AnExecutable.exe (1234000)
        /// ....+-- ConfigFiles (Dir)
        /// 
        /// Results in
        /// - All files and subdirectories of the root directory deleted
        /// - Current directory set to root directory
        /// - File MyFile1.txt added to root directory with content "C:\temp\MyFile1.txt, size 112000 Bytes"
        /// - File MyFile2.txt added to root directory with content "C:\temp\MyFile2.txt, size 50000 Bytes"
        /// - Directory SubDir1 added to root directory
        /// - File AnExecutable.exe added to SubDir1 with content "C:\temp\SubDir1\AnExecutable.exe, size 1234000 Bytes"
        /// - Directory ConfigFiles added to SubDir1
        /// </summary>
        /// <param name="path">The path to a real directory on any memory device.</param>
        void CreateFromRealDirectory(string realPath);
        #endregion
    }
}
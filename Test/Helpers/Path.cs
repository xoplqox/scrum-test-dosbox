// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;

namespace DosBoxTest.Helpers
{
    public class Path
    {
        public static String Combine(String path1, String path2)
        {
            return path1 + "\\" + path2;
        }
    }
}
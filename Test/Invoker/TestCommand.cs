// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.Collections.Generic;
using DosBox.Command.Framework;
using DosBox.Interfaces;

namespace DosBoxTest.Invoker
{
    public class TestCommand : DosCommand
    {
        public bool CheckNumberOfParametersReturnValue {private get; set;}
        public bool CheckParameterValuesReturnValue {private get; set;}
        public bool Executed {get; private set;}
        public int NumberOfPassedParameters {get; private set;}

        public TestCommand(string cmdName, IDrive drive)
            : base(cmdName, drive)
        {
            this.CheckNumberOfParametersReturnValue = true;
            this.CheckParameterValuesReturnValue = true;
            this.Executed = false;
            this.NumberOfPassedParameters = -1;
        }

        public List<string> getParameters()
        {
            List<string> parameters = new List<string>();
            for (int i = 0; i < GetParameterCount(); i++)
            {
                parameters.Add(GetParameterAt(i));
            }
            return parameters;
        }

        public override void Execute(IOutputter IOutputter)
        {
            this.Executed = true;
        }

        protected override bool CheckNumberOfParameters(int number)
        {
            this.NumberOfPassedParameters = number;
            return this.CheckNumberOfParametersReturnValue;
        }

        protected override bool CheckParameterValues(IOutputter IOutputter)
        {
            return this.CheckParameterValuesReturnValue;
        }
    }
}
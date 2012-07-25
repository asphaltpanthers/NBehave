// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feature.cs" company="NBehave">
//   Copyright (c) 2007, NBehave - http://nbehave.codeplex.com/license
// </copyright>
// <summary>
//   Defines the Feature type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace NBehave.Narrator.Framework
{

    [Serializable]
    public class Feature
    {
        public Feature()
            : this(string.Empty, string.Empty)
        {
        }

        public Feature(string title)
            : this(title, string.Empty)
        { }

        public Feature(string title, string source)
        {
            Source = source;
            Scenarios = new List<Scenario>();
            ExtractTitleAndNarrative(title);
            Background = new Scenario(String.Empty, string.Empty, this);
        }

        public Feature(string title, string narrative, string source, int sourceLine)
        {
            Source = source;
            Scenarios = new List<Scenario>();
            Title = title;
            Narrative = narrative;
            SourceLine = sourceLine;
            Background = new Scenario(String.Empty, string.Empty, this, -1);
        }

        public string Title { get; private set; }
        public string Narrative { get; set; }
        public string Source { get; private set; }
        public int SourceLine { get; private set; }
        public List<Scenario> Scenarios { get; private set; }

        public void AddScenario(Scenario scenario)
        {
            scenario.Feature = this;
            Scenarios.Add(scenario);
        }

        private void ExtractTitleAndNarrative(string content)
        {
            if (content.Contains(Environment.NewLine))
            {
                var lineBreakPosn = content.IndexOf(Environment.NewLine);
                Title = content.Substring(0, lineBreakPosn);
                Narrative = content.Substring(lineBreakPosn + Environment.NewLine.Length);
            }
            else
            {
                Title = content;
                Narrative = String.Empty;
            }
        }

        public void AddBackground(Scenario scenario)
        {
            Background = scenario;
        }

        public Scenario Background { get; private set; }

        public override string ToString()
        {
            var str = string.Format("Feature: {1}{0}{2}", Environment.NewLine, Title, Narrative);
            if (Background.Steps.Any())
            {
                var b = Background.ToStringAsBackground();
                //.Split(Environment.NewLine.ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
                str += string.Format("{0}{0}{1}", Environment.NewLine, b);
            }
            return str;
        }
    }
}
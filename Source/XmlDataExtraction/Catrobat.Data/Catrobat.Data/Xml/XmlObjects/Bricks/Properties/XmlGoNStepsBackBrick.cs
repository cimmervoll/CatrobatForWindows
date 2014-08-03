﻿using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlGoNStepsBackBrick : XmlBrick
    {
        public XmlFormula Steps { get; set; }

        public XmlGoNStepsBackBrick()
        {
        }

        public XmlGoNStepsBackBrick(XElement xElement) : base(xElement)
        {
        }

        public override void LoadFromXml(XElement xRoot)
        {
            Steps = new XmlFormula(xRoot.Element("steps"));
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("goNStepsBackBrick");

            var xVariable = new XElement("steps");
            xVariable.Add(Steps.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Steps != null)
                Steps.LoadReference();
        }
    }
}
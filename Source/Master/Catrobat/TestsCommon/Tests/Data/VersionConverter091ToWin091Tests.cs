﻿using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Collections.Generic;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.VersionConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;

namespace Catrobat.TestsCommon.Tests.Data
{
    [TestClass]
    public class VersionConverter091ToWin091Tests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_ObjectReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_ObjectReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_ObjectReferences_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_SoundReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_SoundReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_SoundReferences_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_LookReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_LookReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_LookReferences_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_GlobalVariableReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_GlobalVariableReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_GlobalVariableReferences_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_LocalVariableReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_LocalVariableReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_LocalVariableReferences_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        #region References in Bricks

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_PointToBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_PointTo_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_PointTo_Output");

            CatrobatVersionConverter.ConvertVersions("0.8", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }


        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_ForeverBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_Forever_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_Forever_Output");

            CatrobatVersionConverter.ConvertVersions("0.8", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_RepeatBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_Repeat_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_Repeat_Output");

            CatrobatVersionConverter.ConvertVersions("0.8", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_IfLoginBeginBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_IfLogicBegin_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_IfLogicBegin_Output");

            CatrobatVersionConverter.ConvertVersions("0.8", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        #endregion

        #region Examples from PocketCode.com

        //[TestMethod]
        //public void CatrobatVersionConverterTest_Convert_Compass()
        //{
        //    XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/VersionConverterTest_08_to_Win08_Compass_Input");
        //    XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/VersionConverterTest_08_to_Win08_Compass_Output");

        //    CatrobatVersionConverter.ConvertVersions("0.8", "Win0.80", actualDocument);
        //    XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        //}

        //[TestMethod]
        //public void CatrobatVersionConverterTest_Convert_Wake_Up()
        //{
        //    XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/VersionConverterTest_08_to_Win08_Wake_Up_Input");
        //    XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/VersionConverterTest_08_to_Win08_Wake_Up_Output");

        //    CatrobatVersionConverter.ConvertVersions("0.8", "Win0.80", actualDocument);
        //    XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        //}

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode1()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test1Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test1Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError ,error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }


        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode2()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test2Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test2Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode3()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test3Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test3Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode4()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test4Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test4Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode5()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test5Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test5Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode6()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test6Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test6Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode7()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test7Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test7Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode8()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test8Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test8Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode9()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test9Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test9Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        #endregion

    }
}

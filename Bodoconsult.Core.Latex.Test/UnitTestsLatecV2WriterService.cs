using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Bodoconsult.Core.Latex.Enums;
using Bodoconsult.Core.Latex.Interfaces;
using Bodoconsult.Core.Latex.Model;
using Bodoconsult.Core.Latex.Services;
using Bodoconsult.Core.Latex.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Latex.Test
{
    [TestFixture]
    public class UnitTestsLatecV2WriterService
    {


        private ILatexWriterService _service;

        private readonly string _fileName = Path.Combine(TestHelper.TempPath, "section1.tex");


        [SetUp]
        public void Setup()
        {

            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }

            _service = new LatexV2WriterService(_fileName);

        }

        [Test]
        public void TestCtor()
        {
            // Act: see Setup()

            // Assert
            Assert.IsNotNull(_service.FileName);
            Assert.IsNotNull(_service.LatexDirectory);
            Assert.IsNotNull(_service.ImageDirectory);
            Assert.IsTrue(string.IsNullOrEmpty(_service.Content));
        }


        [Test]
        public void TestSave()
        {

            // Arrange

            var p = new LaTexParagraphItem { Text = "Test" };

            _service.AddSection(p);

            FileAssert.DoesNotExist(_service.FileName);

            // Act
            _service.Save();

            // Assert
            FileAssert.Exists(_service.FileName);
        }


        [Test]
        public void TestAddSection()
        {

            // Arrange
            var p = new LaTexParagraphItem { Text = "Test" };

            // Act
            _service.AddSection(p);

            // Assert
            var content = _service.Content;

            StringAssert.Contains("section", content);
            StringAssert.Contains(p.Text, content);

            Debug.Print(content);
        }


        [Test]
        public void TestAddSubSection()
        {

            // Arrange
            var p = new LaTexParagraphItem { Text = "Test" };

            // Act
            _service.AddSubSection(p);

            // Assert
            var content = _service.Content;

            StringAssert.Contains("subsection", content);
            StringAssert.Contains(p.Text, content);

            Debug.Print(content);
        }


        [Test]
        public void TestAddSubSubSection()
        {

            // Arrange
            var p = new LaTexParagraphItem { Text = "Test" };

            // Act
            _service.AddSubSubSection(p);

            // Assert
            var content = _service.Content;

            StringAssert.Contains("subsubsection", content);
            StringAssert.Contains(p.Text, content);

            Debug.Print(content);
        }


        [Test]
        public void TestAddListSimple()
        {

            // Arrange
            IList<ILaTexItem> items = new List<ILaTexItem>();

            var p = new LaTexParagraphItem
            {
                Text = "Apple"
            };
            items.Add(p);

            p = new LaTexParagraphItem
            {
                Text = "Banana"
            };
            items.Add(p);

            p = new LaTexParagraphItem
            {
                Text = "Cherry"
            };
            items.Add(p);

            // Act
            _service.AddList(items);

            // Assert
            var content = _service.Content;

            Debug.Print(content);

            StringAssert.Contains("itemize", content);
            StringAssert.Contains("begin{itemize}", content);
            StringAssert.Contains("end{itemize}", content);

            StringAssert.Contains("Apple", content);
            StringAssert.Contains("Banana", content);
            StringAssert.Contains("Cherry", content);


        }



        [Test]
        public void TestAddListNested()
        {

            // Arrange
            IList<ILaTexItem> items = new List<ILaTexItem>();

            var p = new LaTexParagraphItem
            {
                Text = "Fruits"
            };
            items.Add(p);

            var subp = new LaTexParagraphItem
            {
                Text = "Apple"
            };

            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Banana"
            };
            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Cherry"
            };
            p.SubItems.Add(subp);

            p = new LaTexParagraphItem
            {
                Text = "Vegetables"
            };
            items.Add(p);

            subp = new LaTexParagraphItem
            {
                Text = "Carrot"
            };

            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Pepper"
            };
            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Cauliflower"
            };
            p.SubItems.Add(subp);


            // Act
            _service.AddList(items);

            // Assert
            var content = _service.Content;

            Debug.Print(content);

            StringAssert.Contains("itemize", content);
            StringAssert.Contains("begin{itemize}", content);
            StringAssert.Contains("end{itemize}", content);

            StringAssert.Contains("Fruits", content);
            StringAssert.Contains("Apple", content);
            StringAssert.Contains("Banana", content);
            StringAssert.Contains("Cherry", content);

            StringAssert.Contains("Vegetables", content);
            StringAssert.Contains("Carrot", content);
            StringAssert.Contains("Pepper", content);
            StringAssert.Contains("Cauliflower", content);
        }


        [Test]
        public void TestAddNumberedListSimple()
        {

            // Arrange
            IList<ILaTexItem> items = new List<ILaTexItem>();

            var p = new LaTexParagraphItem
            {
                Text = "Apple"
            };
            items.Add(p);

            p = new LaTexParagraphItem
            {
                Text = "Banana"
            };
            items.Add(p);

            p = new LaTexParagraphItem
            {
                Text = "Cherry"
            };
            items.Add(p);

            // Act
            _service.AddNumberedList(items);

            // Assert
            var content = _service.Content;

            Debug.Print(content);

            StringAssert.Contains("enumerate", content);
            StringAssert.Contains("begin{enumerate}", content);
            StringAssert.Contains("end{enumerate}", content);

            StringAssert.Contains("Apple", content);
            StringAssert.Contains("Banana", content);
            StringAssert.Contains("Cherry", content);


        }



        [Test]
        public void TestAddNumberedListNested()
        {

            // Arrange
            IList<ILaTexItem> items = new List<ILaTexItem>();

            var p = new LaTexParagraphItem
            {
                Text = "Fruits"
            };
            items.Add(p);

            var subp = new LaTexParagraphItem
            {
                Text = "Apple"
            };

            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Banana"
            };
            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Cherry"
            };
            p.SubItems.Add(subp);

            p = new LaTexParagraphItem
            {
                Text = "Vegetables"
            };
            items.Add(p);

            subp = new LaTexParagraphItem
            {
                Text = "Carrot"
            };

            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Pepper"
            };
            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Cauliflower"
            };
            p.SubItems.Add(subp);


            // Act
            _service.AddNumberedList(items);

            // Assert
            var content = _service.Content;

            Debug.Print(content);

            StringAssert.Contains("enumerate", content);
            StringAssert.Contains("begin{enumerate}", content);
            StringAssert.Contains("end{enumerate}", content);

            StringAssert.Contains("Fruits", content);
            StringAssert.Contains("Apple", content);
            StringAssert.Contains("Banana", content);
            StringAssert.Contains("Cherry", content);

            StringAssert.Contains("Vegetables", content);
            StringAssert.Contains("Carrot", content);
            StringAssert.Contains("Pepper", content);
            StringAssert.Contains("Cauliflower", content);
        }


        [Test]
        public void TestAddImage()
        {

            // Arrange
            var imageFile = Path.Combine(TestHelper.TestDataPath, "test.jpg");

            var buffer = File.ReadAllBytes(imageFile);

            var ms = new MemoryStream(buffer);

            var imageItem = new LaTexImageItem
            {
                Text = "Test",
                ImageData = ms,
                ImageType = LaTexImageType.Jpg
            };

            // Act
            var fileName = _service.AddImage(imageItem);

            // Assert
            var content = _service.Content;

            Debug.Print(content);

            StringAssert.Contains(@"\begin{figure}", content);
            StringAssert.Contains("\\includegraphics[width=\\textwidth]{", content);
            StringAssert.Contains(@"\end{figure}", content);

            FileAssert.Exists(fileName);
        }


        [Test]
        public void TestAddParagraphNested()
        {

            // Arrange
            IList<ILaTexItem> items = new List<ILaTexItem>();

            var p = new LaTexParagraphItem
            {
                Text = "Fruits"
            };
            items.Add(p);

            var subp = new LaTexParagraphItem
            {
                Text = "Apple"
            };

            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Banana"
            };
            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Cherry"
            };
            p.SubItems.Add(subp);

            p = new LaTexParagraphItem
            {
                Text = "Vegetables"
            };
            items.Add(p);

            subp = new LaTexParagraphItem
            {
                Text = "Carrot"
            };

            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Pepper"
            };
            p.SubItems.Add(subp);

            subp = new LaTexParagraphItem
            {
                Text = "Cauliflower"
            };
            p.SubItems.Add(subp);


            // Act
            _service.AddParagraph(items);

            // Assert
            var content = _service.Content;

            Debug.Print(content);

            StringAssert.Contains("itemize", content);
            StringAssert.Contains("begin{itemize}", content);
            StringAssert.Contains("end{itemize}", content);

            StringAssert.Contains("Fruits", content);
            StringAssert.Contains("Apple", content);
            StringAssert.Contains("Banana", content);
            StringAssert.Contains("Cherry", content);

            StringAssert.Contains("Vegetables", content);
            StringAssert.Contains("Carrot", content);
            StringAssert.Contains("Pepper", content);
            StringAssert.Contains("Cauliflower", content);
        }

        [Test]
        public void TestAddTable()
        {

            // Arrange
            const string testValue1 = "Test";
            const string testValue2 = "Blubb";
            const string testValue3 = "Blabb";
            const string testValue4 = "Blobb";


            var tableItem = new LaTextTableItem
            {
                TableData = new[,] {{testValue1, testValue2}, {testValue3, testValue4}}
            };

            // Act
            _service.AddTable(tableItem);

            // Assert
            Debug.Print(_service.Content);

            Assert.IsTrue(_service.Content.Contains(testValue1));
            Assert.IsTrue(_service.Content.Contains(testValue2));
            Assert.IsTrue(_service.Content.Contains(testValue3));
            Assert.IsTrue(_service.Content.Contains(testValue4));

            StringAssert.Contains(@"\begin{tabular}[t]{rl}", _service.Content);
            StringAssert.Contains(@"\end{tabular}", _service.Content);
            StringAssert.Contains(@"\toprule", _service.Content);
            StringAssert.Contains(@"\midrule", _service.Content);
            StringAssert.Contains(@"\bottomrule", _service.Content);
        }

    }
}
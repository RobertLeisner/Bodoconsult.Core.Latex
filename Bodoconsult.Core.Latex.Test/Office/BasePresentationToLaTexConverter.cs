using System.IO;
using Bodoconsult.Core.Latex.Interfaces;
using Bodoconsult.Core.Latex.Test.Helpers;
using NUnit.Framework;
using Path = System.IO.Path;

namespace Bodoconsult.Core.Latex.Test.Office
{
    public abstract class BasePresentationToLaTexConverter
    {

        protected ILatexWriterService LatexWriterService;

        protected IPresentationToLaTexConverter Converter;

        protected IPresentationAnalyzer Analyzer;


        protected string TargetPath = Path.Combine(TestHelper.TempPath, "chapter1.tex");


        protected string Source = Path.Combine(TestHelper.TestDataPath, "Test.pptx");

        [Test]
        public void TestCtor()
        {

            // Act: see Setup()

            // Assert
            Assert.IsNotNull(Analyzer.PresentationMetaData);
            Assert.AreEqual(Source, Analyzer.PresentationMetaData.SourceFileName);

            Assert.IsNotNull(Converter.Analyzer.PresentationMetaData);
            Assert.AreEqual(Source, Converter.Analyzer.PresentationMetaData.SourceFileName);

        }


        [Test]
        public void TestConvert()
        {

            // Arrange
            if (File.Exists(Converter.LaTexWriterService.FileName))
            {
                File.Delete(Converter.LaTexWriterService.FileName);
            }

            // Act: see Setup()
            var result = Converter.Convert();

            // Assert
            Assert.IsNotNull(result);

            Assert.IsFalse(string.IsNullOrEmpty(result.Trim()));

            FileAssert.Exists(Converter.LaTexWriterService.FileName);

            TestHelper.PrintPresentation(Analyzer.PresentationMetaData);

        }
    }
}
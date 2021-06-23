using System.IO;
using Bodoconsult.Core.Latex.Converters;
using Bodoconsult.Core.Latex.Office.Analyzer;
using Bodoconsult.Core.Latex.Services;
using Bodoconsult.Core.Latex.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Latex.Test.Office
{
    [TestFixture]
    public class UnitTestPresentationToLaTexConverterPowerpoint2016AnalyzerRealWorld : BasePresentationToLaTexConverter
    {

        [SetUp]
        public void Setup()
        {

            Source = Path.Combine(TestHelper.TestDataPath, "Grundkurs Wirtschaft.pptx");
            TargetPath = Path.Combine(TestHelper.TempPath, "GrundkursWirtschaft.tex");

            LatexWriterService = new LatexV2WriterService(TargetPath);

            Analyzer = new Powerpoint2016Analyzer(Source);

            Converter = new PresentationToLaTexConverter(Analyzer, LatexWriterService);

        }
    }
}
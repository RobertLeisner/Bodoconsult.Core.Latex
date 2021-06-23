using Bodoconsult.Core.Latex.Converters;
using Bodoconsult.Core.Latex.Office.Analyzer;
using Bodoconsult.Core.Latex.Services;
using NUnit.Framework;

namespace Bodoconsult.Core.Latex.Test.Office
{
    [TestFixture]
    public class UnitTestPresentationToLaTexConverterPowerpoint2016Analyzer : BasePresentationToLaTexConverter
    {

        [SetUp]
        public void Setup()
        {
            LatexWriterService = new LatexV2WriterService(TargetPath);

            Analyzer = new Powerpoint2016Analyzer(Source);

            Converter = new PresentationToLaTexConverter(Analyzer, LatexWriterService);

        }
    }
}
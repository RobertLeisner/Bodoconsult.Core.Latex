using Bodoconsult.Core.Latex.Converters;
using Bodoconsult.Core.Latex.Faking;
using Bodoconsult.Core.Latex.Model;
using Bodoconsult.Core.Latex.Services;
using Bodoconsult.Core.Latex.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Latex.Test.Office
{
    [TestFixture]
    public class UnitTestPresentationToLaTexConverterFakeAnalyzer : BasePresentationToLaTexConverter
    {

        [SetUp]
        public void Setup()
        {

            LatexWriterService = new LatexV2WriterService(TargetPath);

            var presentation = new PresentationMetaData(Source);

            TestHelper.FillPresentation(presentation);

            Analyzer = new FakeAnalyzer(presentation);

            Converter = new PresentationToLaTexConverter(Analyzer, LatexWriterService);

        }
    }
}
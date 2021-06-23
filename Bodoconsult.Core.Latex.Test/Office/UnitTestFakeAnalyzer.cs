using System.IO;
using Bodoconsult.Core.Latex.Faking;
using Bodoconsult.Core.Latex.Model;
using Bodoconsult.Core.Latex.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Latex.Test.Office
{
    [TestFixture]
    public class UnitTestFakeAnalyzer : BasePresentationAnalyzer
    {

        [SetUp]
        public void Setup()
        {

            Source = Path.Combine(TestHelper.TestDataPath, "Test.pptx");

            var presentation = new PresentationMetaData(Source);

            TestHelper.FillPresentation(presentation);

            Analyzer = new FakeAnalyzer(presentation);

        }


    }
}
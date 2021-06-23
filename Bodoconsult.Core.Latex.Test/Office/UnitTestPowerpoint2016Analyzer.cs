using System.IO;
using Bodoconsult.Core.Latex.Office.Analyzer;
using Bodoconsult.Core.Latex.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Latex.Test.Office
{
    [TestFixture]
    public class UnitTestPowerpoint2016Analyzer : BasePresentationAnalyzer
    {

        [SetUp]
        public void Setup()
        {

            Source = Path.Combine(TestHelper.TestDataPath, "Test.pptx");

            Analyzer = new Powerpoint2016Analyzer(Source);

        }


    }
}
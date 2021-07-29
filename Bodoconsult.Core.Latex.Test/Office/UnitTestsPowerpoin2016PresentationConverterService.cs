using System.IO;
using Bodoconsult.Core.Latex.Model;
using Bodoconsult.Core.Latex.Services;
using NUnit.Framework;

namespace Bodoconsult.Core.Latex.Test.Office
{
    public class UnitTestsPowerpoin2016PresentationConverterService: BasePresentationConverterService
    {
        [Test]
        public void TestConvert()
        {

            // Arrange
            Job = new PresentationJob
            {
                PresentationFilePath = @"D:\Daten\Projekte\Customer\Royotech\Presentations\Overview_Datalayer.pptx",
                LaTexFilePath = @"D:\Daten\Projekte\Customer\Royotech\Presentations\LaTex\StSysDatalayer.tex"
            };

            if (File.Exists(Job.LaTexFilePath))
            {
                File.Delete(Job.LaTexFilePath);
            }

            Service = new Powerpoin2016PresentationConverterService(Job);

            // Act
            Service.ConvertPresentation();

            // Assert
            FileAssert.Exists(Job.LaTexFilePath);

        }

    }
}
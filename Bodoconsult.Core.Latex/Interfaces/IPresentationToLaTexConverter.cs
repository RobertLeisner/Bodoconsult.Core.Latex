using Bodoconsult.Core.Latex.Model;

namespace Bodoconsult.Core.Latex.Interfaces
{
    /// <summary>
    /// Interface for converters from <see cref="PresentationMetaData"/>
    /// </summary>
    public interface IPresentationToLaTexConverter
    {


        /// <summary>
        /// Current presentation
        /// </summary>
        PresentationMetaData Presentation { get; }


        /// <summary>
        /// Current presentation analyzer
        /// </summary>
        IPresentationAnalyzer Analyzer { get; }

        /// <summary>
        /// The current LaTex writer service to use for conversion
        /// </summary>
        ILatexWriterService LaTexWriterService { get; }



        /// <summary>
        /// Convert the presentation
        /// </summary>
        string Convert();

    }

}
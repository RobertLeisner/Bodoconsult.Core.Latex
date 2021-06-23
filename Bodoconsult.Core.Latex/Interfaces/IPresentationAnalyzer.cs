using System;
using Bodoconsult.Core.Latex.Model;

namespace Bodoconsult.Core.Latex.Interfaces
{

    /// <summary>
    /// Interface for presentation analyzers
    /// </summary>
    public interface IPresentationAnalyzer: IDisposable
    {

        /// <summary>
        /// Include hidden slides
        /// </summary>
        bool IncludeHiddenSlides { get; set; }


        /// <summary>
        /// Analyse the presentation
        /// </summary>
        PresentationMetaData Analyse();


        /// <summary>
        /// Current meta data of the presentation
        /// </summary>
        PresentationMetaData PresentationMetaData { get; }



    }
}
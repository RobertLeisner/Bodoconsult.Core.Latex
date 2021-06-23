using System.Collections.Generic;

namespace Bodoconsult.Core.Latex.Model
{
    /// <summary>
    /// Meta data class of a presentation
    /// </summary>
    public class PresentationMetaData
    {

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="sourceFileName"></param>
        public PresentationMetaData(string sourceFileName)
        {
            SourceFileName = sourceFileName;
        }


        /// <summary>
        /// Source file name
        /// </summary>
        public string SourceFileName { get; }

        /// <summary>
        /// Name of the LaTex file to convert the presentation into
        /// </summary>
        public string TargetFileName { get; set; }


        /// <summary>
        /// All slides of presentation
        /// </summary>
        public IList<SlideMetaData> Slides { get; } = new List<SlideMetaData>();


    }
}

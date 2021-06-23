using System.Collections.Generic;
using System.IO;
using Bodoconsult.Core.Latex.Enums;
using Bodoconsult.Core.Latex.Interfaces;

namespace Bodoconsult.Core.Latex.Model
{
    /// <summary>
    /// Paragraph class for image paragraphs
    /// </summary>
    public class ImageItem : ILaTexImageItem
    {

        /// <summary>
        /// Image legend text content
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Nested items
        /// </summary>
        public IList<ILaTexItem> SubItems { get; } = new List<ILaTexItem>();


        /// <summary>
        /// Image data as stream
        /// </summary>
        public Stream ImageData { get; set; }


        /// <summary>
        /// Imge type
        /// </summary>
        public LaTexImageType ImageType { get; set; } = LaTexImageType.Jpg;
    }
}

using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Bodoconsult.Core.Latex.Enums;
using Bodoconsult.Core.Latex.Interfaces;

namespace Bodoconsult.Core.Latex.Model
{
    /// <summary>
    /// Paragraph class for image paragraphs
    /// </summary>
    public class LaTexImageItem : ILaTexImageItem
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
        /// The sort order id the items follow up
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// The shape position of surrounding shape
        /// </summary>
        public long ShapePosition { get; set; }


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

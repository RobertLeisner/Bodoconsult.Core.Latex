using System.Collections.Generic;
using System.Numerics;
using Bodoconsult.Core.Latex.Interfaces;

namespace Bodoconsult.Core.Latex.Model
{

    /// <summary>
    /// Paragraph class for text paragraphs
    /// </summary>
    public class LaTexParagraphItem : ILaTexTextItem
    {

        /// <summary>
        /// Paragraph text content
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
        /// Indent level
        /// </summary>
        public int IndentLevel { get; set; } = 0;

    }
}

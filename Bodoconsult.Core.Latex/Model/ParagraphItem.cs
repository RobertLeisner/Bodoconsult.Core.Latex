using System.Collections.Generic;
using Bodoconsult.Core.Latex.Interfaces;

namespace Bodoconsult.Core.Latex.Model
{

    /// <summary>
    /// Paragraph class for text paragraphs
    /// </summary>
    public class ParagraphItem : ILaTexTextItem
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
        /// Indent level
        /// </summary>
        public int IndentLevel { get; set; } = 0;

    }
}

using System.Collections.Generic;
using System.Numerics;

namespace Bodoconsult.Core.Latex.Interfaces
{
    /// <summary>
    /// Interface for items holding text for LaTex output
    /// </summary>
    public interface ILaTexItem
    {

        /// <summary>
        /// Holds the text for the LaTex output
        /// </summary>
        string Text { get; set; }


        /// <summary>
        /// Nested items
        /// </summary>
        IList<ILaTexItem> SubItems { get; }


        /// <summary>
        /// The sort order id the items follow up
        /// </summary>
        int SortId{ get; set; }

        /// <summary>
        /// The shape position of surrounding shape
        /// </summary>
        long ShapePosition { get; set; }

    }
}
using System.Collections.Generic;

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
    }
}
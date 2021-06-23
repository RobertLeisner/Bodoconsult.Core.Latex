namespace Bodoconsult.Core.Latex.Interfaces
{
    /// <summary>
    /// Interface for items holding paragraph information for LaTex output
    /// </summary>
    public interface ILaTexTextItem : ILaTexItem
    {

        /// <summary>
        /// Indent level
        /// </summary>
        int IndentLevel { get; set; }


    }
}

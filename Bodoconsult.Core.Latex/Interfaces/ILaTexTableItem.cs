namespace Bodoconsult.Core.Latex.Interfaces
{
    /// <summary>
    /// Transportation item for table data to LaTex
    /// </summary>
    public interface ILaTexTableItem : ILaTexItem
    {

        /// <summary>
        /// Table content: 2-dimensional array with dim 0 are rows and dim 1 are the columns
        /// </summary>

        string[,] TableData { get; set; }

        

    }
}
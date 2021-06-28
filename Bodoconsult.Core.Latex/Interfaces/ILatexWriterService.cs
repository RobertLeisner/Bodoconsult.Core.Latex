using System.Collections.Generic;

namespace Bodoconsult.Core.Latex.Interfaces
{

    /// <summary>
    /// Interface for LaTex writer services to create LaTex output
    /// </summary>
    public interface ILatexWriterService
    {
        /// <summary>
        /// Get the current content of the LaTex file
        /// </summary>
        string Content { get; }

        /// <summary>
        /// Current directory for LaTex output
        /// </summary>
        string LatexDirectory { get; }

        /// <summary>
        /// Current image directory for LaTex output
        /// </summary>
        string ImageDirectory { get; }


        /// <summary>
        /// Get the current file name of the LaTex file
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Add a section to the file
        /// </summary>
        /// <param name="section">Name of the new section</param>
        void AddSection(ILaTexTextItem section);


        /// <summary>
        /// Add a subsection to the file
        /// </summary>
        /// <param name="section">Name of the new sub section</param>
        void AddSubSection(ILaTexTextItem section);

        /// <summary>
        /// Add a sub sub section to the file
        /// </summary>
        /// <param name="section">Name of the new sub sub section</param>
        void AddSubSubSection(ILaTexTextItem section);


        /// <summary>
        /// Adds a paragraph to output file with indent level 0. Sub items are added as list (LaTex: itemize)
        /// </summary>
        /// <param name="item">Item to write as a paragraph</param>
        void AddParagraph(ILaTexItem item);

        /// <summary>
        /// Adds a paragraph to output file. Sub items are added as list (LaTex: itemize)
        /// </summary>
        /// <param name="item">Item to write as list</param>
        /// <param name="indentLevel">Indent level</param>
        void AddParagraph(ILaTexItem item, int indentLevel);



        /// <summary>
        /// Adds a paragraph to output file with indent level 0. Sub items are added as list (LaTex: itemize)
        /// </summary>
        /// <param name="items">Items to write as list</param>
        void AddParagraph(IList<ILaTexItem> items);

        /// <summary>
        /// Adds a paragraph to output file with indent level 0 Sub items are added as list (LaTex: itemize)
        /// </summary>
        /// <param name="items">Items to write as list</param>
        /// <param name="indentLevel">Indent level</param>
        void AddParagraph(IList<ILaTexItem> items, int indentLevel);



        /// <summary>
        /// Adds a plain unnumbered list of items to output file (LaTex: itemize) with indent level 0
        /// </summary>
        /// <param name="items">Items to write as list</param>
        void AddList(IList<ILaTexItem> items);

        /// <summary>
        /// Adds a plain unnumbered list of items to output file (LaTex: itemize)
        /// </summary>
        /// <param name="items">Items to write as list</param>
        /// <param name="indentLevel">Indent level</param>
        void AddList(IList<ILaTexItem> items, int indentLevel);

        /// <summary>
        /// Adds a numbered list of items to output file (LaTex: itemize) with indent level 0
        /// </summary>
        /// <param name="items">Items to write as list</param>
        void AddNumberedList(IList<ILaTexItem> items);

        /// <summary>
        /// Adds a numbered list of items to output file (LaTex: itemize)
        /// </summary>
        /// <param name="items">Items to write as list</param>
        /// <param name="indentLevel">Indent level</param>
        void AddNumberedList(IList<ILaTexItem> items, int indentLevel);

        /// <summary>
        /// Add an imag eitem to the LaTex output
        /// </summary>
        /// <param name="imageItem">Image item data</param>
        string AddImage(ILaTexImageItem imageItem);


        /// <summary>
        /// Save the LaTex file finally
        /// </summary>
        void Save();

        /// <summary>
        /// Add a table to the LaTex output
        /// </summary>
        /// <param name="data">Table data</param>
        void AddTable(ILaTexTableItem data);
    }
}


// Based on https://github.com/timdams/Pptx2Tex/blob/master/PPT_To_Latex/Program.cs

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Bodoconsult.Core.Latex.Enums;
using Bodoconsult.Core.Latex.Helpers;
using Bodoconsult.Core.Latex.Interfaces;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace Bodoconsult.Core.Latex.Services
{
    /// <summary>
    /// Writes a LaTex file for LaTex version 2
    /// </summary>
    public class LatexV2WriterService : ILatexWriterService
    {
        private readonly StringBuilder _content = new StringBuilder();

        private int _imageCounter;

        private const string ImagesFolderName = "Images";

        private readonly string _imageName;



        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="latexFileName">Name of the LaTex file to create</param>
        public LatexV2WriterService(string latexFileName)
        {

            FileName = latexFileName;

            var fi = new FileInfo(FileName);

            _imageName = fi.Name.Replace(fi.Extension, "");

            LatexDirectory = fi.DirectoryName;

            ImageDirectory = Path.Combine(LatexDirectory, ImagesFolderName);

            if (!Directory.Exists(ImageDirectory))
            {
                Directory.CreateDirectory(ImageDirectory);
            }

        }

        /// <summary>
        /// Get the current content of the LaTex file
        /// </summary>
        public string Content => _content.ToString();

        /// <summary>
        /// Current directory for LaTex output
        /// </summary>
        public string LatexDirectory { get; }

        /// <summary>
        /// Current image directory for LaTex output
        /// </summary>
        public string ImageDirectory { get; }


        /// <summary>
        /// Get the current file name of the LaTex file
        /// </summary>
        public string FileName { get; }


        /// <summary>
        /// Add a section to the file
        /// </summary>
        /// <param name="section">Section data</param>
        public void AddSection(ILaTexTextItem section)
        {
            var s = LaTexHelper.Escape(section.Text);
            WriteWithIndent($@"\section[{s}]{{{s}}}", section.IndentLevel);
            _content.AppendLine("");
        }


        /// <summary>
        /// Add a sub section to the file
        /// </summary>
        /// <param name="section">Section data</param>
        public void AddSubSection(ILaTexTextItem section)
        {
            var s = LaTexHelper.Escape(section.Text);
            WriteWithIndent($@"\subsection[{s}]{{{s}}}", section.IndentLevel);
            _content.AppendLine("");
        }


        /// <summary>
        /// Add a sub sub section to the file
        /// </summary>
        /// <param name="section">Sub sub section data</param>
        public void AddSubSubSection(ILaTexTextItem section)
        {
            var s = LaTexHelper.Escape(section.Text);

            WriteWithIndent($@"\subsubsection[{s}]{{{s}}}", section.IndentLevel);
            _content.AppendLine("");
        }

        /// <summary>
        /// Adds a paragraph to output file with indent level 0. Sub items are added as list (LaTex: itemize)
        /// </summary>
        /// <param name="item">Item to write as a paragraph</param>
        public void AddParagraph(ILaTexItem item)
        {
            AddParagraph(item, 0);
        }

        /// <summary>
        /// Adds a paragraph to output file. Sub items are added as list (LaTex: itemize)
        /// </summary>
        /// <param name="item">Item to write as list</param>
        /// <param name="indentLevel">Indent level</param>
        public void AddParagraph(ILaTexItem item, int indentLevel)
        {
            WriteWithIndent(LaTexHelper.Escape(item.Text), indentLevel);
            AddList(item.SubItems, indentLevel + 1);
        }


        /// <summary>
        /// Adds a paragraph to output file with indent level 0. Sub items are added as list (LaTex: itemize)
        /// </summary>
        /// <param name="items">Items to write as list</param>
        public void AddParagraph(IList<ILaTexItem> items)
        {
            AddParagraph(items, 0);
        }

        /// <summary>
        /// Adds a paragraph to output file with indent level 0 Sub items are added as list (LaTex: itemize)
        /// </summary>
        /// <param name="items">Items to write as list</param>
        /// <param name="indentLevel">Indent level</param>
        public void AddParagraph(IList<ILaTexItem> items, int indentLevel)
        {

            foreach (var item in items)
            {
                WriteWithIndent(LaTexHelper.Escape(item.Text), indentLevel);

                AddList(item.SubItems, indentLevel + 1);

            }

        }


        /// <summary>
        /// Adds a plain unnumbered list of items to output file (LaTex: itemize) with indent level 0
        /// </summary>
        /// <param name="items">Items to write as list</param>
        public void AddList(IList<ILaTexItem> items)
        {

            AddList(items, 0);

        }


        /// <summary>
        /// Adds a plain unnumbered list of items to output file (LaTex: itemize)
        /// </summary>
        /// <param name="items">Items to write as list</param>
        /// <param name="indentLevel">Indent level</param>
        public void AddList(IList<ILaTexItem> items, int indentLevel)
        {

            if (!items.Any())
            {
                return;
            }

            WriteWithIndentNoBlank(@"\begin{itemize}", indentLevel);

            foreach (var item in items)
            {

                if (!string.IsNullOrEmpty(item.Text))
                {

                    WriteWithIndentNoBlank($@"\item {LaTexHelper.Escape(item.Text)}", indentLevel);
                    //Buggy: WriteWithIndentNoBlank(@"\item " + item, indentLevel);

                }

                if (item.SubItems.Any())
                {
                    AddList(item.SubItems, indentLevel + 1);
                }

            }

            WriteWithIndent(@"\end{itemize}", indentLevel);

        }


        /// <summary>
        /// Adds a numbered list of items to output file (LaTex: enumerate) with indent level 0
        /// </summary>
        /// <param name="items">Items to write as list</param>
        public void AddNumberedList(IList<ILaTexItem> items)
        {

            AddNumberedList(items, 0);

        }


        /// <summary>
        /// Adds a numbered list of items to output file (LaTex: enumerate)
        /// </summary>
        /// <param name="items">Items to write as list</param>
        /// <param name="indentLevel">Indent level</param>
        public void AddNumberedList(IList<ILaTexItem> items, int indentLevel)
        {
            if (!items.Any())
            {
                return;
            }

            WriteWithIndentNoBlank(@"\begin{enumerate}", indentLevel);

            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.Text))
                {
                    WriteWithIndentNoBlank($@"\item {LaTexHelper.Escape(item.Text)}", indentLevel);
                    //Buggy: WriteWithIndentNoBlank(@"\item " + item, indentLevel);
                }

                if (item.SubItems.Any())
                {
                    AddNumberedList(item.SubItems, indentLevel + 1);
                }
            }

            WriteWithIndent(@"\end{enumerate}", indentLevel);

        }

        /// <summary>
        /// Add an imag eitem to the LaTex output
        /// </summary>
        /// <param name="imageItem">Image item data</param>
        public string AddImage(ILaTexImageItem imageItem)
        {

            _imageCounter++;

            var ext = imageItem.ImageType == LaTexImageType.Png ? "png" : "jpg";

            var imageFilename = $"{_imageName}{_imageCounter}.{ext}";

            var target = Path.Combine(ImageDirectory, imageFilename);


            try
            {

                var img = System.Drawing.Image.FromStream(imageItem.ImageData);

                if (File.Exists(target))
                {
                    File.Delete(target);
                }
                img.Save(target);
                img.Dispose();
            }
            catch
            {
                _imageCounter--;
                return null;
            }

            _content.AppendLine(@"\begin{figure}[h] \begin{center}");
            _content.AppendLine($"\t\\includegraphics[width=\\textwidth]{{{ImagesFolderName}//{imageFilename}}}");
            _content.AppendLine(@"\end{center} \end{figure}");

            return target;
        }

        /// <summary>
        /// Write indented to the output file
        /// </summary>
        /// <param name="stringtowrite">The string to write to the output file</param>
        /// <param name="indentlevel">Indent level</param>
        private void WriteWithIndent(string stringtowrite, int indentlevel)
        {
            if (indentlevel < 0)
            {
                indentlevel = 0;
            }

            var sb = new StringBuilder();

            for (var i = 0; i < indentlevel; i++)
            {
                sb.Append("\t");
            }
            sb.Append(stringtowrite);

            _content.AppendLine(sb.ToString());
            _content.AppendLine("");
        }


        /// <summary>
        /// Write indented to the output file do not add a blank line at the end
        /// </summary>
        /// <param name="stringtowrite">The string to write to the output file</param>
        /// <param name="indentlevel">Indent level</param>
        private void WriteWithIndentNoBlank(string stringtowrite, int indentlevel)
        {
            if (indentlevel < 0)
            {
                indentlevel = 0;
            }

            var sb = new StringBuilder();

            for (var i = 0; i < indentlevel; i++)
            {
                sb.Append("\t");
            }
            sb.Append(stringtowrite);

            _content.AppendLine(sb.ToString());

        }







        /// <summary>
        /// Save the LaTex file finally
        /// </summary>
        public void Save()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            File.WriteAllText(FileName, _content.ToString(), Encoding.UTF8);
        }


        public void AddTable(ILaTexTableItem item)
        {
            var max0 = item.TableData.GetLength(0) - 1;
            var max1 = item.TableData.GetLength(1) - 1;

            _content.AppendLine($@"\begin{{tabular}}[h]{{{new string('l', max0)}}}");
            _content.AppendLine(@"\toprule");

            _content.Append($@"\midrule{Environment.NewLine}");


            // ToDo: implement it
            for (var rowIndex = 0; rowIndex <= max0; rowIndex++)
            {

                var s = new StringBuilder();

                for (var colIndex = 0; colIndex <= max1; colIndex++)
                {
                    var content = LaTexHelper.Escape(item.TableData.GetValue(rowIndex, colIndex).ToString());
                    s.Append(content);

                    if (colIndex != max1)
                    {
                        s.Append(" & ");
                    }
                }

                s.Append($@" \\ {Environment.NewLine}");

                Debug.Print(s.ToString());

                _content.Append(s);

            }

            _content.AppendLine(@"\bottomrule");
            _content.AppendLine(@"\end{tabular}");
        }
    }
}

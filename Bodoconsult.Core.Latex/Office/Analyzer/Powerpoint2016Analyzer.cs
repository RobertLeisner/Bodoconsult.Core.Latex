﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Bodoconsult.Core.Latex.Enums;
using Bodoconsult.Core.Latex.Interfaces;
using Bodoconsult.Core.Latex.Model;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using Shape = DocumentFormat.OpenXml.Presentation.Shape;

namespace Bodoconsult.Core.Latex.Office.Analyzer
{
    /// <summary>
    /// Analyzer for MS PowerPoint 2016 to 2019
    /// </summary>
    public class Powerpoint2016Analyzer : IPresentationAnalyzer
    {
        private PresentationDocument presentationDocument;

        private PresentationPart presentationPart;

        private Presentation presentation;

        private readonly PresentationMetaData _presentationMetaData;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="sourceFileName">The path to the presentation</param>
        public Powerpoint2016Analyzer(string sourceFileName)
        {
            _presentationMetaData = new PresentationMetaData(sourceFileName);

            presentationDocument = PresentationDocument.Open(sourceFileName, false);

            presentationPart = presentationDocument.PresentationPart;
            presentation = presentationPart.Presentation;

        }

        /// <summary>
        /// Include hidden slides
        /// </summary>
        public bool IncludeHiddenSlides { get; set; }

        /// <summary>
        /// Analyse the presentation
        /// </summary>
        public PresentationMetaData Analyse()
        {

            foreach (SlideId slideId in presentation.SlideIdList)
            {
                AnalyseSlide(slideId);
            }

            return _presentationMetaData;
        }

        private void AnalyseSlide(SlideId slideId)
        {

            if (slideId.RelationshipId == null)
            {
                return;
            }

            var relId = slideId.RelationshipId.Value;

            if (relId == null)
            {
                return;
            }

            var slide = (SlidePart)presentationPart.GetPartById(relId);

            if (!IncludeHiddenSlides) //check if we got hidden slide, of so, skip
            {
                if (slide.Slide.Show != null && slide.Slide.Show.HasValue && slide.Slide.Show.Value == false)
                {
                    return;
                }
            }

            //On equations: http://blogs.msdn.com/b/murrays/archive/2006/10/07/mathml-and-ecma-math-_2800_omml_2900_-.aspx && http://stackoverflow.com/questions/2300757/c-sharp-api-for-ms-word-equation-editor
            //Solution? http://stackoverflow.com/questions/16759100/how-to-parse-mathml-in-output-of-wordopenxml


            var slideMetaData = new SlideMetaData();

            var title = GetSlideTitle(slide);

            slideMetaData.Title = title.ToString();

            if (slide.SlideLayoutPart.SlideLayout.Type == SlideLayoutValues.Title ||
                slide.SlideLayoutPart.SlideLayout.Type == SlideLayoutValues.TitleOnly)
            {

                //Debug.WriteLine("%%%%%%%%%%NEWSECTION%%%%%%%%%%%%");
                //Debug.WriteLine($"section{{{title}}}");

                slideMetaData.SlideType = SlideType.Title;

            }
            else if (slide.SlideLayoutPart.SlideLayout.Type == SlideLayoutValues.SectionHeader)
            {
                //Debug.WriteLine("%%%%%%%%%%NEWSUBSECTION%%%%%%%%%%%%");
                //Debug.WriteLine($"section{{{title}}}");

                slideMetaData.SlideType = SlideType.Section;
            }
            //else
            //{
            //    //Debug.WriteLine("\n\n\n********************************"+ slide.SlideLayoutPart.SlideLayout.Type);

            //    //Debug.WriteLine("%%%%%%%%%%NEWSUBSUBSECTION%%%%%%%%%%%%");
            //    ////Get title
            //    //Debug.WriteLine("\t\t" + title);
            //    //Debug.WriteLine("----------------------");
            //}

            PresentationMetaData.Slides.Add(slideMetaData);


            var previndent = 1;
            //var firstitemdone = false;

            IList<ParagraphItem> predecessors = new List<ParagraphItem>();
            bool lStart = true;


            var dummy = new ParagraphItem { Text = "Dummy", IndentLevel = 0 };

            predecessors.Add(dummy);

            foreach (var paragraph in slide.Slide.Descendants<Paragraph>().Skip(1))
            {

                if (IsInTable(paragraph))
                {
                    continue;
                }


                var p = new ParagraphItem();

                //http://msdn.microsoft.com/en-us/library/ee922775(v=office.14).aspx
                var currentIndentLevel = 1;

                if (paragraph.ParagraphProperties != null)
                {
                    if (paragraph.ParagraphProperties.HasAttributes)
                    {
                        try
                        {
                            var lvl = paragraph.ParagraphProperties.GetAttribute("lvl", "").Value;
                            currentIndentLevel = int.Parse(lvl) + 1;
                        }
                        catch
                        {
                            //Ignore
                        }
                    }
                }


                var paragraphText = new StringBuilder();
                // Iterate through the lines of the paragraph.
                foreach (var text in paragraph.Descendants<DocumentFormat.OpenXml.Drawing.Text>())
                {
                    paragraphText.Append(text.Text);
                }

                var master = predecessors[currentIndentLevel - 1];
                master.SubItems.Add(p);

                if (paragraphText.Length > 0)
                {
                    p.Text = paragraphText.ToString();
                    p.IndentLevel = currentIndentLevel - 1;

                    //Debug.WriteLine($"{paragraphText} {previndent} {currentIndentLevel} {predecessors.Count}  => {master.Text}");

                }


                if (previndent > currentIndentLevel)
                {
                    for (var i = previndent; i >= currentIndentLevel; i--)
                    {
                        predecessors.RemoveAt(predecessors.Count - 1);
                    }

                    predecessors.Add(p);
                }
                else if (previndent < currentIndentLevel)
                {
                    predecessors.Add(p);
                }
                else
                {
                    if (lStart && currentIndentLevel == 1)
                    {
                        predecessors.Add(p);
                        lStart = false;
                    }
                    else
                    {
                        predecessors.RemoveAt(predecessors.Count - 1);
                        predecessors.Add(p);
                    }
                }

                previndent = currentIndentLevel;

            }

            foreach (var p in dummy.SubItems)
            {
                slideMetaData.Items.Add(p);
            }


            //Get all the images!!! 
            foreach (var pic in slide.Slide.Descendants< DocumentFormat.OpenXml.Presentation.Picture >())
            {
                //try
                //{
                    //Extract correct image part and extenion
                    var imagePart = ExtractImage(pic, slide, out var extension);

                    var imageItem = new ImageItem
                    {
                        ImageData = imagePart.GetStream(),
                        ImageType = extension == "png" ? LaTexImageType.Png : LaTexImageType.Jpg,
                    };

                    //switch (extension.ToLower())
                    //{
                    //    case "png":
                    //        imageItem.ImageType = LaTexImageType.Png;
                    //        break;
                    //    default:
                    //        imageItem.ImageType = LaTexImageType.Jpg;
                    //        break;
                    //}

                    slideMetaData.Items.Add(imageItem);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine("Error with an image");
                //}
            }

            //if (firstitemdone == true)
            //{
            //    fileresult.WriteLine(@"\end{itemize}");
            //}


        }

        private void PrintParents(OpenXmlElement paragraphParent)
        {
            if (paragraphParent == null)
            {
                return;
            }

            Debug.Print(paragraphParent.LocalName);

            PrintParents(paragraphParent.Parent);
        }

        private bool IsInTable(OpenXmlElement paragraphParent)
        {
            if (paragraphParent == null)
            {
                return false;
            }

            return paragraphParent.LocalName == "tbl" || IsInTable(paragraphParent.Parent);
        }



        private static StringBuilder GetSlideTitle(SlidePart slide)
        {
            var shapes = from shape in slide.Slide.Descendants<Shape>()
                         where IsTitleShape(shape)
                         select shape;
            var paragraphTexttit = new StringBuilder();
            string paragraphSeparator = null;
            foreach (var shape in shapes)
            {
                // Get the text in each paragraph in this shape.
                foreach (var paragraph in shape.TextBody.Descendants<DocumentFormat.OpenXml.Drawing.Paragraph>())
                {
                    // Add a line break.
                    paragraphTexttit.Append(paragraphSeparator);

                    foreach (var text in paragraph.Descendants<DocumentFormat.OpenXml.Drawing.Text>())
                    {
                        paragraphTexttit.Append(text.Text);
                    }

                    paragraphSeparator = "\n";
                }
            }
            return paragraphTexttit;
        }

        // Determines whether the shape is a title shape.
        private static bool IsTitleShape(Shape shape)
        {
            var placeholderShape = shape.NonVisualShapeProperties.ApplicationNonVisualDrawingProperties.GetFirstChild<PlaceholderShape>();
            if (placeholderShape != null && placeholderShape.Type != null && placeholderShape.Type.HasValue)
            {
                switch ((PlaceholderValues)placeholderShape.Type)
                {
                    // Any title shape.
                    case PlaceholderValues.Title:

                    // A centered title.
                    case PlaceholderValues.CenteredTitle:
                        return true;

                    default:
                        return false;
                }
            }
            return false;
        }

        private static ImagePart ExtractImage(DocumentFormat.OpenXml.Presentation.Picture pic, SlidePart slide, out string extension)
        {
            // First, get relationship id of image
            string rId = pic.BlipFill.Blip.Embed.Value;

            ImagePart imagePart = (ImagePart)slide.GetPartById(rId);

            // Get the original file name.
            Debug.WriteLine("$$Image:" + imagePart.Uri.OriginalString);
            extension = "bmp";
            if (imagePart.ContentType.Contains("jpeg") || imagePart.ContentType.Contains("jpg"))
                extension = "jpg";
            else if (imagePart.ContentType.Contains("png"))
                extension = "png";
            return imagePart;
        }


        /// <summary>
        /// Current meta data of the presentation
        /// </summary>
        public PresentationMetaData PresentationMetaData => _presentationMetaData;

        public void Dispose()
        {
            presentationDocument?.Dispose();
        }
    }
}
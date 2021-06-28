using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Bodoconsult.Core.Latex.Enums;
using Bodoconsult.Core.Latex.Interfaces;
using Bodoconsult.Core.Latex.Model;

namespace Bodoconsult.Core.Latex.Test.Helpers
{
    public static class TestHelper
    {
        static TestHelper()
        {
            if (!Directory.Exists(TempPath))
            {
                Directory.CreateDirectory(TempPath);
            }

            var loc = new FileInfo(typeof(TestHelper).Assembly.Location);

            TestDataPath = Path.Combine(loc.Directory.Parent.Parent.Parent.FullName, "TestData");
        }


        /// <summary>
        /// Folder to save test output temporarily
        /// </summary>
        public static string TempPath => @"D:\temp\Latex";


        /// <summary>
        /// Path to the test data
        /// </summary>
        public static string TestDataPath { get; }

        /// <summary>
        /// Fill a presentation with meta data for tetsing
        /// </summary>
        /// <param name="presentation">Presentation</param>
        public static void FillPresentation(PresentationMetaData presentation)
        {

            // Title
            var slide = AddTitle("Main title");
            presentation.Slides.Add(slide);

            // Content 0.1
            slide = AddSlideContentSimple("Content 0.1");
            presentation.Slides.Add(slide);

            // Content 0.2
            slide = AddSlideContentSimple("Content 0.2");
            presentation.Slides.Add(slide);


            //  Section 1
            slide = AddSection("Section 1");
            presentation.Slides.Add(slide);

            // Content 1.1
            slide = AddSlideContentNested("Content 1.1");
            presentation.Slides.Add(slide);

            // Content 1.2
            slide = AddSlideContentNested("Content 1.2");
            presentation.Slides.Add(slide);


            //  Section 2
            slide = AddSection("Section 2");
            presentation.Slides.Add(slide);

            // Content 2.1
            slide = AddSlideContentNested("Content 2.1");
            presentation.Slides.Add(slide);

            // Content 2.2
            slide = AddSlideContentNested("Content 2.2");
            presentation.Slides.Add(slide);

        }

        private static SlideMetaData AddSection(string title)
        {
            var slide = new SlideMetaData
            {
                SlideType = SlideType.Section,
                Title = title

            };

            return slide;
        }

        private static SlideMetaData AddTitle(string title)
        {
            var slide = new SlideMetaData
            {
                SlideType = SlideType.Title,
                Title =title

            };

            return slide;
        }

        private static SlideMetaData AddSlideContentSimple(string title)
        {
            
            var slide = new SlideMetaData
            {
                SlideType = SlideType.Content,
                Title = title
            };

            var p = new LaTexParagraphItem { Text = "Apple" };
            slide.Items.Add(p);

            p = new LaTexParagraphItem { Text = "Banana" };
            slide.Items.Add(p);

            p = new LaTexParagraphItem { Text = "Cherry" };
            slide.Items.Add(p);

            return slide;
        }



        private static SlideMetaData AddSlideContentNested(string title)
        {

            var slide = new SlideMetaData
            {
                SlideType = SlideType.Content,
                Title = title
            };

            var p0 = new LaTexParagraphItem { Text = "Fruits" };
            slide.Items.Add(p0);

            var p = new LaTexParagraphItem { Text = "Apple" };
            p0.SubItems.Add(p);

            p = new LaTexParagraphItem { Text = "Banana" };
            p0.SubItems.Add(p);

            p = new LaTexParagraphItem { Text = "Cherry" };
            p0.SubItems.Add(p);

            p0 = new LaTexParagraphItem { Text = "Vegetables" };
            slide.Items.Add(p0);

            p = new LaTexParagraphItem { Text = "Carrot" };
            p0.SubItems.Add(p);

            p = new LaTexParagraphItem { Text = "Pepper" };
            p0.SubItems.Add(p);

            p = new LaTexParagraphItem { Text = "Cauliflower" };
            p0.SubItems.Add(p);

            return slide;
        }


        public static void PrintPresentation(PresentationMetaData presentation)
        {
            foreach (var slide in presentation.Slides)
            { 


                Debug.Print(slide.Title);

                PrintItems(slide.Items, "");

            }
        }



        public static void PrintItems(IList<ILaTexItem> items, string indent)
        {

            var sortedItems = items.OrderBy(x => x.ShapePosition).ThenBy(x => x.SortId);

            foreach (var p in sortedItems)
            {

                if (p is ILaTexTextItem paragraph)
                {

                    Debug.Print($"{indent}{p.Text} ({paragraph.ShapePosition}/{paragraph.SortId})");

                    PrintItems(paragraph.SubItems, indent + "    ");
                }

                if (p is ILaTexImageItem imageItem)
                {
                    Debug.Print($"{indent}<Image> ({imageItem.ShapePosition}/{imageItem.SortId})");
                }


                if (p is ILaTexTableItem tableItem)
                {

                    Debug.Print($"{indent}<Table> ({tableItem.ShapePosition}/{tableItem.SortId})");

                    var max0 = tableItem.TableData.GetLength(0) - 1;
                    var max1 = tableItem.TableData.GetLength(1) - 1;


                    for (var rowIndex = 0; rowIndex <= max0; rowIndex++)
                    {
                        Debug.Print($"TableRow {rowIndex}");

                        for (var colIndex = 0; colIndex <= max1; colIndex++)
                        {
                            Debug.Print($"{indent}{tableItem.TableData[rowIndex, colIndex]}  ");
                        }
                    }
                }
            }

        }
    }
}

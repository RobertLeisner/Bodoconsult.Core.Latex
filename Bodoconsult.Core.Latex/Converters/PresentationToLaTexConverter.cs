using System;
using System.Collections.Generic;
using System.Linq;
using Bodoconsult.Core.Latex.Enums;
using Bodoconsult.Core.Latex.Interfaces;
using Bodoconsult.Core.Latex.Model;

namespace Bodoconsult.Core.Latex.Converters
{
    /// <summary>
    /// Converts presentation meta data to LaTex
    /// </summary>
    public class PresentationToLaTexConverter : IPresentationToLaTexConverter
    {

        private readonly bool _isSection;


        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="analyzer">Current <see cref="IPresentationAnalyzer"/> implementation</param>
        /// <param name="laTexWriter">Current LaText writer service</param>
        public PresentationToLaTexConverter(IPresentationAnalyzer analyzer, ILatexWriterService laTexWriter)
        {
            Analyzer = analyzer;
            Presentation = analyzer.PresentationMetaData;
            LaTexWriterService = laTexWriter;

            _isSection = Presentation.Slides.Any(x => x.SlideType == SlideType.Section);
        }




        public PresentationMetaData Presentation { get; }



        public ILatexWriterService LaTexWriterService { get; }



        public IPresentationAnalyzer Analyzer { get; }

        /// <summary>
        /// Convert the presentation
        /// </summary>
        public string Convert()
        {

            // Run the analysis first
            Analyzer.Analyse();

            var result = ConvertIntern();

            return result;
        }

        private string ConvertIntern()
        {

            foreach (var slide in Presentation.Slides)
            {

                switch (slide.SlideType)
                {
                    case SlideType.Content:
                        CreateContentSlide(slide);
                        break;
                    case SlideType.Title:
                        CreateTitleSlide(slide);
                        break;
                    case SlideType.Section:
                        CreateSectionSlide(slide);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            LaTexWriterService.Save();

            return LaTexWriterService.Content;
        }

        private void CreateSectionSlide(SlideMetaData slide)
        {
            var p = new ParagraphItem
            {
                Text = slide.Title
            };
            LaTexWriterService.AddSection(p);

            CreateSubItems(slide.Items);
        }

        private void CreateContentSlide(SlideMetaData slide)
        {
            var p = new ParagraphItem
            {
                Text = slide.Title
            };

            if (_isSection)
            {
                LaTexWriterService.AddSubSubSection(p);
            }
            else
            {
                LaTexWriterService.AddSubSection(p);
            }

            CreateSubItems(slide.Items);
        }

        private void CreateTitleSlide(SlideMetaData slide)
        {

            var p = new ParagraphItem
            {
                Text = slide.Title
            };
            LaTexWriterService.AddSection(p);

            CreateSubItems(slide.Items);
        }

        private void CreateSubItems(IList<ILaTexItem> items)
        {
            // All paragraphs
            IList<ILaTexItem> p = items.OfType<ILaTexTextItem>().OfType<ILaTexItem>().ToList();

            if (p.Any())
            {
                LaTexWriterService.AddParagraph(p);
            }

            // All images
            IList<ILaTexImageItem> images = items.OfType<ILaTexImageItem>().ToList();

            foreach (var image in images)
            {
                LaTexWriterService.AddImage(image);
            }

            // All tables


        }
    }
}
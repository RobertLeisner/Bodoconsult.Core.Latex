using Bodoconsult.Core.Latex.Interfaces;
using Bodoconsult.Core.Latex.Model;

namespace Bodoconsult.Core.Latex.Test.Office
{
    public abstract class BasePresentationConverterService
    {

        /// <summary>
        /// Current presentation job
        /// </summary>
        public PresentationJob Job { get; set; }



        public IPresentationConverterService Service { get; set; }

    }
}
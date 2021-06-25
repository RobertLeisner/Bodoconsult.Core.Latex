using System.Collections.Generic;
using Bodoconsult.Core.Latex.Interfaces;

namespace Bodoconsult.Core.Latex.Model
{
    public class LaTextTableItem : ILaTexTableItem
    {
        public string Text { get; set; }

        public IList<ILaTexItem> SubItems { get; } = new List<ILaTexItem>();


        public string[,] TableData { get; set; }
    }
}
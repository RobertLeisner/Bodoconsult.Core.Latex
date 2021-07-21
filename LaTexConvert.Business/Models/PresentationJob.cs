using System;
using System.Collections.Generic;
using System.Text;

namespace LaTexConvert.Business.Models
{

    /// <summary>
    /// Job for converting a presentation into a LaTex file
    /// </summary>
    public class PresentationJob
    {

        /// <summary>
        /// File path of the source presentation file
        /// </summary>
        public string PresentationFilePath { get; set; }

        /// <summary>
        /// File path of the target LaTex file
        /// </summary>
        public string LaTexFilePath { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TechnicalCertificateImageHandler.AppKeys.ApplicationKeys;

namespace TechnicalCertificateImageHandler.Infrastructure.Models
{
    public class WordLabel
    {
        public string Text { get; set; }

        public LabelTypes Type { get; set; }
    }
}

using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudVisionTestApp.Model
{
    public class MatchedAnnotation
    {
        public Word MatchedWord { get; set; }
        public string TargetValue { get; set; }
        public int TargetValueOrder { get; set; }
    }
}

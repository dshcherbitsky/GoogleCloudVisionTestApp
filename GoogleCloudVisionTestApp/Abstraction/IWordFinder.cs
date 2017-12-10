using Google.Cloud.Vision.V1;
using GoogleCloudVisionTestApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudVisionTestApp.Abstraction
{
    public interface IWordFinder
    {
        IList<Word> FindWords(MatchedAnnotation word);
    }
}

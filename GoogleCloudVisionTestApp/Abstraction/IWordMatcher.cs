using Google.Cloud.Vision.V1;
using GoogleCloudVisionTestApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudVisionTestApp.Abstraction
{
    /// <summary>
    /// Represents objects that can be used for calculating loan payment.
    /// </summary>
    public interface IWordMatcher
    {
        List<MatchedAnnotation> GetMatchedWords(IList<string> targetMatches);
    }
}

using Google.Cloud.Vision.V1;
using System.Collections.Generic;

namespace TechnicalCertificateImgHandler.Abstractions
{
    /// <summary>
    /// Represents objects that can be used for calculating loan payment.
    /// </summary>
    public interface IWordMatcher
    {
        List<MatchedAnnotation> GetMatchedWords(IList<string> targetMatches);
    }
}

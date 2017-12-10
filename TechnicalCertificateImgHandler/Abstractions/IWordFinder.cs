using Google.Cloud.Vision.V1;
using System.Collections.Generic;

namespace TechnicalCertificateImgHandler.Abstractions
{
    public interface IWordFinder
    {
        IList<Word> FindWords(MatchedAnnotation word);
    }
}

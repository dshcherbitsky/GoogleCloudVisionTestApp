using Google.Cloud.Vision.V1;
using System.Collections.Generic;
using static TechnicalCertificateImgHandler.AppKeys.ApplicationKeys;

namespace TechnicalCertificateImgHandler.Abstractions
{
    public interface IWordFinder
    {
        IList<Word> FindWords(Word word, LabelTypes labelType);
    }
}

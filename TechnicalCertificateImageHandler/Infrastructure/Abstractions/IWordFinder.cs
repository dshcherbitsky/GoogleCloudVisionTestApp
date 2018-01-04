using Google.Cloud.Vision.V1;
using System.Collections.Generic;
using static TechnicalCertificateImageHandler.AppKeys.ApplicationKeys;

namespace TechnicalCertificateImageHandler.Infrastructure.Abstractions
{
    /// <summary>
    /// Represents objects that can be used to find words by label.
    /// </summary>
    public interface IWordFinder
    {
        IList<Word> FindWords(Word word, LabelTypes labelType);
    }
}

using Google.Cloud.Vision.V1;
using System.Collections.Generic;

namespace TechnicalCertificateImageHandler.Infrastructure.Abstractions
{
    /// <summary>
    /// Represents objects that can be used for getting word by label text.
    /// </summary>
    public interface IWordMatcher
    {
        Word GetMatchedLabel(string label);
    }
}

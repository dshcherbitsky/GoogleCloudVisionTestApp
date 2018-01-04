using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalCertificateImageHandler.Infrastructure.Abstractions;

namespace TechnicalCertificateImageHandler.Infrastructure
{
    public class WordMatcher : IWordMatcher
    {
        private readonly TextAnnotation annotationContext;

        public WordMatcher(TextAnnotation annotationContext)
        {
            this.annotationContext = annotationContext;
        }

        public Word GetMatchedLabel(string label)
        {
            foreach (var block in annotationContext.Pages[0].Blocks)
            {
                foreach (var paragraph in block.Paragraphs)
                {
                    foreach (var word in paragraph.Words)
                    {
                        string value = string.Join(string.Empty, word.Symbols.Select(sym => sym.Text));
                        if (value == label)
                        {
                            return word;
                        }
                    }
                }
            }

            return null;
        }
    }
}
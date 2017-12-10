using Google.Cloud.Vision.V1;
using GoogleCloudVisionTestApp.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudVisionTestApp.Model
{
    public class WordMatcher : IWordMatcher
    {
        private readonly TextAnnotation annotationContext;

        public WordMatcher(TextAnnotation annotationContext)
        {
            this.annotationContext = annotationContext;
        }
        public List<MatchedAnnotation> GetMatchedWords(IList<string> targetMatches)
        {
            List<MatchedAnnotation> result = new List<MatchedAnnotation>();

            foreach (var type in targetMatches)
            {
                foreach (var block in annotationContext.Pages[0].Blocks)
                {
                    foreach (var paragraph in block.Paragraphs)
                    {
                        foreach (var word in paragraph.Words)
                        {
                            string value = string.Join(string.Empty, word.Symbols.Select(sym => sym.Text));
                            if (value == type)
                            {
                                result.Add(new MatchedAnnotation() {
                                    MatchedWord = word,
                                    TargetValue = type,
                                    TargetValueOrder = targetMatches.IndexOf(type)
                                });

                                goto BreakLoops;
                            }
                        }
                    }
                }
                BreakLoops:;
            }

            return result;
        }
    }
}

﻿using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalCertificateImgHandler.Abstractions;

namespace TechnicalCertificateImgHandler
{
    public class WordMatcher : IWordMatcher
    {
        private readonly TextAnnotation annotationContext;

        public WordMatcher(TextAnnotation annotationContext)
        {
            this.annotationContext = annotationContext;
        }

        public List<MatchedAnnotation> GetMatchedLabels(IList<string> labels)
        {
            List<MatchedAnnotation> result = new List<MatchedAnnotation>();

            foreach (var type in labels)
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
                                    TargetValueOrder = labels.IndexOf(type)
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
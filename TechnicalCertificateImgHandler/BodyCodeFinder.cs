using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using TechnicalCertificateImgHandler.Abstractions;

namespace TechnicalCertificateImgHandler
{
    public class BodyCodeFinder : IWordFinder
    {
        private readonly TextAnnotation annotationContext;

        public BodyCodeFinder(TextAnnotation annotationContext)
        {
            this.annotationContext = annotationContext;
        }

        public IList<Word> FindWords(MatchedAnnotation word)
        {
            double wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            double wordLenght = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            double Y1 = 0;
            double Y2 = 0;
            double X = word.MatchedWord.BoundingBox.Vertices[1].X;
            //Set "Karosserie" label coordinates range.
            if (word.TargetValueOrder == 0)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.5); 
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                X = X + Math.Round(wordLenght * 1.25);
            }
            //Set "Carrosserie" label coordinates range.
            if (word.TargetValueOrder == 1)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.3);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1.8);
                X = X + Math.Round(wordLenght * 1.1);
            }
            //Set "Carrozzeria" label coordinates range.
            if (word.TargetValueOrder == 2)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2.3);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1);
                X = X + Math.Round(wordLenght * 1.01);
            }
            //Set "Carossaria" label coordinates range.
            if (word.TargetValueOrder == 3)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3.2);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.5); ;
                X = X + Math.Round(wordLenght * 1.25);
            }

            IList<Word> bodyCodeMatchedWords = new List<Word>();

            foreach (var block in annotationContext.Pages[0].Blocks)
            {
                foreach (var paragraph in block.Paragraphs)
                {
                    foreach (var w in paragraph.Words)
                    {
                        int blokY1 = w.BoundingBox.Vertices[0].Y;
                        int blokY2 = w.BoundingBox.Vertices[3].Y;
                        int blokX1 = w.BoundingBox.Vertices[0].X;
                        int blokX2 = w.BoundingBox.Vertices[1].X;
                        if (blokY1 > Y1 && blokY2 < Y2 && blokX2 > X)
                        {
                            bodyCodeMatchedWords.Add(w);
                        }
                    }
                }
            }

            return bodyCodeMatchedWords;
        }
    }
}

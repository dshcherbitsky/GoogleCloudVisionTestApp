using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using TechnicalCertificateImgHandler.Abstractions;

namespace TechnicalCertificateImgHandler
{
    public class SONumFinder : IWordFinder
    {
        private readonly TextAnnotation annotationContext;

        public SONumFinder(TextAnnotation annotationContext)
        {
            this.annotationContext = annotationContext;
        }

        public IList<Word> FindWords(MatchedAnnotation word)
        {
            double wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            double wordLenght = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            double Y1 = 0;
            double Y2 = 0;
            double X1 = word.MatchedWord.BoundingBox.Vertices[1].X;
            double X2 = X1;
            //Set "Schild" label coordinates range.
            if (word.TargetValueOrder == 0)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                X1 = X1 + Math.Round(wordLenght * 0.5);
                X2 = X2 + Math.Round(wordLenght * 9); ;
            }
            //Set "Plaque" label coordinates range.
            if (word.TargetValueOrder == 1)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2);
                X1 = X1 + Math.Round(wordLenght * 0.5);
                X2 = X2 + Math.Round(wordLenght * 9); ;
            }
            //Set "Targa" label coordinates range.
            if (word.TargetValueOrder == 2)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1);
                X1 = X1 + Math.Round(wordLenght * 0.5);
                X2 = X2 + Math.Round(wordLenght * 9); ;
            }
            //Set "Numer" label coordinates range.
            if (word.TargetValueOrder == 3)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X1 = X1 + Math.Round(wordLenght * 0.5);
                X2 = X2 + Math.Round(wordLenght * 9); ;
            }

            IList<Word> receptionNumMatchedWords = new List<Word>();

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
                        if (blokY1 > Y1 && blokY2 < Y2 && blokX1 > X1 && blokX2 < X2)
                        {
                            receptionNumMatchedWords.Add(w);
                        }
                    }
                }
            }

            return receptionNumMatchedWords;
        }
    }
}

using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using TechnicalCertificateImgHandler.Abstractions;

namespace TechnicalCertificateImgHandler
{
    public class TypeFinder : IWordFinder
    {
        private readonly TextAnnotation annotationContext;

        public TypeFinder(TextAnnotation annotationContext)
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
            //Set "Art" label coordinates range.
            if (word.TargetValueOrder == 0)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                X = X + Math.Round(wordLenght * 10);
            }
            //Set "Fahrzeugs" label coordinates range.
            if (word.TargetValueOrder == 1)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                X = X + Math.Round(wordLenght * 2.2);
            }
            //Set "Genre" label coordinates range.
            if (word.TargetValueOrder == 2)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2);
                X = X + Math.Round(wordLenght * 2.5);
            }
            //Set "véhicule" label coordinates range.
            if (word.TargetValueOrder == 3)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2);
                X = X + Math.Round(wordLenght * 0.6);
            }
            //Set "Genere" label coordinates range.
            if (word.TargetValueOrder == 4)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight);
                X = X + Math.Round(wordLenght * 2.3);
            }
            //Set "veicolo" label coordinates range.
            if (word.TargetValueOrder == 5)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight);
                X = X + Math.Round(wordLenght * 0.8);
            }
            //Set "Gener" label coordinates range.
            if (word.TargetValueOrder == 6)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X = X + Math.Round(wordLenght * 2.5);
            }
            //Set "vehichel" label coordinates range.
            if (word.TargetValueOrder == 7)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X = X + Math.Round(wordLenght * 0.4);
            }

            IList<Word> typeMatchedWords = new List<Word>();

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
                            typeMatchedWords.Add(w);
                        }
                    }
                }
            }

            return typeMatchedWords;
        }
    }
}

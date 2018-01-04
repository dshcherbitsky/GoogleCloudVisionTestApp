using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using TechnicalCertificateImgHandler.Abstractions;
using static TechnicalCertificateImgHandler.AppKeys.ApplicationKeys;

namespace TechnicalCertificateImgHandler
{
    public class SONumFinder : IWordFinder
    {
        private readonly TextAnnotation annotationContext;

        public SONumFinder(TextAnnotation annotationContext)
        {
            this.annotationContext = annotationContext;
        }

        public IList<Word> FindWords(Word word, LabelTypes labelType)
        {
            IList<Word> words = new List<Word>();

            double wordHeight = word.BoundingBox.Vertices[3].Y - word.BoundingBox.Vertices[0].Y;
            double wordLenght = word.BoundingBox.Vertices[1].X - word.BoundingBox.Vertices[0].X;
            double Y1 = 0;
            double Y2 = 0;
            double X1 = word.BoundingBox.Vertices[1].X;
            double X2 = X1;

            switch (labelType)
            {
                //Set "Schild" label coordinates range.
                case LabelTypes.Label_1_1:
                    Y1 = word.BoundingBox.Vertices[0].Y;
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                    X1 = X1 + Math.Round(wordLenght * 0.5);
                    X2 = X2 + Math.Round(wordLenght * 9);
                    break;
                //Set "Plaque" label coordinates range.
                case LabelTypes.Label_2_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2);
                    X1 = X1 + Math.Round(wordLenght * 0.5);
                    X2 = X2 + Math.Round(wordLenght * 9);
                    break;
                //Set "Targa" label coordinates range.
                case LabelTypes.Label_3_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1);
                    X1 = X1 + Math.Round(wordLenght * 0.5);
                    X2 = X2 + Math.Round(wordLenght * 9);
                    break;
                //Set "Numer" label coordinates range.
                case LabelTypes.Label_4_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3);
                    Y2 = word.BoundingBox.Vertices[3].Y;
                    X1 = X1 + Math.Round(wordLenght * 0.5);
                    X2 = X2 + Math.Round(wordLenght * 9);
                    break;
                default:
                    // TODO Add log information
                    return words;
            }

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
                            words.Add(w);
                        }
                    }
                }
            }

            return words;
        }
    }
}

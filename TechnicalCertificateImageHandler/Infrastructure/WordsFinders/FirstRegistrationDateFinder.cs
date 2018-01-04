using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using TechnicalCertificateImageHandler.Infrastructure.Abstractions;
using static TechnicalCertificateImageHandler.AppKeys.ApplicationKeys;

namespace TechnicalCertificateImageHandler.Infrastructure.WordsFinders
{
    public class FirstRegistrationDateFinder : IWordFinder
    {
        private readonly TextAnnotation annotationContext;

        public FirstRegistrationDateFinder(TextAnnotation annotationContext)
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
                //Set "Inverkehrsetzung" label coordinates range.
                case LabelTypes.Label_1_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.5);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2.2);
                    X1 = X1 + Math.Round(wordLenght * 0.3);
                    X2 = X2 + Math.Round(wordLenght * 2.75);
                    break;
                //Set "mise" label coordinates range.
                case LabelTypes.Label_2_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.3);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1.2);
                    X1 = X1 + Math.Round(wordLenght * 4.3);
                    X2 = X2 + Math.Round(wordLenght * 15.2);
                    break;
                //Set "circulation" label coordinates range.
                case LabelTypes.Label_2_2:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.3);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1.2);
                    X1 = X1 + Math.Round(wordLenght * 0.3);
                    X2 = X2 + Math.Round(wordLenght * 4.8);
                    break;
                //Set "messa" label coordinates range.
                case LabelTypes.Label_3_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.5);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.7);
                    X1 = X1 + Math.Round(wordLenght * 3);
                    X2 = X2 + Math.Round(wordLenght * 10.9);
                    break;
                //Set "circolazione" label coordinates range.
                case LabelTypes.Label_3_2:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.5);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.7);
                    X1 = X1 + Math.Round(wordLenght * 0.2);
                    X2 = X2 + Math.Round(wordLenght * 4.1);
                    break;
                //Set "entrada" label coordinates range.
                case LabelTypes.Label_4_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2.5);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.2);
                    X1 = X1 + Math.Round(wordLenght * 2.2);
                    X2 = X2 + Math.Round(wordLenght * 8.3);
                    break;
                //Set "circulaziun" label coordinates range.
                case LabelTypes.Label_4_2:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2.5);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.2);
                    X1 = X1 + Math.Round(wordLenght * 0.2);
                    X2 = X2 + Math.Round(wordLenght * 4.8);
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

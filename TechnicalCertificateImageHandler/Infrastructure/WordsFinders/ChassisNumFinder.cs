using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using TechnicalCertificateImageHandler.Infrastructure.Abstractions;
using static TechnicalCertificateImageHandler.AppKeys.ApplicationKeys;

namespace TechnicalCertificateImageHandler.Infrastructure.WordsFinders
{
    public class ChassisNumFinder : IWordFinder
    {
        private readonly TextAnnotation annotationContext;

        public ChassisNumFinder(TextAnnotation annotationContext)
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
            double X = word.BoundingBox.Vertices[1].X;

            switch (labelType)
            {
                //Set "Fahrgestell" label coordinates range.
                case LabelTypes.Label_1_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.5);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                    X = X + Math.Round(wordLenght * 1.1);
                    break;
                //Set "Nr." label coordinates range.
                case LabelTypes.Label_1_2:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.5);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                    X = X + Math.Round(wordLenght * 3.3);
                    break;
                //Set "Chassis" label coordinates range.
                case LabelTypes.Label_2_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.5);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2);
                    X = X + Math.Round(wordLenght * 2.2);
                    break;
                //Set "Telaio" label coordinates range.
                case LabelTypes.Label_3_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2.5);
                    Y2 = word.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1);
                    X = X + Math.Round(wordLenght * 2.75);
                    break;
                //Set "Schassis" label coordinates range.
                case LabelTypes.Label_4_1:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3);
                    Y2 = word.BoundingBox.Vertices[3].Y;
                    X = X + Math.Round(wordLenght * 1.48);
                    break;
                //Set "nr." label coordinates range.
                case LabelTypes.Label_4_2:
                    Y1 = word.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3);
                    Y2 = word.BoundingBox.Vertices[3].Y;
                    X = X + Math.Round(wordLenght * 4.8);
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
                        if (blokY1 > Y1 && blokY2 < Y2 && blokX2 > X)
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

using Google.Cloud.Vision.V1;
using GoogleCloudVisionTestApp.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudVisionTestApp.Model
{
    public class MatriculNumFinder : IWordFinder
    {
        private readonly TextAnnotation annotationContext;

        public MatriculNumFinder(TextAnnotation annotationContext)
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
            //Set "Stammnummer" label coordinates range.
            if (word.TargetValueOrder == 0)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2.4);
                X1 = X1 + Math.Round(wordLenght * 0.45);
                X2 = X2 + Math.Round(wordLenght * 3);
            }
            //Set "matricule" label coordinates range.
            if (word.TargetValueOrder == 1)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1.6);
                X1 = X1 + Math.Round(wordLenght * 1.35);
                X2 = X2 + Math.Round(wordLenght * 6.2);
            }
            //Set "matricola" label coordinates range.
            if (word.TargetValueOrder == 2)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.5);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.7);
                X1 = X1 + Math.Round(wordLenght * 1.1);
                X2 = X2 + Math.Round(wordLenght * 6.05);
            }
            //Set "matricla" label coordinates range.
            if (word.TargetValueOrder == 3)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2.5);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X1 = X1 + Math.Round(wordLenght * 1.2);
                X2 = X2 + Math.Round(wordLenght * 7.1);
            }

            IList<Word> matriculeNumMatchedWords = new List<Word>();

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
                        if (blokY2 > Y1 && blokY1 < Y2 && blokX1 > X1 && blokX2 < X2)
                        {
                            matriculeNumMatchedWords.Add(w);
                        }
                    }
                }
            }

            return matriculeNumMatchedWords;
        }
    }
}

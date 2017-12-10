using Google.Cloud.Vision.V1;
using GoogleCloudVisionTestApp.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudVisionTestApp.Model
{
    public class FirstRegistrationDateFinder : IWordFinder
    {
        private readonly TextAnnotation annotationContext;

        public FirstRegistrationDateFinder(TextAnnotation annotationContext)
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
            //Set "Inverkehrsetzung" label coordinates range.
            if (word.TargetValueOrder == 0)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + 2 * wordHeight;
                X1 = X1 + Math.Round(wordLenght * 0.3);
                X2 = X2 + Math.Round(wordLenght * 2.75);
            }
            //Set "mise" label coordinates range.
            if (word.TargetValueOrder == 1)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.8);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + wordHeight;
                X1 = X1 + Math.Round(wordLenght * 4.3);
                X2 = X2 + Math.Round(wordLenght * 15.2);
            }
            //Set "circulation" label coordinates range.
            if (word.TargetValueOrder == 2)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.8);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + wordHeight;
                X1 = X1 + Math.Round(wordLenght * 0.3);
                X2 = X2 + Math.Round(wordLenght * 4.8);
            }
            //Set "messa" label coordinates range.
            if (word.TargetValueOrder == 3)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.5);
                X1 = X1 + Math.Round(wordLenght * 3);
                X2 = X2 + Math.Round(wordLenght * 10.9);
            }
            //Set "circolazione" label coordinates range.
            if (word.TargetValueOrder == 4)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + +Math.Round(wordHeight * 0.5);
                X1 = X1 + Math.Round(wordLenght * 0.2);
                X2 = X2 + Math.Round(wordLenght * 4.1);
            }
            //Set "entrada" label coordinates range.
            if (word.TargetValueOrder == 5)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - 2 * wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X1 = X1 + Math.Round(wordLenght * 2.2);
                X2 = X2 + Math.Round(wordLenght * 8.3);
            }
            //Set "{" label coordinates range.
            if (word.TargetValueOrder == 6)
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - 2 * wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X1 = X1 + Math.Round(wordLenght * 0.2);
                X2 = X2 + Math.Round(wordLenght * 4.8);
            }

            IList<Word> firstRegistrationDateMatchedWords = new List<Word>();

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
                            firstRegistrationDateMatchedWords.Add(w);
                        }
                    }
                }
            }

            return firstRegistrationDateMatchedWords;
        }
    }
}

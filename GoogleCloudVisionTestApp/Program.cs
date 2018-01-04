using CarAuktion.OCR;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using GoogleCloudVisionTestApp.Abstraction;
using GoogleCloudVisionTestApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoogleCloudVisionTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var image = Image.FromFile(@"C:\Users\home\Desktop\CarAuto\img2.jpg");
            //var client = ImageAnnotatorClient.Create();
            //var response = client.DetectText(image);

            //////////////////////////////////////////////////////////////////////////
            // \d{3}\s*[.]\s*\d{3}\s*[.]\s*\d{3}
            //[A-Z0-9]{4}\s*[A-Z0-9]{2}
            //////////////////////////////////////////////////////////////////////////

            var image1 = Image.FromFile(@"C:\Users\home\Desktop\CarAuto\img1.jpg");
            var image2 = Image.FromFile(@"C:\Users\home\Desktop\CarAuto\img2.jpg");
            var image3 = Image.FromFile(@"C:\Users\home\Desktop\CarAuto\img3_rotated.jpg");
            var image4 = Image.FromFile(@"C:\Users\home\Desktop\CarAuto\new_tp_3.jpg");
            var image5 = Image.FromFile(@"C:\Users\home\Desktop\CarAuto\new_tp_4.jpg");
            var client = ImageAnnotatorClient.Create();          


            IList<Image> images = new List<Image>() { image1, image2, image3, image4, image5 };

            foreach (var img in images)
            {
                var response = client.DetectDocumentText(img);

                TechnicalCertificateImageHandler.Infrastructure.Abstractions.IDocumentProcesser docProcesser = new TechnicalCertificateImageHandler.TechnicalCertificateImageProcesser(response);

                TechnicalCertificateImageHandler.Infrastructure.Models.VehicleCertificateContentDTO result = docProcesser.Process();
                
                Console.WriteLine($"processed file# {images.IndexOf(img) + 1}");
                Console.WriteLine("   - SO Num:         " + result.SONo);
                Console.WriteLine("   - Reception Num:  " + result.ReceptionNum);
                Console.WriteLine("   - Type:           " + result.Type);
                Console.WriteLine("   - Type code:      " + result.TypeCode);
                Console.WriteLine("   - Body:           " + result.Body);
                Console.WriteLine("   - Body code:      " + result.BodyCode);
                Console.WriteLine("   - Mark Model:     " + result.MarkAndModel);
                Console.WriteLine("   - Chassis Num:    " + result.ChassisNum);
                Console.WriteLine("   - Color:          " + result.Color);
                Console.WriteLine("   - MatriculNum:    " + result.MatriculNum);
                Console.WriteLine("   - Fst Reg Date:   " + result.FirstRegistrationDate);
                Console.WriteLine();
            }

            Console.ReadKey();




            //IDocumentProcesser docProcesser = new TechnicalCertificateImageProcesser(new WordMatcher(response),
            //                        new TypeFinder(response),
            //                        new MarkAndModelFinder(response),
            //                        new ChassisNumFinder(response),
            //                        new BodyCodeFinder(response),
            //                        new ColorFinder(response),
            //                        new MatriculNumFinder(response),
            //                        new FirstRegistrationDateFinder(response));
            //VehicleCertificateContentDTO result = docProcesser.Process();



            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // TEST First Registration Date block
            List<string> targetFirstRegistrationDateAnnotations = new List<string>() { "Inverkehrsetzung", "mise", "circulation", "messa", "circolazione", "entrada", "circulaziun" };
            //foreach (var firstRegistrationDate in targetFirstRegistrationDateAnnotations)
            //{
            //    var list = new List<string>();
            //    list.Add(firstRegistrationDate);
            //    var targetFirstRegistrationDateMatchedWord = GetMatchedWord(response1.Pages[0], list);
            //    if (targetFirstRegistrationDateMatchedWord == null)
            //    {
            //        Console.WriteLine(firstRegistrationDate + ": doesn't matched!!!");
            //        continue;
            //    }
            //    targetFirstRegistrationDateMatchedWord.TargetValueOrder = targetFirstRegistrationDateAnnotations.IndexOf(firstRegistrationDate);
            //    var matchedWords = FindFirstRegistrationDateWords(response1.Pages[0], targetFirstRegistrationDateMatchedWord);
            //    if (matchedWords.Count != 0)
            //    {
            //        var typeSplitWords = GetWordsText(matchedWords);
            //        string value = typeSplitWords;
            //        Console.WriteLine($"{firstRegistrationDate} First Registration Date: {value}");
            //    }
            //    else
            //    {
            //        Console.WriteLine(firstRegistrationDate + ": doesn't find words!!!");
            //    }
            //}



            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // TEST Matricule Number block
            List<string> targetMatriculeNumAnnotations = new List<string>() { "Stammnummer", "matricule", "matricola", "matricla" };
            //foreach (var matriculeNum in targetMatriculeNumAnnotations)
            //{
            //    var list = new List<string>();
            //    list.Add(matriculeNum);
            //    var targetMatriculeNumMatchedWord = GetMatchedWord(response1.Pages[0], list);
            //    if (targetMatriculeNumMatchedWord == null)
            //    {
            //        Console.WriteLine(matriculeNum + ": doesn't matched!!!");
            //        continue;
            //    }
            //    targetMatriculeNumMatchedWord.TargetValueOrder = targetMatriculeNumAnnotations.IndexOf(matriculeNum);
            //    var matchedWords = FindMatriculeNumWords(response1.Pages[0], targetMatriculeNumMatchedWord);
            //    if (matchedWords.Count != 0)
            //    {
            //        var typeSplitWords = GetWordsText(matchedWords);
            //        string value = typeSplitWords;
            //        Console.WriteLine($"Matricule Number: {value}");
            //    }
            //    else
            //    {
            //        Console.WriteLine(matriculeNum + ": doesn't find words!!!");
            //    }
            //}

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // TEST Color block

            List<string> targetColorAnnotations = new List<string>() { "Farbe", "Couleur", "Colore", "Colur" };
            //foreach (var color in targetColorAnnotations)
            //{
            //    var list = new List<string>();
            //    list.Add(color);
            //    var targetColorMatchedWord = GetMatchedWord(response1.Pages[0], list);
            //    if (targetColorMatchedWord == null)
            //    {
            //        Console.WriteLine(color + ": doesn't matched!!!");
            //        continue;
            //    }
            //    targetColorMatchedWord.TargetValueOrder = targetColorAnnotations.IndexOf(color);
            //    var matchedWords = FindColorWords(response1.Pages[0], targetColorMatchedWord);
            //    if (matchedWords.Count != 0)
            //    {
            //        var typeSplitWords = GetWordsText(matchedWords);
            //        string colorValue = typeSplitWords;
            //        Console.WriteLine($"color: {colorValue}");
            //    }
            //    else
            //    {
            //        Console.WriteLine(color + ": doesn't find words!!!");
            //    }
            //}

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // TEST Body Code block

            List<string> targetBodyCodeAnnotations = new List<string>() { "Karosserie", "Carrosserie", "Carrozzeria", "Carossaria" };
            //foreach (var targetBodyCode in targetBodyCodeAnnotations)
            //{
            //    var list = new List<string>();
            //    list.Add(targetBodyCode);
            //    var targetBodyCodeMatchedWord = GetMatchedWord(response1.Pages[0], list);
            //    if (targetBodyCodeMatchedWord == null)
            //    {
            //        Console.WriteLine(targetBodyCode + ": doesn't matched!!!");
            //        continue;
            //    }
            //    targetBodyCodeMatchedWord.TargetValueOrder = targetBodyCodeAnnotations.IndexOf(targetBodyCode);
            //    var matchedWords = FindBodyCodeWords(response1.Pages[0], targetBodyCodeMatchedWord);
            //    if (matchedWords.Count != 0)
            //    {
            //        var typeSplitWords = GetWordsText(matchedWords);
            //        string code = GetTypeCodeValue(typeSplitWords);
            //        string body = GetTypeValue(typeSplitWords);

            //        Console.WriteLine($"{targetBodyCode} - body: {body} | code: {code}");
            //    }
            //    else
            //    {
            //        Console.WriteLine(targetBodyCode + ": doesn't find words!!!");
            //    }
            //}

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // TEST ChassisNum Detection block

            List<string> targetChassisNumAnnotations = new List<string>() { "Fahrgestell", "Nr.", "Chassis", "Telaio", "Schassis", "nr." };
            //foreach (var chassisNum in targetChassisNumAnnotations)
            //{
            //    var list = new List<string>();
            //    list.Add(chassisNum);
            //    var targetChassisNumMatchedWord = GetMatchedWord(response1.Pages[0], list);
            //    if (targetChassisNumMatchedWord == null)
            //    {
            //        Console.WriteLine(chassisNum + ": doesn't matched!!!");
            //        continue;
            //    }
            //    targetChassisNumMatchedWord.TargetValueOrder = targetChassisNumAnnotations.IndexOf(chassisNum);
            //    var matchedWords = FindChassisNumWords(response1.Pages[0], targetChassisNumMatchedWord);
            //    if (matchedWords.Count != 0)
            //    {
            //        var typeSplitWords = GetWordsText(matchedWords);
            //        string num = typeSplitWords;

            //        Console.WriteLine($"{chassisNum} - chassisNum: {num}");
            //    }
            //    else
            //    {
            //        Console.WriteLine(chassisNum + ": doesn't find words!!!");
            //    }
            //}

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // TEST Mark and Model Detection block

            //List<string> targetMarkAndModelAnnotations = new List<string>() { "Marke", "Typ", "Marque", "type", "Marca", "tipo", "Marca", "tip" };
            List<string> targetMarkAndModelAnnotations = new List<string>() { "Marke", "Typ", "Marque", "type", "Marca", "tipo" };
            //int index = 0;
            //foreach (var targetMarkAndModel in targetMarkAndModelAnnotations)
            //{
            //    var list = new List<string>();
            //    list.Add(targetMarkAndModel);
            //    var targetMarkAndModelMatchedWord = GetMatchedWord(response1.Pages[0], list);
            //    if (targetMarkAndModelMatchedWord == null)
            //    {
            //        Console.WriteLine(targetMarkAndModel + ": doesn't matched!!!");
            //        index++;
            //        continue;
            //    }
            //    targetMarkAndModelMatchedWord.TargetValueOrder = index;
            //    var matchedWords = FindMarkAndModelWords(response1.Pages[0], targetMarkAndModelMatchedWord);
            //    if (matchedWords.Count != 0)
            //    {
            //        var typeSplitWords = GetWordsText(matchedWords);
            //        string markAndModel = typeSplitWords;
            //        Console.WriteLine($"{targetMarkAndModel} - mark and model: {markAndModel}");
            //    }
            //    else
            //    {
            //        Console.WriteLine(targetMarkAndModel + ": doesn't find words!!!");
            //    }
            //    index++;
            //}

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // TEST TYPE and CODE Detection block

            List<string> targetTypeAnnotations = new List<string>() { "Art", "Fahrzeugs", "Genre", "véhicule", "Genere", "veicolo", "Gener", "vehichel" };
            //foreach (var targetType in targetTypeAnnotations)
            //{
            //    var list = new List<string>();
            //    list.Add(targetType);
            //    var typeMatchedWord = GetMatchedWord(response1.Pages[0], list);
            //    if (typeMatchedWord == null)
            //    {
            //        Console.WriteLine(targetType + ": doesn't matched!!!");
            //        continue;
            //    }
            //    typeMatchedWord.TargetValueOrder = targetTypeAnnotations.IndexOf(targetType);
            //    var matchedWords = FindTypeWords(response1.Pages[0], typeMatchedWord);
            //    if (matchedWords.Count != 0)
            //    {
            //        var typeSplitWords = GetWordsText(matchedWords);
            //        string code = GetTypeCodeValue(typeSplitWords);
            //        string type = GetTypeValue(typeSplitWords);

            //        Console.WriteLine($"{targetType} - type: {type} | code: {code}");
            //    }
            //    else
            //    {
            //        Console.WriteLine(targetType + ": doesn't find words!!!");
            //    }
            //}

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            
           // //List<string> targetBodyCodeAnnotations = new List<string>() { "Karosserie", "Carrosserie", "Carrozzeria", "Carossaria" };
           // var bodyCodeMatchedWord = GetMatchedWord(response1.Pages[0], targetBodyCodeAnnotations);
           // var bodyCodeMatchedParagraphs = FindBodyCodeParagraphs(response1.Pages[0], bodyCodeMatchedWord);
           // var bodyCodeFields = GetTypeFields(bodyCodeMatchedParagraphs);

           // //List<string> targetColorAnnotations = new List<string>() { "Farbe", "Couleur", "Colore", "Colur" };
           // var colorMatchedWord = GetMatchedWord(response1.Pages[0], targetColorAnnotations);
           // var colorMatchedParagraphs = FindColorParagraphs(response1.Pages[0], colorMatchedWord);
           // var colorFields = GetTypeFields(colorMatchedParagraphs);

           //// List<string> targetMarkAndModelAnnotations = new List<string>() { "Marke", "und", "Typ", "Marque", "type", "Marca", "tipo", "Marca", "tig" };
           // var markAndModelMatchedWord = GetMatchedWord(response1.Pages[0], targetMarkAndModelAnnotations);
           // var markAndModelMatchedParagraphs = FindMarkAndModelWords(response1.Pages[0], markAndModelMatchedWord);
           // var markAndModelFields = GetWordsText(markAndModelMatchedParagraphs);

           // //List<string> targetChassisNumAnnotations = new List<string>() { "Fahrgestell", "Nr.", "Chassis", "Telaio", "Schassis", "nr." };
           // var chassisNumMatchedWord = GetMatchedWord(response1.Pages[0], targetChassisNumAnnotations);
           // var chassisNumMatchedWords = FindChassisNumWords(response1.Pages[0], chassisNumMatchedWord);
           // var chassisNumFields = GetWordsText(chassisNumMatchedWords);

           // //List<string> targetMatriculeNumAnnotations = new List<string>() { "Stammnummer", "matricule", "matricola", "matricla"};
           // var matriculeNumMatchedWord = GetMatchedWord(response1.Pages[0], targetMatriculeNumAnnotations);
           // var matriculeNumMatchedWords = FindMatriculeNumWords(response1.Pages[0], matriculeNumMatchedWord);
           // var matriculeNumFields = GetWordsText(matriculeNumMatchedWords);

           // //List<string> targetFirstRegistrationDateAnnotations = new List<string>() { "Inverkehrsetzung", "mise", "circulation", "messa", "circolazione", "entrada", "circulaziun" };
           // var firstRegistrationDateMatchedWord = GetMatchedWord(response1.Pages[0], targetFirstRegistrationDateAnnotations);
           // var firstRegistrationDateMatchedWords = FindFirstRegistrationDateWords(response1.Pages[0], firstRegistrationDateMatchedWord);
           // var firstRegistrationDateFields = GetWordsText(firstRegistrationDateMatchedWords);

            Console.ReadKey();

        //IAnnotationProcesser processer = new TechnicalPassportProcesser(response, new AnnotationMatcher(), new AnnotationFinder());
        //ExecutionResult result = processer.Process();



        }

        private static string GetTypeCodeValue(string splitWords)
        {
            var code = Regex.Match(splitWords, "\\d+").Value;
            return code;
        }

        private static string GetTypeValue(string splitWords)
        {
            var codeValue = GetTypeCodeValue(splitWords);
            var type = splitWords.Replace("Code", string.Empty);
            if (!string.IsNullOrEmpty(codeValue))
            {
                type = type.Replace(codeValue, string.Empty);
            }
            return type.Trim();
        }

        private static MatchedAnnotation GetMatchedWord(Page page, IList<string> targetMatches)
        {
            foreach (var type in targetMatches)
            {
                foreach (var block in page.Blocks)
                {
                    foreach (var paragraph in block.Paragraphs)
                    {
                        foreach (var word in paragraph.Words)
                        {
                            string value = string.Join(string.Empty, word.Symbols.Select(sym => sym.Text));
                            if (value == type)
                            {
                                return new MatchedAnnotation() { MatchedWord = word, TargetValue = type, TargetValueOrder = targetMatches.IndexOf(type)};
                            }
                        }
                    }
                }
            }

            return null;
        }



        private static IList<Paragraph> FindTypeParagraphs(Page page, MatchedAnnotation word)
        {
            int wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            int wordLenght = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            int y = 0;
            int x = 0;
            x = word.MatchedWord.BoundingBox.Vertices[1].X;
            if (word.TargetValueOrder == 0) // "Art"
            {
                y = word.MatchedWord.BoundingBox.Vertices[3].Y + 3 * wordHeight;
                x = x + (wordLenght / 3) * 18;
            }
            if (word.TargetValueOrder == 1)// "Fahrzeugs"
            {
                y = word.MatchedWord.BoundingBox.Vertices[3].Y + 3 * wordHeight;
                x = x + wordLenght * (1 / 3);
            }
            if (word.TargetValueOrder == 2) // "Genre"
            {
                y = word.MatchedWord.BoundingBox.Vertices[3].Y;
            }
            if (word.TargetValueOrder == 3) // "véhicule"
            {
                y = word.MatchedWord.BoundingBox.Vertices[3].Y;
            }
            if (word.TargetValueOrder == 4) // "Genere"
            {
                y = word.MatchedWord.BoundingBox.Vertices[0].Y;
            }
            if (word.TargetValueOrder == 5) // "veicolo"
            {
                y = word.MatchedWord.BoundingBox.Vertices[0].Y;
            }
            if (word.TargetValueOrder == 6) // "Gener"
            {
                y = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
            }
            if (word.TargetValueOrder == 7) // "vehichel"
            {
                y = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
            }

            IList<Paragraph> typeMatchedParagraphs = new List<Paragraph>();

            foreach (var block in page.Blocks)
            {
                foreach (var paragraph in block.Paragraphs)
                {
                    int blokY1 = paragraph.BoundingBox.Vertices[0].Y;
                    int blokY2 = paragraph.BoundingBox.Vertices[3].Y;
                    int blokX = paragraph.BoundingBox.Vertices[0].X;
                    if (y > blokY1 && y < blokY2 && blokX > x)
                    {
                        typeMatchedParagraphs.Add(paragraph);
                    }
                }
            }

            return typeMatchedParagraphs;
        }
        private static IList<Word> FindTypeWords(Page page, MatchedAnnotation word)
        {
            double wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            double wordLenght = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            double Y1 = 0;
            double Y2 = 0;
            double X = word.MatchedWord.BoundingBox.Vertices[1].X;
            if (word.TargetValueOrder == 0) // "Art"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                X = X + Math.Round(wordLenght * 10);
            }
            if (word.TargetValueOrder == 1)// "Fahrzeugs"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                X = X + Math.Round(wordLenght * 2.2);
            }
            if (word.TargetValueOrder == 2) // "Genre"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2);
                X = X + Math.Round(wordLenght * 2.5);
            }
            if (word.TargetValueOrder == 3) // "véhicule"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2);
                X = X + Math.Round(wordLenght * 0.6);
            }
            if (word.TargetValueOrder == 4) // "Genere"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight);
                X = X + Math.Round(wordLenght * 2.3);
            }
            if (word.TargetValueOrder == 5) // "veicolo"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight);
                X = X + Math.Round(wordLenght * 0.8);
            }
            if (word.TargetValueOrder == 6) // "Gener"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X = X + Math.Round(wordLenght * 2.5);
            }
            if (word.TargetValueOrder == 7) // "vehichel"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X = X + Math.Round(wordLenght * 0.4);
            }

            IList<Word> typeMatchedWords = new List<Word>();

            foreach (var block in page.Blocks)
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

        private static IList<Paragraph> FindMarkAndModelParagraphs(Page page, MatchedAnnotation word)
        {
            int wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            int wordWidth = word.MatchedWord.BoundingBox.Vertices[3].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            int y = 0;
            int x = word.MatchedWord.BoundingBox.Vertices[1].X + 3 * wordWidth;
            if (word.TargetValueOrder == 0 || word.TargetValueOrder == 1 || word.TargetValueOrder == 2) // "Marke" or "und" or "Typ"
            {
                y = word.MatchedWord.BoundingBox.Vertices[3].Y + wordHeight;
            }
            if (word.TargetValueOrder == 3 || word.TargetValueOrder == 4) //"Marque" or "type"
            {
                y = word.MatchedWord.BoundingBox.Vertices[3].Y;
            }
            if (word.TargetValueOrder == 5 || word.TargetValueOrder == 6) //"Marca" or "tipo"
            {
                y = word.MatchedWord.BoundingBox.Vertices[0].Y;
            }
            if (word.TargetValueOrder == 7 || word.TargetValueOrder == 8) //"Marca" or "tig"
            {
                y = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
            }

            IList<Paragraph> markAndModelMatchedParagraphs = new List<Paragraph>();

            foreach (var block in page.Blocks)
            {
                foreach (var paragraph in block.Paragraphs)
                {
                    int blokY1 = paragraph.BoundingBox.Vertices[0].Y;
                    int blokY2 = paragraph.BoundingBox.Vertices[3].Y;
                    int blokX = paragraph.BoundingBox.Vertices[0].X;
                    if (y > blokY1 && y < blokY2 && blokX > x)
                    {
                        markAndModelMatchedParagraphs.Add(paragraph);
                    }
                }
            }

            return markAndModelMatchedParagraphs;
        }
        private static IList<Word> FindMarkAndModelWords(Page page, MatchedAnnotation word)
        {
            double wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            double wordLenght = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            double Y1 = 0;
            double Y2 = 0;
            double X = word.MatchedWord.BoundingBox.Vertices[1].X;
            if (word.TargetValueOrder == 0) // "Marke"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2.7);
                X = X + Math.Round(wordLenght * 3.1);
            }
            if (word.TargetValueOrder == 1) // "Typ"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2.7);
                X = X + Math.Round(wordLenght * 2.3);
            }
            if (word.TargetValueOrder == 2) //"Marque"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.7);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1.3);
                X = X + Math.Round(wordLenght * 2.1);
            }
            if (word.TargetValueOrder == 3) //"type"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.7);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1.3);
                X = X + Math.Round(wordLenght * 1.6);
            }
            if (word.TargetValueOrder == 4) //"Marca"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.5);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.85);
                X = X + Math.Round(wordLenght * 3.2);
            }
            if (word.TargetValueOrder == 5) //"tipo"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.5);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.85);
                X = X + Math.Round(wordLenght * 3.1);
            }
            if (word.TargetValueOrder == 6) //"Marca"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X = X + Math.Round(wordLenght * 5);
            }
            if (word.TargetValueOrder == 7) //"tig"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 3);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X = X + Math.Round(wordLenght * 9);
            }

            IList<Word> markAndModelMatchedWords = new List<Word>();

            foreach (var block in page.Blocks)
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
                            markAndModelMatchedWords.Add(w);
                        }
                    }
                }
            }

            return markAndModelMatchedWords;
        }

        private static IList<Word> FindChassisNumWords(Page page, MatchedAnnotation word)
        {
            double wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            double wordLenght = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            double Y1 = 0;
            double Y2 = 0;
            double X = word.MatchedWord.BoundingBox.Vertices[1].X;
            if (word.TargetValueOrder == 0) // "Fahrgestell"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                X = X + Math.Round(wordLenght * 1.1);
            }
            if (word.TargetValueOrder == 1) // "Nr."
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 3);
                X = X + Math.Round(wordLenght * 3.3);
            }
            if (word.TargetValueOrder == 2) //"Chassis"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2);
                X = X + Math.Round(wordLenght * 2.2);
            }
            if (word.TargetValueOrder == 3) //"Telaio"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight);
                X = X + Math.Round(wordLenght * 2.75);
            }
            if (word.TargetValueOrder == 4) //"Schassis"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2.5);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X = X + Math.Round(wordLenght * 1.48);
            }
            if (word.TargetValueOrder == 5) //"nr."
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2.5);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X = X + Math.Round(wordLenght * 4.8);
            }

            IList<Word> chassisNumMatchedWords = new List<Word>();

            foreach (var block in page.Blocks)
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
                            chassisNumMatchedWords.Add(w);
                        }
                    }
                }
            }

            return chassisNumMatchedWords;
        }

        private static IList<Paragraph> FindBodyCodeParagraphs(Page page, MatchedAnnotation word)
        {
            int wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            int y = 0;
            int x = 0;
            x = word.MatchedWord.BoundingBox.Vertices[1].X;
            if (word.TargetValueOrder == 0) //"Karosserie"
            {
                y = word.MatchedWord.BoundingBox.Vertices[3].Y + wordHeight;
            }
            if (word.TargetValueOrder == 1) //"Carrosserie"
            {
                y = word.MatchedWord.BoundingBox.Vertices[3].Y;
            }
            if (word.TargetValueOrder == 2) //"Carrozzeria"
            {
                y = word.MatchedWord.BoundingBox.Vertices[0].Y;
            }
            if (word.TargetValueOrder == 3) //"Carossaria"
            {
                y = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
            }

            IList<Paragraph> bodyCodeMatchedParagraphs = new List<Paragraph>();

            foreach (var block in page.Blocks)
            {
                foreach (var paragraph in block.Paragraphs)
                {
                    int blokY1 = paragraph.BoundingBox.Vertices[0].Y;
                    int blokY2 = paragraph.BoundingBox.Vertices[3].Y;
                    int blokX = paragraph.BoundingBox.Vertices[0].X;
                    if (y > blokY1 && y < blokY2 && blokX > x)
                    {
                        bodyCodeMatchedParagraphs.Add(paragraph);
                    }
                }
            }

            return bodyCodeMatchedParagraphs;
        }
        private static IList<Word> FindBodyCodeWords(Page page, MatchedAnnotation word)
        {
            double wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            double wordLenght = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            double Y1 = 0;
            double Y2 = 0;
            double X = word.MatchedWord.BoundingBox.Vertices[1].X;
            if (word.TargetValueOrder == 0) // "Karosserie"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2.8);
                X = X + Math.Round(wordLenght * 1.25);
            }
            if (word.TargetValueOrder == 1) // "Carrosserie"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.8);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1.6);
                X = X + Math.Round(wordLenght * 1.1);
            }
            if (word.TargetValueOrder == 2) // "Carrozzeria"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.8);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.8);
                X = X + Math.Round(wordLenght * 1.01);
            }
            if (word.TargetValueOrder == 3) // "Carossaria"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2.7);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X = X + Math.Round(wordLenght * 1.25);
            }

            IList<Word> bodyCodeMatchedWords = new List<Word>();

            foreach (var block in page.Blocks)
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

        private static IList<Paragraph> FindColorParagraphs(Page page, MatchedAnnotation word)
        {
            int wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            int wordWidth = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            int y = 0;
            int x = word.MatchedWord.BoundingBox.Vertices[1].X + wordWidth;
            if (word.TargetValueOrder == 0)
            {
                y = word.MatchedWord.BoundingBox.Vertices[3].Y + wordHeight;
            }
            if (word.TargetValueOrder == 1)
            {
                y = word.MatchedWord.BoundingBox.Vertices[3].Y;
            }
            if (word.TargetValueOrder == 2)
            {
                y = word.MatchedWord.BoundingBox.Vertices[0].Y;
            }
            if (word.TargetValueOrder == 3)
            {
                y = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
            }

            IList<Paragraph> colorMatchedParagraphs = new List<Paragraph>();

            foreach (var block in page.Blocks)
            {
                foreach (var paragraph in block.Paragraphs)
                {
                    int blokY1 = paragraph.BoundingBox.Vertices[0].Y;
                    int blokY2 = paragraph.BoundingBox.Vertices[3].Y;
                    int blokX = paragraph.BoundingBox.Vertices[0].X;
                    if (y > blokY1 && y < blokY2 && blokX > x)
                    {
                        colorMatchedParagraphs.Add(paragraph);
                    }
                }
            }

            return colorMatchedParagraphs;
        }
        private static IList<Word> FindColorWords(Page page, MatchedAnnotation word)
        {
            double wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            double wordLenght = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            double Y1 = 0;
            double Y2 = 0;
            double X = word.MatchedWord.BoundingBox.Vertices[1].X;
            if (word.TargetValueOrder == 0) // "Farbe"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2.65);
                X = X + Math.Round(wordLenght * 3.12);
            }
            if (word.TargetValueOrder == 1) // "Couleur"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1.6);
                X = X + Math.Round(wordLenght * 2.03);
            }
            if (word.TargetValueOrder == 2) // "Colore"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(1.7 * wordHeight);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight);
                X = X + Math.Round(wordLenght * 2.75);
            }
            if (word.TargetValueOrder == 3) // "Colur"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2.7);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X = X + Math.Round(wordLenght * 3.5);
            }

            IList<Word> colorMatchedWords = new List<Word>();

            foreach (var block in page.Blocks)
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
                            colorMatchedWords.Add(w);
                        }
                    }
                }
            }

            return colorMatchedWords;

        }

        private static IList<Word> FindMatriculeNumWords(Page page, MatchedAnnotation word)
        {
            double wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            double wordLenght = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            double Y1 = 0;
            double Y2 = 0;
            double X1 = word.MatchedWord.BoundingBox.Vertices[1].X;
            double X2 = X1;
            if (word.TargetValueOrder == 0) // "Stammnummer"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 2.4);
                X1 = X1 + Math.Round(wordLenght * 0.45);
                X2 = X2 + Math.Round(wordLenght * 3);
            }
            if (word.TargetValueOrder == 1) //"matricule"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 1.6);
                X1 = X1 + Math.Round(wordLenght * 1.35);
                X2 = X2 + Math.Round(wordLenght * 6.2);
            }
            if (word.TargetValueOrder == 2) //"matricola"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 1.5);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.7);
                X1 = X1 + Math.Round(wordLenght * 1.1);
                X2 = X2 + Math.Round(wordLenght * 6.05);
            }
            if (word.TargetValueOrder == 3) //"matricla"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 2.5);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X1 = X1 + Math.Round(wordLenght * 1.2);
                X2 = X2 + Math.Round(wordLenght * 7.1);
            }

            IList<Word> matriculeNumMatchedWords = new List<Word>();

            foreach (var block in page.Blocks)
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

        private static IList<Word> FindFirstRegistrationDateWords(Page page, MatchedAnnotation word)
        {
            double wordHeight = word.MatchedWord.BoundingBox.Vertices[3].Y - word.MatchedWord.BoundingBox.Vertices[0].Y;
            double wordLenght = word.MatchedWord.BoundingBox.Vertices[1].X - word.MatchedWord.BoundingBox.Vertices[0].X;
            double Y1 = 0;
            double Y2 = 0;
            double X1 = word.MatchedWord.BoundingBox.Vertices[1].X;
            double X2 = X1;

            if (word.TargetValueOrder == 0) // "Inverkehrsetzung"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + 2 * wordHeight;
                X1 = X1 + Math.Round(wordLenght * 0.3);
                X2 = X2 + Math.Round(wordLenght * 2.75);
            }
            if (word.TargetValueOrder == 1) //"mise"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.8);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + wordHeight;
                X1 = X1 + Math.Round(wordLenght * 4.3);
                X2 = X2 + Math.Round(wordLenght * 15.2);
            }
            if (word.TargetValueOrder == 2) //"circulation"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - Math.Round(wordHeight * 0.8);
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + wordHeight;
                X1 = X1 + Math.Round(wordLenght * 0.3);
                X2 = X2 + Math.Round(wordLenght * 4.8);
            }
            if (word.TargetValueOrder == 3) //"messa"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + Math.Round(wordHeight * 0.5);
                X1 = X1 + Math.Round(wordLenght * 3);
                X2 = X2 + Math.Round(wordLenght * 10.9);
            }
            if (word.TargetValueOrder == 4) //"circolazione"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y + +Math.Round(wordHeight * 0.5);
                X1 = X1 + Math.Round(wordLenght * 0.2);
                X2 = X2 + Math.Round(wordLenght * 4.1);
            }
            if (word.TargetValueOrder == 5) //"entrada"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - 2 * wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X1 = X1 + Math.Round(wordLenght * 2.2);
                X2 = X2 + Math.Round(wordLenght * 8.3);
            }
            if (word.TargetValueOrder == 6) //"circulaziun"
            {
                Y1 = word.MatchedWord.BoundingBox.Vertices[0].Y - 2 * wordHeight;
                Y2 = word.MatchedWord.BoundingBox.Vertices[3].Y;
                X1 = X1 + Math.Round(wordLenght * 0.2);
                X2 = X2 + Math.Round(wordLenght * 4.8);
            }
    
            IList<Word> firstRegistrationDateMatchedWords = new List<Word>();

            foreach (var block in page.Blocks)
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
        


        private static IList<string> GetTypeFields(IList<Paragraph> paragraphs)
        {
            IList<string> result = new List<string>();
            foreach (var paragraph in paragraphs)
            {
                string parText = string.Empty;
                foreach (var word in paragraph.Words)
                {
                    string value = string.Join(string.Empty, word.Symbols.Select(sym => sym.Text));
                    parText += value + " ";
                }
                result.Add(parText.Remove(parText.Length - 1));
            }
            return result;
        }

        private static string GetWordsText(IList<Word> words)
        {
            string result = string.Empty;
            foreach (var word in words)
            {
                string value = string.Join(string.Empty, word.Symbols.Select(sym => sym.Text));
                result += value + " ";
            }
            return result.Remove(result.Length - 1);
        }

        private static void SaveTestImg(System.Drawing.Bitmap img, string filename)
        {
            try
            {
                string folderPath = @"C:\Users\home\Desktop\CarAuto";


                string filePath = Path.Combine(folderPath, filename);

                img.Save(filePath);

            }
            catch (Exception exc)
            { }
        }
    }
}

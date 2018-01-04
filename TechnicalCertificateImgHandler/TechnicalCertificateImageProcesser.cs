using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechnicalCertificateImgHandler.Abstractions;
using TechnicalCertificateImgHandler.AppKeys;
using static TechnicalCertificateImgHandler.AppKeys.ApplicationKeys;

namespace TechnicalCertificateImgHandler
{
    public class TechnicalCertificateImageProcesser : IDocumentProcesser
    {
        // "Fahrzeugs", "Genre", "véhicule", "Genere", "veicolo", "Gener", "vehichel"
        private readonly List<string> targetTypeLabels = new List<string>()
        {
            WordLabelCodes.TYPE_CODE_L1,
            WordLabelCodes.TYPE_CODE_L2,
            WordLabelCodes.TYPE_CODE_L3,
            WordLabelCodes.TYPE_CODE_L4,
            WordLabelCodes.TYPE_CODE_L5,
            WordLabelCodes.TYPE_CODE_L6,
            WordLabelCodes.TYPE_CODE_L7,
            WordLabelCodes.TYPE_CODE_L8
        };

        //"Marke", "Typ", "Marque", "type", "Marca", "tipo" - using this one
        //or
        //"Marke", "und", "Typ", "Marque", "type", "Marca", "tipo", "Marca", "tip"
        private readonly List<string> targetMarkAndModelLabels = new List<string>()
        {
            WordLabelCodes.MARK_AND_MODEL_L1,
            WordLabelCodes.MARK_AND_MODEL_L2,
            WordLabelCodes.MARK_AND_MODEL_L3,
            WordLabelCodes.MARK_AND_MODEL_L4,
            WordLabelCodes.MARK_AND_MODEL_L5,
            WordLabelCodes.MARK_AND_MODEL_L6
        };

        //"Fahrgestell", "Nr.", "Chassis", "Telaio", "Schassis", "nr."
        private readonly List<string> targetChassisNumLabels = new List<string>()
        {
            WordLabelCodes.CHASSIS_NUM_L1,
            WordLabelCodes.CHASSIS_NUM_L2,
            WordLabelCodes.CHASSIS_NUM_L3,
            WordLabelCodes.CHASSIS_NUM_L4,
            WordLabelCodes.CHASSIS_NUM_L5,
            WordLabelCodes.CHASSIS_NUM_L6
        };

        //"Karosserie", "Carrosserie", "Carrozzeria", "Carossaria"
        private readonly List<string> targetBodyCodeLabels = new List<string>()
        {
            WordLabelCodes.BODY_CODE_L1,
            WordLabelCodes.BODY_CODE_L2,
            WordLabelCodes.BODY_CODE_L3,
            WordLabelCodes.BODY_CODE_L4
        };

        //"Farbe", "Couleur", "Colore", "Colur"
        private readonly List<string> targetColorLabels = new List<string>()
        {
            WordLabelCodes.COLOR_L1,
            WordLabelCodes.COLOR_L2,
            WordLabelCodes.COLOR_L3,
            WordLabelCodes.COLOR_L4
        };

        //"Stammnummer", "matricule", "matricola", "matricla"
        private readonly List<string> targetMatriculeNumLabels = new List<string>()
        {
            WordLabelCodes.MARTICULE_NUM_L1,
            WordLabelCodes.MARTICULE_NUM_L2,
            WordLabelCodes.MARTICULE_NUM_L3,
            WordLabelCodes.MARTICULE_NUM_L4
        };

        //"Inverkehrsetzung", "mise", "circulation", "messa", "circolazione", "entrada", "circulaziun"
        private readonly List<string> targetFirstRegistrationDateLabels = new List<string>()
        {
            WordLabelCodes.FIRST_REGISTRATION_DATE_L1,
            WordLabelCodes.FIRST_REGISTRATION_DATE_L2,
            WordLabelCodes.FIRST_REGISTRATION_DATE_L3,
            WordLabelCodes.FIRST_REGISTRATION_DATE_L4,
            WordLabelCodes.FIRST_REGISTRATION_DATE_L5,
            WordLabelCodes.FIRST_REGISTRATION_DATE_L6,
            WordLabelCodes.FIRST_REGISTRATION_DATE_L7
        };

        //"Typengenehmigung", "Reception", "Approvazione", "Approvaziun"
        private readonly List<string> targetReceptionNumLabels = new List<string>()
        {
            WordLabelCodes.RECEPTION_NUM_L1,
            WordLabelCodes.RECEPTION_NUM_L2,
            WordLabelCodes.RECEPTION_NUM_L3,
            WordLabelCodes.RECEPTION_NUM_L4
        };

        //"Schild", "Plaque", "Targa", "Numer"
        private readonly List<string> targetSoNumLabels = new List<string>()
        {
            WordLabelCodes.SONum_l1,
            WordLabelCodes.SONum_l2,
            WordLabelCodes.SONum_l3,
            WordLabelCodes.SONum_l4
        };

        private readonly IWordMatcher wordMatcher;
        private readonly IWordFinder typeFinder;
        private readonly IWordFinder markAndModelFinder;
        private readonly IWordFinder chassisNumFinder;
        private readonly IWordFinder bodyCodeFinder;
        private readonly IWordFinder colorFinder;
        private readonly IWordFinder matriculNumFinder;
        private readonly IWordFinder firstRegistrationDateFinder;
        private readonly IWordFinder receptionNumFinder;
        private readonly IWordFinder soNumFinder;

        ITechnicalCertificateService technicalCertificateService

        private readonly ITechnicalCertificateValidatior validator;

        public TechnicalCertificateImageProcesser(ITechnicalCertificateService technicalCertificateService,
                            IWordMatcher wordMatcher, 
                            IWordFinder typeFinder,
                            IWordFinder markAndModelFinder,
                            IWordFinder chassisNumFinder,
                            IWordFinder bodyCodeFinder,
                            IWordFinder colorFinder,
                            IWordFinder matriculNumFinder,
                            IWordFinder firstRegistrationDateFinder,
                            IWordFinder receptionNumFinder,
                            IWordFinder soNumFinder,
                            ITechnicalCertificateValidatior validator)
        {
            this.technicalCertificateService = technicalCertificateService;
            this.wordMatcher = wordMatcher;
            this.typeFinder = typeFinder;
            this.markAndModelFinder = markAndModelFinder;
            this.chassisNumFinder = chassisNumFinder;
            this.bodyCodeFinder = bodyCodeFinder;
            this.colorFinder = colorFinder;
            this.matriculNumFinder = matriculNumFinder;
            this.firstRegistrationDateFinder = firstRegistrationDateFinder;
            this.receptionNumFinder = receptionNumFinder;
            this.soNumFinder = soNumFinder;
            this.validator = validator;
        }

        public VehicleCertificateContentDTO Process()
        {
            string type = FindType();
            string typeCode = GetCodeValue(type);
            string typeValue = GetTypeValue(type);

            string body = FindBody();
            string bodyCode = GetCodeValue(body);
            string bodyValue = GetTypeValue(body);

            string markAndModel = FindMarkAndModel();
            string chassisNum = FindChassisNum();
            string color = FindColor();
            string matriculNum = FindMatriculNum();
            string firstRegistrationDate = FindFirstRegistrationDate();
            string receptionNum = FindReceptionNum();
            string soNum = FindSoNum();

            return
                new VehicleCertificateContentDTO()
                {
                    SONo = soNum,
                    ReceptionNum = receptionNum,
                    Type = typeValue,
                    TypeCode = typeCode,
                    Body = bodyValue,
                    BodyCode = bodyCode,
                    MarkAndModel = markAndModel,
                    ChassisNum = chassisNum.ToUpper(),
                    Color = color,
                    MatriculNum = matriculNum.Replace(" ", string.Empty),
                    FirstRegistrationDate = firstRegistrationDate.Replace(" ", string.Empty)
                };
        }


        public string GetTypeRawValue(IList<string> targetTypeLabels)
        {
            string rawValue = string.Empty;
            foreach (var label in targetTypeLabels)
            {
                rawValue = technicalCertificateService.GetTypeRawValueByLabel(label, );
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }
        public string GetMarkAndModelRawValue(IList<string> targetMarkAndModelLabels)
        {
            string rawValue = string.Empty;
            foreach (var label in targetMarkAndModelLabels)
            {
                rawValue = technicalCertificateService.GetMarkAndModelValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }
        public string GetChassisNumRawValue(IList<string> targetChassisNumLabels)
        {
            string rawValue = string.Empty;
            foreach (var label in targetChassisNumLabels)
            {
                rawValue = technicalCertificateService.GetChassisNumRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }
        public string GetBodyRawValue(IList<string> targetBodyCodeLabels)
        {
            string rawValue = string.Empty;
            foreach (var label in targetBodyCodeLabels)
            {
                rawValue = technicalCertificateService.GetBodyRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }
        public string GetColorRawValue(IList<string> targetColorLabels)
        {
            string rawValue = string.Empty;
            foreach (var label in targetColorLabels)
            {
                rawValue = technicalCertificateService.GetColorRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }
        public string GetMatriculNumRawValue(IList<string> targetMatriculeNumLabels)
        {
            string rawValue = string.Empty;
            foreach (var label in targetMatriculeNumLabels)
            {
                rawValue = technicalCertificateService.GetMatriculNumRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }
        public string GetFirstRegistrationDateRawValue(IList<string> targetFirstRegistrationDateLabels)
        {
            string rawValue = string.Empty;
            foreach (var label in targetFirstRegistrationDateLabels)
            {
                rawValue = technicalCertificateService.GetFirstRegistrationDateRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }
        public string GetReceptionNumRawValue(IList<string> targetReceptionNumLabels)
        {
            string rawValue = string.Empty;
            foreach (var label in targetReceptionNumLabels)
            {
                rawValue = technicalCertificateService.GetReceptionNumRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }
        public string GetSoNumRawValue(IList<string> targetSoNumLabels)
        {
            string rawValue = string.Empty;
            foreach (var label in targetSoNumLabels)
            {
                rawValue = technicalCertificateService.GetSoNumRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }





        private string FindType()
        {
            string typeSplitWords = string.Empty;

            var typeMatchedWords = wordMatcher.GetMatchedWords(targetTypeLabels);
            if (typeMatchedWords.Count == 0)
            {
                //TO DO add log info
                return typeSplitWords;
            }

            foreach (var matchedWord in typeMatchedWords)
            {
                var typeWords = typeFinder.FindWords(matchedWord);
                if (typeWords.Count == 0)
                {
                    //TO DO add log info
                }
                else
                {
                    typeSplitWords = GetWordsText(typeWords);
                }

                if (!string.IsNullOrEmpty(typeSplitWords))
                {
                    break;
                }
            }

            return typeSplitWords;
        }

        private string FindMarkAndModel()
        {
            string markAndModelSplitWords = string.Empty;

            var markAndModelMatchedWords = wordMatcher.GetMatchedWords(targetMarkAndModelLabels);
            if (markAndModelMatchedWords.Count == 0)
            {
                //TO DO add log info
                return markAndModelSplitWords;
            }

            foreach (var matchedWord in markAndModelMatchedWords)
            {
                var markAndModelWords = markAndModelFinder.FindWords(matchedWord);
                if (markAndModelWords.Count == 0)
                {
                    //TO DO add log info
                }
                else
                {
                    markAndModelSplitWords = GetWordsText(markAndModelWords);
                }

                if (!string.IsNullOrEmpty(markAndModelSplitWords))
                {
                    break;
                }
            }

            return markAndModelSplitWords;
        }

        private string FindChassisNum()
        {
            string chassisNumSplitWords = string.Empty;

            var chassisNumMatchedWords = wordMatcher.GetMatchedWords(targetChassisNumLabels);
            if (chassisNumMatchedWords.Count == 0)
            {
                //TO DO add log info
                return chassisNumSplitWords;
            }

            foreach (var matchedWord in chassisNumMatchedWords)
            {
                var chassisNumWords = chassisNumFinder.FindWords(matchedWord);
                if (chassisNumWords.Count == 0)
                {
                    //TO DO add log info
                }
                else
                {
                    chassisNumSplitWords = GetWordsText(chassisNumWords);
                }

                if (!string.IsNullOrEmpty(chassisNumSplitWords))
                {
                    break;
                }
            }

            return chassisNumSplitWords;
        }

        private string FindBody()
        {
            string bodySplitWords = string.Empty;

            var bodyCodeMatchedWords = wordMatcher.GetMatchedWords(targetBodyCodeLabels);
            if (bodyCodeMatchedWords.Count == 0)
            {
                //TO DO add log info
                return bodySplitWords;
            }

            foreach (var matchedWord in bodyCodeMatchedWords)
            {
                var bodyCodeWords = bodyCodeFinder.FindWords(matchedWord);
                if (bodyCodeWords.Count == 0)
                {
                    //TO DO add log info
                }
                else
                {
                    bodySplitWords = GetWordsText(bodyCodeWords);
                }

                if (!string.IsNullOrEmpty(bodySplitWords))
                {
                    break;
                }
            }

            return bodySplitWords;
        }

        private string FindColor()
        {
            string colorSplitWords = string.Empty;

            var colorMatchedWords = wordMatcher.GetMatchedWords(targetColorLabels);
            if (colorMatchedWords.Count == 0)
            {
                //TO DO add log info
                return colorSplitWords;
            }

            foreach (var matchedWord in colorMatchedWords)
            {
                var colorWords = colorFinder.FindWords(matchedWord);
                if (colorWords.Count == 0)
                {
                    //TO DO add log info
                }
                else
                {
                    colorSplitWords = GetWordsText(colorWords);
                }

                if (!string.IsNullOrEmpty(colorSplitWords))
                {
                    break;
                }
            }

            return colorSplitWords;
        }

        private string FindMatriculNum()
        {
            string matriculNumSplitWords = string.Empty;

            var matriculNumMatchedWords = wordMatcher.GetMatchedWords(targetMatriculeNumLabels);
            if (matriculNumMatchedWords.Count == 0)
            {
                //TO DO add log info
                return matriculNumSplitWords;
            }

            foreach (var matchedWord in matriculNumMatchedWords)
            {
                var matriculNumWords = matriculNumFinder.FindWords(matchedWord);
                if (matriculNumWords.Count == 0)
                {
                    //TO DO add log info
                }
                else
                {
                    matriculNumSplitWords = GetWordsText(matriculNumWords);
                }

                if (!string.IsNullOrEmpty(matriculNumSplitWords))
                {
                    break;
                }
            }

            return matriculNumSplitWords;
        }

        private string FindFirstRegistrationDate()
        {
            string firstRegistrationDateSplitWords = string.Empty;

            var firstRegistrationDateMatchedWords = wordMatcher.GetMatchedWords(targetFirstRegistrationDateLabels);
            if (firstRegistrationDateMatchedWords.Count == 0)
            {
                //TO DO add log info
                return firstRegistrationDateSplitWords;
            }

            foreach (var matchedWord in firstRegistrationDateMatchedWords)
            {
                var firstRegistrationDateWords = firstRegistrationDateFinder.FindWords(matchedWord);
                if (firstRegistrationDateWords.Count == 0)
                {
                    //TO DO add log info
                }
                else
                {
                    firstRegistrationDateSplitWords = GetWordsText(firstRegistrationDateWords);
                }


                if (!string.IsNullOrEmpty(firstRegistrationDateSplitWords))
                {
                    break;
                }
            }           

            return firstRegistrationDateSplitWords;
        }

        private string FindReceptionNum()
        {
            string receptionNumSplitWords = string.Empty;

            var receptionNumMatchedWords = wordMatcher.GetMatchedWords(targetReceptionNumLabels);
            if (receptionNumMatchedWords.Count == 0)
            {
                //TO DO add log info
                return receptionNumSplitWords;
            }

            foreach (var matchedWord in receptionNumMatchedWords)
            {
                var receptionNumWords = receptionNumFinder.FindWords(matchedWord);
                if (receptionNumWords.Count == 0)
                {
                    //TO DO add log info
                }
                else
                {
                    receptionNumSplitWords = GetWordsText(receptionNumWords);
                }


                if (!string.IsNullOrEmpty(receptionNumSplitWords))
                {
                    break;
                }
            }

            return receptionNumSplitWords;
        }

        private string FindSoNum()
        {
            string soNumSplitWords = string.Empty;

            var soNumMatchedWords = wordMatcher.GetMatchedWords(targetSoNumLabels);
            if (soNumMatchedWords.Count == 0)
            {
                //TO DO add log info
                return soNumSplitWords;
            }

            foreach (var matchedWord in soNumMatchedWords)
            {
                var soNumWords = soNumFinder.FindWords(matchedWord);
                if (soNumWords.Count == 0)
                {
                    //TO DO add log info
                }
                else
                {
                    soNumSplitWords = GetWordsText(soNumWords);
                }


                if (!string.IsNullOrEmpty(soNumSplitWords))
                {
                    break;
                }
            }

            return soNumSplitWords;
        }

        private string GetWordsText(IList<Word> words)
        {
            string result = string.Empty;
            foreach (var word in words)
            {
                string value = string.Join(string.Empty, word.Symbols.Select(sym => sym.Text));
                result += value + " ";
            }
            return result != string.Empty ? result.Remove(result.Length - 1) : result;
        }

        private string GetCodeValue(string splitWords)
        {
            var code = Regex.Match(splitWords, "\\d+").Value;
            return code;
        }

        private string GetTypeValue(string splitWords)
        {
            var codeValue = GetCodeValue(splitWords);
            var type = splitWords.Replace("Code", string.Empty);
            if (!string.IsNullOrEmpty(codeValue))
            {
                type = type.Replace(codeValue, string.Empty);
            }
            return type.Trim();
        }
    }
}

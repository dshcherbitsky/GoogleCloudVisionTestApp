using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechnicalCertificateImageHandler.Infrastructure;
using TechnicalCertificateImageHandler.Infrastructure.Abstractions;
using TechnicalCertificateImageHandler.Infrastructure.Models;
using static TechnicalCertificateImageHandler.AppKeys.ApplicationKeys;

namespace TechnicalCertificateImageHandler
{
    public class TechnicalCertificateImageProcesser : IDocumentProcesser
    {
        private readonly TextAnnotation annotationContext;

        private readonly ITechnicalCertificateService technicalCertificateService;
        private readonly ITechnicalCertificateValidatior validator;

        private readonly IList<WordLabel> typeLabels;
        private readonly IList<WordLabel> markAndModelLabels;
        private readonly IList<WordLabel> chassisNumLabels;
        private readonly IList<WordLabel> bodyLabels;
        private readonly IList<WordLabel> colorLabels;
        private readonly IList<WordLabel> matriculNumLabels;
        private readonly IList<WordLabel> firstRegistrationDateLabels;
        private readonly IList<WordLabel> receptionNumLabels;
        private readonly IList<WordLabel> soNumLabels;

        public TechnicalCertificateImageProcesser(TextAnnotation textAnnotation)
        {
            this.annotationContext = textAnnotation;
            this.technicalCertificateService = new TechnicalCertificateService(textAnnotation);
            this.validator = new TechnicalCertificateValidatior();

            // "Art", "Fahrzeugs", "Genre", "véhicule", "Genere", "veicolo", "Gener", "vehichel"
            this.typeLabels = new List<WordLabel>()
            {
                new WordLabel(){ Text = WordLabelCodes.TYPE_CODE_L1, Type = LabelTypes.Label_1_1 },
                new WordLabel(){ Text = WordLabelCodes.TYPE_CODE_L2, Type = LabelTypes.Label_1_2 },
                new WordLabel(){ Text = WordLabelCodes.TYPE_CODE_L3, Type = LabelTypes.Label_2_1 },
                new WordLabel(){ Text = WordLabelCodes.TYPE_CODE_L4, Type = LabelTypes.Label_2_2 },
                new WordLabel(){ Text = WordLabelCodes.TYPE_CODE_L5, Type = LabelTypes.Label_3_1 },
                new WordLabel(){ Text = WordLabelCodes.TYPE_CODE_L6, Type = LabelTypes.Label_3_2 },
                new WordLabel(){ Text = WordLabelCodes.TYPE_CODE_L7, Type = LabelTypes.Label_4_1 },
                new WordLabel(){ Text = WordLabelCodes.TYPE_CODE_L8, Type = LabelTypes.Label_4_2 }
            };

            //"Marke", "Typ", "Marque", "type", "Marca", "tipo", "Marca", "tip" - Now using this one!!!
            //or
            //"Marke", "und", "Typ", "Marque", "type", "Marca", "tipo", "Marca", "tip"
            this.markAndModelLabels = new List<WordLabel>()
            {
                new WordLabel(){ Text = WordLabelCodes.MARK_AND_MODEL_L1, Type = LabelTypes.Label_1_1 },
                new WordLabel(){ Text = WordLabelCodes.MARK_AND_MODEL_L2, Type = LabelTypes.Label_1_2 },
                new WordLabel(){ Text = WordLabelCodes.MARK_AND_MODEL_L3, Type = LabelTypes.Label_2_1 },
                new WordLabel(){ Text = WordLabelCodes.MARK_AND_MODEL_L4, Type = LabelTypes.Label_2_2 },
                new WordLabel(){ Text = WordLabelCodes.MARK_AND_MODEL_L5, Type = LabelTypes.Label_3_1 },
                new WordLabel(){ Text = WordLabelCodes.MARK_AND_MODEL_L6, Type = LabelTypes.Label_3_2 },
                new WordLabel(){ Text = WordLabelCodes.MARK_AND_MODEL_L7, Type = LabelTypes.Label_4_1 },
                new WordLabel(){ Text = WordLabelCodes.MARK_AND_MODEL_L8, Type = LabelTypes.Label_4_2 }
            };

            //"Fahrgestell", "Nr.", "Chassis", "Telaio", "Schassis", "nr."
            this.chassisNumLabels = new List<WordLabel>()
            {
                new WordLabel(){ Text = WordLabelCodes.CHASSIS_NUM_L1, Type = LabelTypes.Label_1_1 },
                new WordLabel(){ Text = WordLabelCodes.CHASSIS_NUM_L2, Type = LabelTypes.Label_1_2 },
                new WordLabel(){ Text = WordLabelCodes.CHASSIS_NUM_L3, Type = LabelTypes.Label_2_1 },
                new WordLabel(){ Text = WordLabelCodes.CHASSIS_NUM_L4, Type = LabelTypes.Label_3_1 },
                new WordLabel(){ Text = WordLabelCodes.CHASSIS_NUM_L5, Type = LabelTypes.Label_4_1 },
                new WordLabel(){ Text = WordLabelCodes.CHASSIS_NUM_L6, Type = LabelTypes.Label_4_2 }
            };

            //"Karosserie", "Carrosserie", "Carrozzeria", "Carossaria"
            this.bodyLabels = new List<WordLabel>()
            {
                new WordLabel(){ Text = WordLabelCodes.BODY_CODE_L1, Type = LabelTypes.Label_1_1 },
                new WordLabel(){ Text = WordLabelCodes.BODY_CODE_L2, Type = LabelTypes.Label_2_1 },
                new WordLabel(){ Text = WordLabelCodes.BODY_CODE_L3, Type = LabelTypes.Label_3_1 },
                new WordLabel(){ Text = WordLabelCodes.BODY_CODE_L4, Type = LabelTypes.Label_4_1 }
            };

            //"Farbe", "Couleur", "Colore", "Colur"
            this.colorLabels = new List<WordLabel>()
            {
                new WordLabel(){ Text = WordLabelCodes.COLOR_L1, Type = LabelTypes.Label_1_1 },
                new WordLabel(){ Text = WordLabelCodes.COLOR_L2, Type = LabelTypes.Label_2_1 },
                new WordLabel(){ Text = WordLabelCodes.COLOR_L3, Type = LabelTypes.Label_3_1 },
                new WordLabel(){ Text = WordLabelCodes.COLOR_L4, Type = LabelTypes.Label_4_1 }
            };

            //"Stammnummer", "matricule", "matricola", "matricla"
            this.matriculNumLabels = new List<WordLabel>()
            {
                new WordLabel(){ Text = WordLabelCodes.MARTICULE_NUM_L1, Type = LabelTypes.Label_1_1 },
                new WordLabel(){ Text = WordLabelCodes.MARTICULE_NUM_L2, Type = LabelTypes.Label_2_1 },
                new WordLabel(){ Text = WordLabelCodes.MARTICULE_NUM_L3, Type = LabelTypes.Label_3_1 },
                new WordLabel(){ Text = WordLabelCodes.MARTICULE_NUM_L4, Type = LabelTypes.Label_4_1 }
            };

            //"Inverkehrsetzung", "mise", "circulation", "messa", "circolazione", "entrada", "circulaziun"
            this.firstRegistrationDateLabels = new List<WordLabel>()
            {
                new WordLabel(){ Text = WordLabelCodes.FIRST_REGISTRATION_DATE_L1, Type = LabelTypes.Label_1_1 },
                new WordLabel(){ Text = WordLabelCodes.FIRST_REGISTRATION_DATE_L2, Type = LabelTypes.Label_2_1 },
                new WordLabel(){ Text = WordLabelCodes.FIRST_REGISTRATION_DATE_L3, Type = LabelTypes.Label_2_2 },
                new WordLabel(){ Text = WordLabelCodes.FIRST_REGISTRATION_DATE_L4, Type = LabelTypes.Label_3_1 },
                new WordLabel(){ Text = WordLabelCodes.FIRST_REGISTRATION_DATE_L5, Type = LabelTypes.Label_3_2 },
                new WordLabel(){ Text = WordLabelCodes.FIRST_REGISTRATION_DATE_L6, Type = LabelTypes.Label_4_1 },
                new WordLabel(){ Text = WordLabelCodes.FIRST_REGISTRATION_DATE_L7, Type = LabelTypes.Label_4_2 }
            };

            //"Typengenehmigung", "Reception", "Approvazione", "Approvaziun"
            this.receptionNumLabels = new List<WordLabel>()
            {
                new WordLabel(){ Text = WordLabelCodes.RECEPTION_NUM_L1, Type = LabelTypes.Label_1_1 },
                new WordLabel(){ Text = WordLabelCodes.RECEPTION_NUM_L2, Type = LabelTypes.Label_2_1 },
                new WordLabel(){ Text = WordLabelCodes.RECEPTION_NUM_L3, Type = LabelTypes.Label_3_1 },
                new WordLabel(){ Text = WordLabelCodes.RECEPTION_NUM_L4, Type = LabelTypes.Label_4_1 }
            };

            //"Schild", "Plaque", "Targa", "Numer"
            this.soNumLabels = new List<WordLabel>()
            {
                new WordLabel(){ Text = WordLabelCodes.SONum_L1, Type = LabelTypes.Label_1_1 },
                new WordLabel(){ Text = WordLabelCodes.SONum_L2, Type = LabelTypes.Label_2_1 },
                new WordLabel(){ Text = WordLabelCodes.SONum_L3, Type = LabelTypes.Label_3_1 },
                new WordLabel(){ Text = WordLabelCodes.SONum_L4, Type = LabelTypes.Label_4_1 }
            };
        }

        public TechnicalCertificateImageProcesser(TextAnnotation textAnnotation,
               ITechnicalCertificateService technicalCertificateService,
               ITechnicalCertificateValidatior validator,
               IList<WordLabel> typeLabels,
               IList<WordLabel> markAndModelLabels,
               IList<WordLabel> chassisNumLabels,
               IList<WordLabel> bodyLabels,
               IList<WordLabel> colorLabels,
               IList<WordLabel> matriculNumLabels,
               IList<WordLabel> firstRegistrationDateLabels,
               IList<WordLabel> receptionNumLabels,
               IList<WordLabel> soNumLabels)
        {
            this.annotationContext = textAnnotation;
            this.technicalCertificateService = technicalCertificateService;
            this.validator = validator;
            this.typeLabels = typeLabels;
            this.markAndModelLabels = markAndModelLabels;
            this.chassisNumLabels = chassisNumLabels;
            this.bodyLabels = bodyLabels;
            this.colorLabels = colorLabels;
            this.matriculNumLabels = matriculNumLabels;
            this.firstRegistrationDateLabels = firstRegistrationDateLabels;
            this.receptionNumLabels = receptionNumLabels;
            this.soNumLabels = soNumLabels;
        }

        public string MatriculNumPattern { get; set; } = @"\d{3}\s*[.]\s*\d{3}\s*[.]\s*\d{3}";

        public string ReceptionNumPattern { get; set; } = @"[A-Z0-9]{4}\s*[A-Z0-9]{2}";

        public VehicleCertificateContentDTO Process()
        {
            string typeRawValue = GetTypeRawValue(typeLabels);
            string typeCode = GetCodeValue(typeRawValue);
            string typeValue = GetTypeValue(typeRawValue);

            string bodyRawValue = GetBodyRawValue(bodyLabels);
            string bodyCode = GetCodeValue(bodyRawValue);
            string bodyValue = GetTypeValue(bodyRawValue);

            string markAndModel = GetMarkAndModelRawValue(markAndModelLabels);
            string chassisNum = GetChassisNumRawValue(chassisNumLabels);
            string color = GetColorRawValue(colorLabels);
            string matriculNum = GetMatriculNumRawValue(matriculNumLabels);
            string firstRegistrationDate = GetFirstRegistrationDateRawValue(firstRegistrationDateLabels);
            string receptionNum = GetReceptionNumRawValue(receptionNumLabels);
            string soNum = GetSoNumRawValue(soNumLabels);

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

        public string GetTypeRawValue(IList<WordLabel> labels)
        {
            string rawValue = string.Empty;
            foreach (var label in labels)
            {
                rawValue = technicalCertificateService.GetTypeRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }

        public string GetMarkAndModelRawValue(IList<WordLabel> labels)
        {
            string rawValue = string.Empty;
            foreach (var label in labels)
            {
                rawValue = technicalCertificateService.GetMarkAndModelValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }

        public string GetChassisNumRawValue(IList<WordLabel> labels)
        {
            string rawValue = string.Empty;
            foreach (var label in labels)
            {
                rawValue = technicalCertificateService.GetChassisNumRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }

        public string GetBodyRawValue(IList<WordLabel> labels)
        {
            string rawValue = string.Empty;
            foreach (var label in labels)
            {
                rawValue = technicalCertificateService.GetBodyRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }

        public string GetColorRawValue(IList<WordLabel> labels)
        {
            string rawValue = string.Empty;
            foreach (var label in labels)
            {
                rawValue = technicalCertificateService.GetColorRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }

        public string GetMatriculNumRawValue(IList<WordLabel> labels)
        {
            string rawValue = string.Empty;

            rawValue = Regex.Match(annotationContext.Text, MatriculNumPattern).Value;
            if (!string.IsNullOrEmpty(rawValue))
            {
                return rawValue;
            }

            foreach (var label in labels)
            {
                rawValue = technicalCertificateService.GetMatriculNumRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }

        public string GetFirstRegistrationDateRawValue(IList<WordLabel> labels)
        {
            string rawValue = string.Empty;
            foreach (var label in labels)
            {
                rawValue = technicalCertificateService.GetFirstRegistrationDateRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }

        public string GetReceptionNumRawValue(IList<WordLabel> labels)
        {
            string rawValue = string.Empty;

            //rawValue = Regex.Match(annotationContext.Text, ReceptionNumPattern).Value;
            //if (!string.IsNullOrEmpty(rawValue))
            //{
            //    return rawValue;
            //}

            foreach (var label in labels)
            {
                rawValue = technicalCertificateService.GetReceptionNumRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }

        public string GetSoNumRawValue(IList<WordLabel> labels)
        {
            string rawValue = string.Empty;
            foreach (var label in labels)
            {
                rawValue = technicalCertificateService.GetSoNumRawValueByLabel(label);
                if (!string.IsNullOrEmpty(rawValue))
                {
                    return rawValue;
                }
            }

            return rawValue;
        }
    
        private string GetCodeValue(string value)
        {
            var code = Regex.Match(value, "\\d+").Value;
            return code;
        }

        private string GetTypeValue(string value)
        {
            var codeValue = GetCodeValue(value);
            var type = value.Replace("Code", string.Empty);
            if (!string.IsNullOrEmpty(codeValue))
            {
                type = type.Replace(codeValue, string.Empty);
            }
            return type.Trim();
        }       
    }
}

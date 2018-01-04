using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalCertificateImgHandler.Abstractions;
using static TechnicalCertificateImgHandler.AppKeys.ApplicationKeys;

namespace TechnicalCertificateImgHandler
{
    public class TechnicalCertificateService : ITechnicalCertificateService
    {
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

        public TechnicalCertificateService(TextAnnotation textAnnotation) : this(new WordMatcher(textAnnotation),
                new TypeFinder(textAnnotation),
                new MarkAndModelFinder(textAnnotation),
                new ChassisNumFinder(textAnnotation),
                new BodyCodeFinder(textAnnotation),
                new ColorFinder(textAnnotation),
                new MatriculNumFinder(textAnnotation),
                new FirstRegistrationDateFinder(textAnnotation),
                new ReceptionNumFinder(textAnnotation),
                new SONumFinder(textAnnotation))
        {

        }

        public TechnicalCertificateService(IWordMatcher wordMatcher,
                            IWordFinder typeFinder,
                            IWordFinder markAndModelFinder,
                            IWordFinder chassisNumFinder,
                            IWordFinder bodyCodeFinder,
                            IWordFinder colorFinder,
                            IWordFinder matriculNumFinder,
                            IWordFinder firstRegistrationDateFinder,
                            IWordFinder receptionNumFinder,
                            IWordFinder soNumFinder)
        {
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
        }

        public string GetBodyRawValueByLabel(string label, LabelTypes labelType)
        {
            string value = string.Empty;

            var matchedLabel = wordMatcher.GetMatchedLabel(label);
            if (matchedLabel == null)
            {
                //TO DO add log info
                return value;
            }

            var words = bodyCodeFinder.FindWords(matchedLabel, labelType);
            if (words.Count == 0)
            {
                //TO DO add log info
            }
            else
            {
                value = ConcatinateWordsText(words);
            }

            return value;
        }

        public string GetChassisNumRawValueByLabel(string label, LabelTypes labelType)
        {
            string value = string.Empty;

            var matchedLabel = wordMatcher.GetMatchedLabel(label);
            if (matchedLabel == null)
            {
                //TO DO add log info
                return value;
            }

            var words = chassisNumFinder.FindWords(matchedLabel, labelType);
            if (words.Count == 0)
            {
                //TO DO add log info
            }
            else
            {
                value = ConcatinateWordsText(words);
            }

            return value;
        }

        public string GetColorRawValueByLabel(string label, LabelTypes labelType)
        {
            string value = string.Empty;

            var matchedLabel = wordMatcher.GetMatchedLabel(label);
            if (matchedLabel == null)
            {
                //TO DO add log info
                return value;
            }

            var words = colorFinder.FindWords(matchedLabel, labelType);
            if (words.Count == 0)
            {
                //TO DO add log info
            }
            else
            {
                value = ConcatinateWordsText(words);
            }

            return value;
        }

        public string GetFirstRegistrationDateRawValueByLabel(string label, LabelTypes labelType)
        {
            string value = string.Empty;

            var matchedLabel = wordMatcher.GetMatchedLabel(label);
            if (matchedLabel == null)
            {
                //TO DO add log info
                return value;
            }

            var words = firstRegistrationDateFinder.FindWords(matchedLabel, labelType);
            if (words.Count == 0)
            {
                //TO DO add log info
            }
            else
            {
                value = ConcatinateWordsText(words);
            }

            return value;
        }

        public string GetMarkAndModelValueByLabel(string label, LabelTypes labelType)
        {
            string value = string.Empty;

            var matchedLabel = wordMatcher.GetMatchedLabel(label);
            if (matchedLabel == null)
            {
                //TO DO add log info
                return value;
            }

            var words = markAndModelFinder.FindWords(matchedLabel, labelType);
            if (words.Count == 0)
            {
                //TO DO add log info
            }
            else
            {
                value = ConcatinateWordsText(words);
            }

            return value;
        }

        public string GetMatriculNumRawValueByLabel(string label, LabelTypes labelType)
        {
            string value = string.Empty;

            var matchedLabel = wordMatcher.GetMatchedLabel(label);
            if (matchedLabel == null)
            {
                //TO DO add log info
                return value;
            }

            var words = matriculNumFinder.FindWords(matchedLabel, labelType);
            if (words.Count == 0)
            {
                //TO DO add log info
            }
            else
            {
                value = ConcatinateWordsText(words);
            }

            return value;
        }

        public string GetReceptionNumRawValueByLabel(string label, LabelTypes labelType)
        {
            string value = string.Empty;

            var matchedLabel = wordMatcher.GetMatchedLabel(label);
            if (matchedLabel == null)
            {
                //TO DO add log info
                return value;
            }

            var words = receptionNumFinder.FindWords(matchedLabel, labelType);
            if (words.Count == 0)
            {
                //TO DO add log info
            }
            else
            {
                value = ConcatinateWordsText(words);
            }

            return value;
        }

        public string GetSoNumRawValueByLabel(string label, LabelTypes labelType)
        {
            string value = string.Empty;

            var matchedLabel = wordMatcher.GetMatchedLabel(label);
            if (matchedLabel == null)
            {
                //TO DO add log info
                return value;
            }

            var words = soNumFinder.FindWords(matchedLabel, labelType);
            if (words.Count == 0)
            {
                //TO DO add log info
            }
            else
            {
                value = ConcatinateWordsText(words);
            }

            return value;
        }

        public string GetTypeRawValueByLabel(string label, LabelTypes labelType)
        {
            string value = string.Empty;

            var matchedLabel = wordMatcher.GetMatchedLabel(label);
            if (matchedLabel == null)
            {
                //TO DO add log info
                return value;
            }

            var words = typeFinder.FindWords(matchedLabel, labelType);
            if (words.Count == 0)
            {
                //TO DO add log info
            }
            else
            {
                value = ConcatinateWordsText(words);
            }

            return value;
        }

        private string ConcatinateWordsText(IList<Word> words)
        {
            string result = string.Empty;
            foreach (var word in words)
            {
                string value = string.Join(string.Empty, word.Symbols.Select(sym => sym.Text));
                result += $"{value} ";
            }
            return result != string.Empty ? result.Remove(result.Length - 1).Trim() : result.Trim();
        }
    }
}

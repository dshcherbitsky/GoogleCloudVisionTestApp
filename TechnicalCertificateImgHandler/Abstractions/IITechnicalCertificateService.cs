using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TechnicalCertificateImgHandler.AppKeys.ApplicationKeys;

namespace TechnicalCertificateImgHandler.Abstractions
{
    public interface ITechnicalCertificateService
    {
        string GetTypeRawValueByLabel(string label, LabelTypes labelType);

        string GetMarkAndModelValueByLabel(string label, LabelTypes labelType);

        string GetChassisNumRawValueByLabel(string label, LabelTypes labelType);

        string GetBodyRawValueByLabel(string label, LabelTypes labelType);

        string GetColorRawValueByLabel(string label, LabelTypes labelType);

        string GetMatriculNumRawValueByLabel(string label, LabelTypes labelType);

        string GetFirstRegistrationDateRawValueByLabel(string label, LabelTypes labelType);

        string GetReceptionNumRawValueByLabel(string label, LabelTypes labelType);

        string GetSoNumRawValueByLabel(string label, LabelTypes labelType);
    }
}

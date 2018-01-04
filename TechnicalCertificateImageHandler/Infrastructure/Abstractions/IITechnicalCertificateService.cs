using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalCertificateImageHandler.Infrastructure.Models;

namespace TechnicalCertificateImageHandler.Infrastructure.Abstractions
{
    public interface ITechnicalCertificateService
    {
        string GetTypeRawValueByLabel(WordLabel label);

        string GetMarkAndModelValueByLabel(WordLabel label);

        string GetChassisNumRawValueByLabel(WordLabel label);

        string GetBodyRawValueByLabel(WordLabel label);

        string GetColorRawValueByLabel(WordLabel label);

        string GetMatriculNumRawValueByLabel(WordLabel label);

        string GetFirstRegistrationDateRawValueByLabel(WordLabel label);

        string GetReceptionNumRawValueByLabel(WordLabel label);

        string GetSoNumRawValueByLabel(WordLabel label);
    }
}

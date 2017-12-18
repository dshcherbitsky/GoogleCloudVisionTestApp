using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalCertificateImgHandler.Abstractions
{
    public interface ITechnicalCertificateValidatior
    {
        /// <summary>
        ///  Validate Technical Certificate detected data.
        /// </summary>
        /// <param name="certificate">The certificate object value to validate</param>
        bool IsValid(VehicleCertificateContentDTO certificate);

        /// <summary>
        ///  Returns Technical Certificate detected data errors.
        /// </summary>
        List<string> GetValidationErrors();
    }
}

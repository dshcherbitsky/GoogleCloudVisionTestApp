using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalCertificateImageHandler.AppKeys;
using TechnicalCertificateImageHandler.Infrastructure.Abstractions;
using TechnicalCertificateImageHandler.Infrastructure.Models;

namespace TechnicalCertificateImageHandler.Infrastructure
{
    public class TechnicalCertificateValidatior : ITechnicalCertificateValidatior
    {
        private List<string> errors;

        /// <summary>
        /// Initializes a new instance of the TechnicalCertificateValidatior class.
        /// </summary>
        public TechnicalCertificateValidatior() : this(new List<string>())
        {

        }

        /// <summary>
        /// Initializes a new instance of the TechnicalCertificateValidatior class.
        /// </summary>
        /// <param name="errors">The list of validation errors.</param>
        /// 
        public TechnicalCertificateValidatior(List<string> errors)
        {
            this.errors = errors;
        }

        public bool IsValid(VehicleCertificateContentDTO certificate)
        {
            if (string.IsNullOrEmpty(certificate.Body))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.BODY_ERROR);
            }

            if (string.IsNullOrEmpty(certificate.BodyCode))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.BODY_CODE_ERROR);
            }

            if (string.IsNullOrEmpty(certificate.ChassisNum))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.CHASSIS_NUM_ERROR);
            }

            if (string.IsNullOrEmpty(certificate.Color))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.COLOR_ERROR);
            }

            if (string.IsNullOrEmpty(certificate.FirstRegistrationDate))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.FIRST_REGISTRATION_DATE_ERROR);
            }

            if (string.IsNullOrEmpty(certificate.MarkAndModel))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.MARK_AND_MODEL_ERROR);
            }

            if (string.IsNullOrEmpty(certificate.MatriculNum))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.MARTICUL_NUM_ERROR);
            }

            if (string.IsNullOrEmpty(certificate.ReceptionNum))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.RECEPTION_NUM_ERROR);
            }

            if (string.IsNullOrEmpty(certificate.SONo))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.SO_NUM_ERROR);
            }

            if (string.IsNullOrEmpty(certificate.Type))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.TYPE_ERROR);
            }

            if (string.IsNullOrEmpty(certificate.TypeCode))
            {
                errors.Add(ApplicationKeys.TechnicalCertificateValidation.TYPE_CODE_ERROR);
            }

            return errors.Count == 0;
        }

        /// <summary>
        ///  Returns Technical Certificate Validatior data errors.
        /// </summary>
        public List<string> GetValidationErrors()
        {
            return errors;
        }
    }
}

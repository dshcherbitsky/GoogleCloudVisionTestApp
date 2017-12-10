using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudVisionTestApp.Model
{
    public class VehicleCertificateContentDTO
    {
        /// <summary>
        ///  field 15
        /// </summary>
        public string SONo { get; set; }

        /// <summary>
        ///  field 18
        /// </summary>
        public string MatriculNum { get; set; }

        /// <summary>
        ///  field 19
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///  field 19
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        ///  field 21
        /// </summary>
        public string MarkAndModel { get; set; }

        /// <summary>
        ///  field 23
        /// </summary>
        public string ChassisNum { get; set; }

        /// <summary>
        ///  field 24
        /// </summary>
        public string ReceptionNum { get; set; }

        /// <summary>
        ///  field 25
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///  field 25
        /// </summary>
        public string BodyCode { get; set; }

        /// <summary>
        ///  field 26
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        ///  field 36
        /// </summary>
        public string FirstRegistrationDate { get; set; }
    }
}

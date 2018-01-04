namespace TechnicalCertificateImageHandler.AppKeys
{
    public class ApplicationKeys
    {
        /// <summary>
        /// Represents Technical Certificate labels type.
        /// </summary>
        public enum LabelTypes
        {
            Label_1_1 = 1,
            Label_1_2 = 2,
            Label_1_3 = 3,
            Label_2_1 = 4,
            Label_2_2 = 5,
            Label_2_3 = 6,
            Label_3_1 = 7,
            Label_3_2 = 8,
            Label_3_3 = 9,
            Label_4_1 = 10,
            Label_4_2 = 11,
            Label_4_3 = 12
        }

        /// <summary>
        /// Represents Technical Certificate labels description.
        /// </summary>
        public class WordLabelCodes
        {
            public static string SONum_L1 = "Schild";
            public static string SONum_L2 = "Plaque";
            public static string SONum_L3 = "Targa";
            public static string SONum_L4 = "Numer";

            public static string TYPE_CODE_L1 = "Art";
            public static string TYPE_CODE_L2 = "Fahrzeugs";
            public static string TYPE_CODE_L3 = "Genre";
            public static string TYPE_CODE_L4 = "véhicule";
            public static string TYPE_CODE_L5 = "Genere";
            public static string TYPE_CODE_L6 = "veicolo";
            public static string TYPE_CODE_L7 = "Gener";
            public static string TYPE_CODE_L8 = "vehichel";

            public static string MARK_AND_MODEL_L1 = "Marke";
            public static string MARK_AND_MODEL_L2 = "Typ";
            public static string MARK_AND_MODEL_L3 = "Marque";
            public static string MARK_AND_MODEL_L4 = "type";
            public static string MARK_AND_MODEL_L5 = "Marca";
            public static string MARK_AND_MODEL_L6 = "tipo";
            public static string MARK_AND_MODEL_L7 = "Marca";
            public static string MARK_AND_MODEL_L8 = "tig";

            public static string CHASSIS_NUM_L1 = "Fahrgestell";
            public static string CHASSIS_NUM_L2 = "Nr.";
            public static string CHASSIS_NUM_L3 = "Chassis";
            public static string CHASSIS_NUM_L4 = "Telaio";
            public static string CHASSIS_NUM_L5 = "Schassis";
            public static string CHASSIS_NUM_L6 = "nr.";

            public static string BODY_CODE_L1 = "Karosserie";
            public static string BODY_CODE_L2 = "Carrosserie";
            public static string BODY_CODE_L3 = "Carrozzeria";
            public static string BODY_CODE_L4 = "Carossaria";

            public static string COLOR_L1 = "Farbe";
            public static string COLOR_L2 = "Couleur";
            public static string COLOR_L3 = "Colore";
            public static string COLOR_L4 = "Colur";

            public static string RECEPTION_NUM_L1 = "Typengenehmigung";
            public static string RECEPTION_NUM_L2 = "Reception";
            public static string RECEPTION_NUM_L3 = "Approvazione";
            public static string RECEPTION_NUM_L4 = "Approvaziun";

            public static string MARTICULE_NUM_L1 = "Stammnummer";
            public static string MARTICULE_NUM_L2 = "matricule";
            public static string MARTICULE_NUM_L3 = "matricola";
            public static string MARTICULE_NUM_L4 = "matricla";

            public static string FIRST_REGISTRATION_DATE_L1 = "Inverkehrsetzung";
            public static string FIRST_REGISTRATION_DATE_L2 = "mise";
            public static string FIRST_REGISTRATION_DATE_L3 = "circulation";
            public static string FIRST_REGISTRATION_DATE_L4 = "messa";
            public static string FIRST_REGISTRATION_DATE_L5 = "circolazione";
            public static string FIRST_REGISTRATION_DATE_L6 = "entrada";
            public static string FIRST_REGISTRATION_DATE_L7 = "circulaziun";
        }

        /// <summary>
        /// Represents Technical Certificate detected data validation errors description.
        /// </summary>
        public class TechnicalCertificateValidation
        {
            /// <summary>
            /// Represents body detection error description.
            /// </summary>
            public const string BODY_ERROR = "Body detection error occured.";

            // <summary>
            /// Represents body code detection error description.
            /// </summary>
            public const string BODY_CODE_ERROR = "Body code detection error occured.";

            // <summary>
            /// Represents chassis number detection error description.
            /// </summary>
            public const string CHASSIS_NUM_ERROR = "Chassis number detection error occured.";

            // <summary>
            /// Represents color detection error description.
            /// </summary>
            public const string COLOR_ERROR = "Body detection error occured.";

            // <summary>
            /// Represents first registration date detection error description.
            /// </summary>
            public const string FIRST_REGISTRATION_DATE_ERROR = "First registration date detection error occured.";

            // <summary>
            /// Represents mark and model detection error description.
            /// </summary>
            public const string MARK_AND_MODEL_ERROR = "Mark and model detection error occured.";

            // <summary>
            /// Represents marticul number detection error description.
            /// </summary>
            public const string MARTICUL_NUM_ERROR = "Marticul number detection error occured.";

            // <summary>
            /// Represents reception number detection error description.
            /// </summary>
            public const string RECEPTION_NUM_ERROR = "Reception number detection error occured.";

            // <summary>
            /// Represents so number detection error description.
            /// </summary>
            public const string SO_NUM_ERROR = "So number detection error occured.";

            // <summary>
            /// Represents type detection error description.
            /// </summary>
            public const string TYPE_ERROR = "Type detection error occured.";

            // <summary>
            /// Represents type code detection error description.
            /// </summary>
            public const string TYPE_CODE_ERROR = "Type code detection error occured.";
        }
    }
}

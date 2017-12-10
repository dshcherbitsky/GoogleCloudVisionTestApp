using System;

namespace TechnicalCertificateImgHandler.Abstractions
{
    public interface IDocumentProcesser
    {
        VehicleCertificateContentDTO Process();
    }
}

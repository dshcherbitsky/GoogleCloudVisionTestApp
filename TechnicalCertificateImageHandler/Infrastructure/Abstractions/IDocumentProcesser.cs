using System;
using TechnicalCertificateImageHandler.Infrastructure.Models;

namespace TechnicalCertificateImageHandler.Infrastructure.Abstractions
{
    public interface IDocumentProcesser
    {
        VehicleCertificateContentDTO Process();
    }
}

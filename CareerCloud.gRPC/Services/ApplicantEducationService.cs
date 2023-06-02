using CareerCloud.BusinessLogicLayer;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService : 
        Protos.ApplicantEducationService.ApplicantEducationServiceBase
    {
        private readonly ApplicantEducationLogic _applicantEducationLogic;
     public   ApplicantEducationService( ApplicantEducationLogic applicantEducationLogic)
        {
            _applicantEducationLogic = applicantEducationLogic;
        }

    }
}

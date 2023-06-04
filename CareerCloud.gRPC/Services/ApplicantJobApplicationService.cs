using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService: ApplicantJobApplication.ApplicantJobApplicationBase
    {
        private readonly ApplicantJobApplicationLogic _applicantJobApplicationLogic;


        public ApplicantJobApplicationService()
        {
            var repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _applicantJobApplicationLogic = new ApplicantJobApplicationLogic(repo);
        }

        public override Task<ApplicantJobApplicationResponse> GetSingle(ApplicantJobApplicationRequest request, ServerCallContext context)
        {
            var result = _applicantJobApplicationLogic.Get(Guid.Parse(request.Id));
            return Task.FromResult<ApplicantJobApplicationResponse>(FromPoco(result));
        }


        public override Task<MultipleJobApplicationResponses> GetAll(Empty request, ServerCallContext context)
        {
            MultipleJobApplicationResponses reply = new MultipleJobApplicationResponses { MultipleJobResponse = { } };
            var applicantJobApplicationPocos = _applicantJobApplicationLogic.GetAll();

            foreach (var poco in applicantJobApplicationPocos)
            {
                reply.MultipleJobResponse.Add(FromPoco(poco));
            }
            return Task.FromResult<MultipleJobApplicationResponses>(reply);
        }



        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplicationResponse request, ServerCallContext context)
        {
           
          

            return new Task<Empty>(() => new Empty());

        }


        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplicationResponse request, ServerCallContext context)
        {





            return new Task<Empty>(() => new Empty());
        }








        private ApplicantJobApplicationResponse FromPoco(ApplicantJobApplicationPoco poco)
        {
            return new ApplicantJobApplicationResponse()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Job = poco.Job.ToString(),
              
                ApplicationDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.ApplicationDate, DateTimeKind.Utc)),
                
               
            };
        }

        private ApplicantJobApplicationPoco ToPoco(ApplicantJobApplicationResponse reply)
        {
            return new ApplicantJobApplicationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Applicant = Guid.Parse(reply.Applicant),
                Job = Guid.Parse(reply.Job),
                
                ApplicationDate = reply.ApplicationDate.ToDateTime(),
                
             

            };
        }


    }
}
    

using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Protos;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using System.Runtime.ConstrainedExecution;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService : Protos.ApplicantEducation.ApplicantEducationBase

    {
        private readonly ApplicantEducationLogic _applicantEducationLogic;


        public ApplicantEducationService()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>();
            _applicantEducationLogic = new ApplicantEducationLogic(repo);
        }

        public override Task<ApplicantEducationResponse> GetSingle(ApplicantEducationRequest request, ServerCallContext context)
        {
            var result = _applicantEducationLogic.Get(Guid.Parse(request.Id));
            return Task.FromResult<ApplicantEducationResponse>(FromPoco(result));
        }


        public override Task<MultipleEducationResponses> GetAll(Empty request, ServerCallContext context)
        {
            MultipleEducationResponses reply = new MultipleEducationResponses { MultipleEduResponse = { } };
            var applicantEducationPocos = _applicantEducationLogic.GetAll();

            foreach (var poco in applicantEducationPocos)
            {
                reply.MultipleEduResponse.Add(FromPoco(poco));
            }
            return Task.FromResult<MultipleEducationResponses>(reply);
        }



        public override Task<Empty> UpdateApplicantEducation(ApplicantEducationResponse request, ServerCallContext context)
        {


            ApplicantEducationPoco[] pocos =
                {   new ApplicantEducationPoco()
              {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Major = request.Major,
                CertificateDiploma = request.CertificateDiploma,
                StartDate = request.StartDate.ToDateTime(),
                CompletionDate = request.CompletionDate.ToDateTime(),
                CompletionPercent = (byte?)request.CompletionPercent

            } };
            _applicantEducationLogic.Update(pocos);
            return new Task<Empty>(() => new Empty());

        }


        public override Task<Empty> DeleteApplicantEducation(ApplicantEducationResponse request, ServerCallContext context)
        {
            ApplicantEducationPoco poco = _applicantEducationLogic.Get(Guid.Parse(request.Id));
            _applicantEducationLogic.Delete(new ApplicantEducationPoco[] { poco });
            return new Task<Empty>(() => new Empty());





        }








        private ApplicantEducationResponse FromPoco(ApplicantEducationPoco poco)
        {
            return new ApplicantEducationResponse()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                CertificateDiploma = poco.CertificateDiploma,
                Major = poco.Major,
                StartDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.StartDate, DateTimeKind.Utc)),
                CompletionDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.CompletionDate, DateTimeKind.Utc)),
                CompletionPercent = poco.CompletionPercent ?? 0,
                Timestamp = ByteString.CopyFrom(poco.TimeStamp)
            };
        }

        private ApplicantEducationPoco ToPoco(ApplicantEducationResponse reply)
        {
            return new ApplicantEducationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Applicant = Guid.Parse(reply.Applicant),
                CertificateDiploma = reply.CertificateDiploma,
                Major = reply.Major,
                StartDate = reply.StartDate.ToDateTime(),
                CompletionDate = reply.CompletionDate.ToDateTime(),
                CompletionPercent = (byte?)reply.CompletionPercent

            };
        }


    }
}

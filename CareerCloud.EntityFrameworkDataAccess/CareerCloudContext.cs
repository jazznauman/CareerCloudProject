using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        private readonly string _connectionString;

        public CareerCloudContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CareerCloudContext()
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
       
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get;set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfile { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set;}
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set;}
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCode { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region ApplicantEducationPoco
            modelBuilder.Entity<ApplicantEducationPoco>()

            .HasOne(p => p.ApplicantProfile)
            .WithMany(p => p.ApplicantEducations)
           .HasForeignKey(p => p.Applicant);
            #endregion

            #region ApplicantJobApplicationPoco
            modelBuilder.Entity<ApplicantJobApplicationPoco>()
            .HasOne(p => p.ApplicantProfile)
            .WithMany(p => p.ApplicantJobApplications)
             .HasForeignKey(p => new { p.Applicant, p.Job });
            #endregion

            #region ApplicantProfilePoco
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(p => p.SystemCountryCode)
                .WithMany(p => p.ApplicantProfiles);
             

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(p => p.SecurityLogin)
               .WithMany(p => p.ApplicantProfiles);
            

            modelBuilder.Entity<ApplicantProfilePoco>()
              
                  .HasOne(p => p.SecurityLogin)
                .WithMany(p => p.ApplicantProfiles)
                 .HasForeignKey(p => new { p.Login, p.SystemCountryCode });
               

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(p => p.SystemCountryCode)
                .WithMany(p => p.ApplicantProfiles);
            #endregion

            #region ApplicantSkillPoco
            modelBuilder.Entity<ApplicantSkillPoco>()
            
                .HasOne(p => p.ApplicantProfile)
                .WithMany(p => p.ApplicantSkills)
                .HasForeignKey(p => p.Applicant);

            #endregion


           


            #region ApplicantResumePoco
            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(p => p.ApplicantProfile)
                .WithMany(p => p.ApplicantResumes)
                .HasForeignKey(p => p.Applicant);
            #endregion

#region CompanyDescriptionPoco

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(p => p.CompanyProfile)
                .WithMany(p =>p.CompanyDescriptions);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(p => p.SystemLanguageCode)
                .WithMany(p =>p.CompanyDescriptions)

           
               .HasForeignKey(c => new { c.Company, c.LanguageId });
            #endregion

#region CompanyJobDescriptionPoco
            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .HasOne(p => p.CompanyJob)
                .WithMany(p => p.CompanyJobDescriptions)

            
               .HasForeignKey(p => p.Job);

            #endregion

#region CompanyJobEducationPoco
            modelBuilder.Entity<CompanyJobEducationPoco>()
                .HasOne(p => p.CompanyJob)
                .WithMany(p => p.CompanyJobEducations)
                .HasForeignKey(p => p.Job);
            #endregion

#region CompanyJobPoco
            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(p => p.CompanyProfile)
                .WithMany(p => p.CompanyJobs)
                .HasForeignKey(c => c.Company);
            #endregion

#region CompanyLocationPoco
            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(p=>p.CompanyProfile)
                .WithMany(p=>p.CompanyLocations)
                .HasForeignKey(c => c.Company)
                ;





          
            #endregion

            #region ApplicantWorkHistoryPoco

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(p => p.SystemCountryCode)
            .WithMany(p => p.ApplicantWorkHistorys);



         modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(p => p.ApplicantProfile)
                .WithMany(p => p.ApplicantWorkHistorys)
                .HasForeignKey(a => new { a.Applicant, a.CountryCode });

            #endregion

            #region SecurityLoginsLogPoco


            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(p => p.SecurityLogin)
                .WithMany(p => p.SecurityLoginsLogs)
                .HasForeignKey(l => l.Login);
            #endregion

#region SecurityLoginsRolePoco



            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(p => p.SecurityLogin)
                .WithMany(p => p.SecurityLoginsRoles)
                .HasForeignKey(l=>new { l.Login, l.Role });

            #endregion

            #region CompanyJobSkillPoco
            modelBuilder.Entity<CompanyJobSkillPoco>()
               .HasKey(i => i.Id);

            #endregion

            #region SystemCountryCodePoco
            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasKey(c => c.Code);

            #endregion

#region SecurityLoginPoco
            modelBuilder.Entity<SecurityLoginPoco>()
                .HasKey(p => p.Id);
            #endregion

#region SystemLanguageCodePoco
            modelBuilder.Entity<SystemLanguageCodePoco>()
                .HasKey(l => l.LanguageID);
            #endregion
           
#region CompanyProfilePoco


            modelBuilder.Entity<CompanyProfilePoco>()
                .HasKey(p => p.Id);

            #endregion

#region SecurityLoginsRolePoco


            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(p => p.SecurityRole)
                .WithMany(p => p.SecurityLoginsRoles);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasKey(p => p.Id);

            #endregion

#region SecurityRolePoco


            modelBuilder.Entity<SecurityRolePoco>()
                .HasKey(p => p.Id);



            #endregion

































        }



    }

}
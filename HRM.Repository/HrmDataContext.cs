using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Effort;
using HRM.Domain.Base;
using HRM.Domain.Entity;
using HRM.Domain.ReportModel;
using HRM.Repository.Base;
using HRM.Repository.Initializer;

namespace HRM.Repository
{
    public class HrmDataContext : DbContext, IContext
    {

        //public static DbConnection Connection = DbConnectionFactory.CreateTransient();

        //public HrmDataContext() : base(Connection, true)
        //{

        //}

        private static readonly bool[] _migrated = { false };
        public HrmDataContext() 
            : base()
        {
            //Database.SetInitializer<HrmDataContext>(null);
            Database.SetInitializer<HrmDataContext>(new DatabaseInitializer());
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Department> Departments { get; set; }
        public IDbSet<Company> Companies { get; set; }
        public IDbSet<CompanyContact> CompanyContacts { get; set; }
        public IDbSet<CompanyChangeHistory> CompanyChangeHistories { get; set; }
        public IDbSet<Country> Countries { get; set; }
        public IDbSet<EthnicGroup> EthnicGroups { get; set; }
        public IDbSet<Religion> Religions { get;set; }
        public IDbSet<Relation> Relations { get; set; }
        public IDbSet<Education> Educations { get; set; }
        public IDbSet<Skill> Skills { get;set; }
        public IDbSet<ContractType> ContractTypes { get; set; }
        public IDbSet<DocumentType> DocumentTypes { get; set; }
        public IDbSet<Test> Tests { get; set; }
        public IDbSet<WorkTitle> WorkTitles { get; set; }
        public IDbSet<Achievement> Achievements{ get; set; }
        public IDbSet<Contract> Contracts { get; set; }        
        public IDbSet<Decision> Decisions { get; set; }
        public IDbSet<DecisionType> DecisionTypes { get; set; }
        public IDbSet<DepartmentEmployee> DepartmentEmployees { get; set; }
        public IDbSet<Document> Documents { get; set; }
        public IDbSet<SalaryHistory> SalaryHistories { get; set; }
        public IDbSet<SalaryLevel> SalaryLevels { get; set; }
        public IDbSet<WorkContract> WorkContracts { get; set; }
        public IDbSet<WorkTitleDetail> WorkTitleDetails { get; set; }
        public IDbSet<DateOffHistory> DateOffHistories { get; set; }
        public IDbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public IDbSet<TimeAttendanceType> TimeAttendanceTypes { get; set; }
        public IDbSet<Timekeeping> Timekeepings { get; set; }
        public IDbSet<Report01Model> Report_01 { get; set; }
        public IDbSet<Report02Model> Report_02 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Report01Model>()
                .HasKey(x => x.Id)
                .ToTable("Report_01");
            modelBuilder.Entity<Report02Model>()
               .HasKey(x => x.Id)
               .ToTable("Report_02");

            modelBuilder.Entity<User>()
                .HasMany(x => x.Roles);
            modelBuilder.Entity<Role>()
                .HasMany(x => x.Users);

            modelBuilder.Entity<Relation>()
            .HasRequired(c => c.Employee)
            .WithMany()
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Relation>()
            .HasRequired(s => s.RelationWithEmployee)
            .WithMany()
            .WillCascadeOnDelete(false);

        }
        

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                if (!(entry.Entity is IAuditableEntity entity)) continue;
                var identityName = Thread.CurrentPrincipal.Identity?.Name;
                
                if (String.IsNullOrEmpty(identityName)) identityName = "Anonymoust";

                var now = DateTime.UtcNow;

                if (entry.State == System.Data.Entity.EntityState.Added)
                {
                    entity.CreatedBy = identityName;
                    entity.CreatedDate = now;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedBy = identityName;
                entity.UpdatedDate = now;
            }

            return base.SaveChanges();
        }

    }
}

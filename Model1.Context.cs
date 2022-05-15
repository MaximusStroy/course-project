﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vacancy
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class RecruitmentAgencyEntities : DbContext
    {
        public RecruitmentAgencyEntities()
            : base("name=RecruitmentAgencyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<applicants> applicants { get; set; }
        public virtual DbSet<companyMany> companyMany { get; set; }
        public virtual DbSet<competencies> competencies { get; set; }
        public virtual DbSet<employers> employers { get; set; }
        public virtual DbSet<responses> responses { get; set; }
        public virtual DbSet<resume_tab> resume_tab { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<types_users> types_users { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<vacancy> vacancy { get; set; }
        public virtual DbSet<vacancyView> vacancyView { get; set; }
    
        [DbFunction("RecruitmentAgencyEntities", "procStr")]
        public virtual IQueryable<procStr_Result> procStr(string s)
        {
            var sParameter = s != null ?
                new ObjectParameter("s", s) :
                new ObjectParameter("s", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<procStr_Result>("[RecruitmentAgencyEntities].[procStr](@s)", sParameter);
        }
    
        [DbFunction("RecruitmentAgencyEntities", "selectCompany")]
        public virtual IQueryable<selectCompany_Result> selectCompany(Nullable<int> idEmp)
        {
            var idEmpParameter = idEmp.HasValue ?
                new ObjectParameter("idEmp", idEmp) :
                new ObjectParameter("idEmp", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<selectCompany_Result>("[RecruitmentAgencyEntities].[selectCompany](@idEmp)", idEmpParameter);
        }
    
        [DbFunction("RecruitmentAgencyEntities", "selectProfile")]
        public virtual IQueryable<selectProfile_Result> selectProfile(Nullable<int> p_idUser)
        {
            var p_idUserParameter = p_idUser.HasValue ?
                new ObjectParameter("p_idUser", p_idUser) :
                new ObjectParameter("p_idUser", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<selectProfile_Result>("[RecruitmentAgencyEntities].[selectProfile](@p_idUser)", p_idUserParameter);
        }
    
        [DbFunction("RecruitmentAgencyEntities", "selectResponsesApp")]
        public virtual IQueryable<selectResponsesApp_Result> selectResponsesApp(Nullable<int> idApp)
        {
            var idAppParameter = idApp.HasValue ?
                new ObjectParameter("idApp", idApp) :
                new ObjectParameter("idApp", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<selectResponsesApp_Result>("[RecruitmentAgencyEntities].[selectResponsesApp](@idApp)", idAppParameter);
        }
    
        [DbFunction("RecruitmentAgencyEntities", "selectResponsesEmp")]
        public virtual IQueryable<selectResponsesEmp_Result> selectResponsesEmp(Nullable<int> idApp)
        {
            var idAppParameter = idApp.HasValue ?
                new ObjectParameter("idApp", idApp) :
                new ObjectParameter("idApp", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<selectResponsesEmp_Result>("[RecruitmentAgencyEntities].[selectResponsesEmp](@idApp)", idAppParameter);
        }
    
        [DbFunction("RecruitmentAgencyEntities", "selectResume")]
        public virtual IQueryable<selectResume_Result> selectResume(Nullable<int> idRes)
        {
            var idResParameter = idRes.HasValue ?
                new ObjectParameter("idRes", idRes) :
                new ObjectParameter("idRes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<selectResume_Result>("[RecruitmentAgencyEntities].[selectResume](@idRes)", idResParameter);
        }
    
        [DbFunction("RecruitmentAgencyEntities", "selectResumeComboBox")]
        public virtual IQueryable<selectResumeComboBox_Result> selectResumeComboBox(Nullable<int> idRes)
        {
            var idResParameter = idRes.HasValue ?
                new ObjectParameter("idRes", idRes) :
                new ObjectParameter("idRes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<selectResumeComboBox_Result>("[RecruitmentAgencyEntities].[selectResumeComboBox](@idRes)", idResParameter);
        }
    
        [DbFunction("RecruitmentAgencyEntities", "selectVacancy")]
        public virtual IQueryable<selectVacancy_Result> selectVacancy()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<selectVacancy_Result>("[RecruitmentAgencyEntities].[selectVacancy]()");
        }
    
        public virtual int dropResume(Nullable<int> idRes)
        {
            var idResParameter = idRes.HasValue ?
                new ObjectParameter("idRes", idRes) :
                new ObjectParameter("idRes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dropResume", idResParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int dropVacancy(Nullable<int> idRes)
        {
            var idResParameter = idRes.HasValue ?
                new ObjectParameter("idRes", idRes) :
                new ObjectParameter("idRes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dropVacancy", idResParameter);
        }
    }
}

using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;
using Enrollment.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Enrollment.Bsl.Flow.Integration.Tests
{
    internal static class DatabaseSeeder
    {
        internal static async Task Seed_Database(IEnrollmentRepository repository)
        {
            if ((await repository.CountAsync<LookUpsModel, LookUps>()) > 0)
                return;//database has been seeded

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Path.Combine(Directory.GetCurrentDirectory(), "DropDowns.xml"));

            IList<LookUpsModel> lookUps = xDoc.SelectNodes("//list")
                .OfType<XmlElement>()
                .SelectMany
                (
                    e => e.ChildNodes.OfType<XmlElement>()
                    .Where(c => c.Name == "item")
                    .Select
                    (
                        i =>
                        {
                            if (new HashSet<string> { "isVeteran", "receivedGed", "creditHoursAtCmc", "yesNo" }.Contains(e.Attributes["id"].Value))
                                return new LookUpsModel
                                {
                                    ListName = e.Attributes["id"].Value,
                                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                                    BooleanValue = bool.Parse(i.Attributes["name"].Value),
                                    Text = i.Attributes["value"].Value,
                                    Order = 0
                                };
                            else
                                return new LookUpsModel
                                {
                                    ListName = e.Attributes["id"].Value,
                                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                                    Value = i.Attributes["name"].Value,
                                    Text = i.Attributes["value"].Value,
                                    Order = 0
                                };
                        }
                    )
                ).ToList();

            await repository.SaveGraphsAsync<LookUpsModel, LookUps>(lookUps);

            UserModel[] users = new UserModel[]
            {
                new UserModel
                {
                    UserName = "ForeignStudent01",
                    Residency = new ResidencyModel
                    {
                        CitizenshipStatus = "US",
                        DriversLicenseNumber = "NC12345",
                        DriversLicenseState = "NC",
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        HasValidDriversLicense = true,
                        ImmigrationStatus = "AA",
                        ResidentState = "AR",
                        StatesLivedIn = new List<StateLivedInModel>
                        {
                            new StateLivedInModel { EntityState = LogicBuilder.Domain.EntityStateType.Added, State = "OH"  },
                            new StateLivedInModel { EntityState = LogicBuilder.Domain.EntityStateType.Added, State = "TN"  }
                        }
                    },
                    Academic = new AcademicModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        AttendedPriorColleges = true,
                        FromDate = new DateTime(2010, 10, 10),
                        ToDate = new DateTime(2014, 10, 10),
                        GraduationStatus = "H",
                        EarnedCreditAtCmc = true,
                        LastHighSchoolLocation = "NC",
                        NcHighSchoolName = "NCSCHOOL1",
                        Institutions = new List<InstitutionModel>
                        {
                            new InstitutionModel
                            {
                                EntityState = LogicBuilder.Domain.EntityStateType.Added,
                                HighestDegreeEarned = "BD",
                                StartYear = "2015",
                                EndYear = "2018",
                                InstitutionName = "Florida Institution 1",
                                InstitutionState = "FL",
                                MonthYearGraduated = new DateTime(2020, 10, 10)
                            }
                        }
                    },
                    Admissions = new AdmissionsModel
                    {
                        EnteringStatus = "1",
                        EnrollmentTerm = "FA",
                        EnrollmentYear = "2021",
                        ProgramType = "degreePrograms",
                        Program = "degreeProgram1",
                        EntityState = LogicBuilder.Domain.EntityStateType.Added
                    },
                    EntityState = LogicBuilder.Domain.EntityStateType.Added
                },
                new UserModel
                {
                    UserName = "DomesticStudent01",
                    Residency = new ResidencyModel
                    {
                        CitizenshipStatus = "RA",
                        CountryOfCitizenship = "AA",
                        DriversLicenseNumber = "GA12345",
                        DriversLicenseState = "DA",
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        HasValidDriversLicense = true,
                        ImmigrationStatus = "BB",
                        ResidentState = "AR",
                        StatesLivedIn = new List<StateLivedInModel>
                        {
                            new StateLivedInModel { EntityState = LogicBuilder.Domain.EntityStateType.Added, State = "GA"  },
                            new StateLivedInModel { EntityState = LogicBuilder.Domain.EntityStateType.Added, State = "TN" }
                        }
                    },
                    Academic = new AcademicModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        AttendedPriorColleges = true,
                        FromDate = new DateTime(2010, 10, 10),
                        ToDate = new DateTime(2014, 10, 10),
                        GraduationStatus = "CSD",
                        EarnedCreditAtCmc = false,
                        LastHighSchoolLocation = "NC",
                        NcHighSchoolName = "NCSCHOOL1",
                        Institutions = new List<InstitutionModel>
                        {
                            new InstitutionModel
                            {
                                EntityState = LogicBuilder.Domain.EntityStateType.Added,
                                HighestDegreeEarned = "CT",
                                StartYear = "2016",
                                EndYear = "2019",
                                InstitutionName = "Florida Institution 1",
                                InstitutionState = "FL",
                                MonthYearGraduated = new DateTime(2020, 10, 10)
                            }
                        }
                    },
                    Admissions = new AdmissionsModel
                    {
                        EnteringStatus = "1",
                        EnrollmentTerm = "FA",
                        EnrollmentYear = "2021",
                        ProgramType = "degreePrograms",
                        Program = "degreeProgram1",
                        EntityState = LogicBuilder.Domain.EntityStateType.Added
                    },
                    EntityState = LogicBuilder.Domain.EntityStateType.Added
                }
            };

            await repository.SaveGraphsAsync<UserModel, User>(users);
        }
    }
}

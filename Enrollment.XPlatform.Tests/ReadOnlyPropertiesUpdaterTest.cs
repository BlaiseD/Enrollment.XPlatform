using AutoMapper;
using Enrollment.Domain.Entities;
using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Tests.Mocks;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Enrollment.XPlatform.Tests
{
    public class ReadOnlyPropertiesUpdaterTest
    {
        public ReadOnlyPropertiesUpdaterTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapResidencyModelToIReadOnlyList()
        {
            //arrange
            ResidencyModel residency = new ResidencyModel
            {
                UserId = 3,
                CitizenshipStatus = "US",
                ResidentState = "OH",
                HasValidDriversLicense = true,
                StatesLivedIn = new List<StateLivedInModel>
                {
                    new StateLivedInModel
                    {
                        StateLivedInId = 1,
                        UserId = 3,
                        State = "GA"
                    },
                    new StateLivedInModel
                    {
                        StateLivedInId = 2,
                        UserId = 3,
                        State = "MI"
                    },
                    new StateLivedInModel
                    {
                        StateLivedInId = 3,
                        UserId = 3,
                        State = "OH"
                    }
                }
            };
            ObservableCollection<IReadOnly> properties = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.ResidencyForm
            );

            //act
            serviceProvider.GetRequiredService<IReadOnlyPropertiesUpdater>().UpdateProperties
            (
                properties,
                residency,
                ReadOnlyDescriptors.ResidencyForm.FieldSettings
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["UserId"]);
            Assert.Equal("US", propertiesDictionary["CitizenshipStatus"]);
            Assert.Equal("OH", propertiesDictionary["ResidentState"]);
            Assert.Equal(true, propertiesDictionary["HasValidDriversLicense"]);
            Assert.Equal("GA", ((IEnumerable<StateLivedInModel>)propertiesDictionary["StatesLivedIn"]).First().State);
        }

        [Fact]
        public void MapAcademicModelToIReadOnlyList()
        {
            //arrange
            AcademicModel academic = new AcademicModel
            {
                UserId = 1,
                LastHighSchoolLocation = "NC",
                NcHighSchoolName = "NCSCHOOL1",
                FromDate = new DateTime(2021, 5, 20),
                ToDate = new DateTime(2021, 5, 20),
                GraduationStatus = "DP",
                EarnedCreditAtCmc = true,
                Institutions = new List<InstitutionModel>
                {
                    new InstitutionModel
                    {
                        InstitutionId = 1,
                        InstitutionState = "FL",
                        InstitutionName = "I1",
                        StartYear = "2011",
                        EndYear = "2013",
                        HighestDegreeEarned = "CT"
                    },
                    new InstitutionModel
                    {
                        InstitutionId = 2,
                        InstitutionState = "GA",
                        InstitutionName = "I1",
                        StartYear = "2012",
                        EndYear = "2014",
                        HighestDegreeEarned = "DP"
                    },
                    new InstitutionModel
                    {
                        InstitutionId = 3,
                        InstitutionState = "FL",
                        InstitutionName = "I2",
                        StartYear = "2014",
                        EndYear = "2015",
                        HighestDegreeEarned = "MA"
                    }
                }
            };
            ObservableCollection<IReadOnly> properties = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.AcademicForm
            );

            //act
            serviceProvider.GetRequiredService<IReadOnlyPropertiesUpdater>().UpdateProperties
            (
                properties,
                academic,
                ReadOnlyDescriptors.AcademicForm.FieldSettings
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);

            Assert.Equal(1, propertiesDictionary["UserId"]);
            Assert.Equal("NC", propertiesDictionary["LastHighSchoolLocation"]);
            Assert.Equal("NCSCHOOL1", propertiesDictionary["NcHighSchoolName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["FromDate"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["ToDate"]);
            Assert.Equal("2011", ((IEnumerable<InstitutionModel>)propertiesDictionary["Institutions"]).First().StartYear);
        }

        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            if (MapperConfiguration == null)
            {
                MapperConfiguration = new MapperConfiguration(cfg =>
                {
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddSingleton<UiNotificationService, UiNotificationService>()
                .AddSingleton<IFlowManager, FlowManager>()
                .AddSingleton<FlowActivityFactory, FlowActivityFactory>()
                .AddSingleton<DirectorFactory, DirectorFactory>()
                .AddSingleton<FlowDataCache, FlowDataCache>()
                .AddSingleton<ScreenData, ScreenData>()
                .AddSingleton<IAppLogger, AppLoggerMock>()
                .AddSingleton<IRulesCache, RulesCacheMock>()
                .AddSingleton<IDialogFunctions, DialogFunctions>()
                .AddSingleton<IActions, Actions>()
                .AddSingleton<IFieldsCollectionBuilder, FieldsCollectionBuilder>()
                .AddSingleton<IReadOnlyFieldsCollectionBuilder, ReadOnlyFieldsCollectionBuilder>()
                .AddSingleton<IConditionalValidationConditionsBuilder, ConditionalValidationConditionsBuilder>()
                .AddSingleton<IHideIfConditionalDirectiveBuilder, HideIfConditionalDirectiveBuilder>()
                .AddSingleton<IGetItemFilterBuilder, GetItemFilterBuilder>()
                .AddSingleton<ISearchSelectorBuilder, SearchSelectorBuilder>()
                .AddSingleton<IEntityStateUpdater, EntityStateUpdater>()
                .AddSingleton<IEntityUpdater, EntityUpdater>()
                .AddSingleton<IPropertiesUpdater, PropertiesUpdater>()
                .AddSingleton<IReadOnlyPropertiesUpdater, ReadOnlyPropertiesUpdater>()
                .AddSingleton<IContextProvider, ContextProvider>()
                .AddHttpClient()
                .AddSingleton<IHttpService, HttpServiceMock>()
                .BuildServiceProvider();
        }
    }
}

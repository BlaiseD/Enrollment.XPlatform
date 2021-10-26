﻿using AutoMapper;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Bsl.Flow.Cache;
using Enrollment.Bsl.Flow.Services;
using Enrollment.Repositories;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Enrollment.Bsl.Flow
{
    public class FlowManager : IFlowManager
    {
        public FlowManager(IMapper mapper,
            ICustomActions customActions,
            IGetItemFilterBuilder getItemFilterBuilder,
            DirectorFactory directorFactory,
            FlowActivityFactory flowActivityFactory,
            IEnrollmentRepository enrollmentRepository,
            ILogger<FlowManager> logger, 
            Progress progress,
            FlowDataCache flowDataCache)
        {
            this.CustomActions = customActions;
            this.GetItemFilterBuilder = getItemFilterBuilder;
            this.logger = logger;
            this.EnrollmentRepository = enrollmentRepository;
            this.Mapper = mapper;
            this.Progress = progress;
            this.FlowDataCache = flowDataCache;
            this.Director = directorFactory.Create(this);
            this.FlowActivity = flowActivityFactory.Create(this);
        }

        public IFlowActivity FlowActivity { get; }
        public FlowDataCache FlowDataCache { get; }
        public Progress Progress { get; }
        public ICustomActions CustomActions { get; }
        public IGetItemFilterBuilder GetItemFilterBuilder { get; }

        private ILogger<FlowManager> logger;

        public IEnrollmentRepository EnrollmentRepository { get; }
        public IMapper Mapper { get; }
        public DirectorBase Director { get; }

        public void FlowComplete()
        {
            if (FlowDataCache.Response == null)
            {
                logger.LogError("Response cannot be null.");
                throw new InvalidOperationException("Response cannot be null.");
            }
        }

        public void SetCurrentBusinessBackupData() {}

        public void Terminate() => throw new NotImplementedException();

        public void Start(string module)
        {
            try
            {
                System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
                this.Director.StartInitialFlow(module);
                stopWatch.Stop();
                logger.LogInformation("this.Director.StartInitialFlow: {0}", stopWatch.Elapsed.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                FlowDataCache.Response = new ErrorResponse
                {
                    Success = false,
                    ErrorMessages = new List<string> { ex.Message }
                };
                logger.LogWarning(0, string.Format("Progress Start {0}", JsonSerializer.Serialize(this.Progress)));
                this.logger.LogError(ex, ex.Message);
            }
        }
    }
}

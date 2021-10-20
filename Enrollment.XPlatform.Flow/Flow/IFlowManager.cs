﻿using AutoMapper;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Flow.Requests;
using Enrollment.XPlatform.Flow.Settings;
using LogicBuilder.RulesDirector;
using System.Threading.Tasks;

namespace Enrollment.XPlatform.Flow
{
    public interface IFlowManager
    {
        Progress Progress { get; }
        DirectorBase Director { get; }
        FlowDataCache FlowDataCache { get; }
        IDialogFunctions DialogFunctions { get; }
        IActions Actions { get; }
        IFlowActivity FlowActivity { get; }
        IMapper Mapper { get; }

        Task<FlowSettings> Start(string module);
        Task<FlowSettings> Next(CommandButtonRequest request);
        Task<FlowSettings> NavStart(NavBarRequest navBarRequest);
        void FlowComplete();
        void Wait();
        void Terminate();
        void SetCurrentBusinessBackupData();
    }
}
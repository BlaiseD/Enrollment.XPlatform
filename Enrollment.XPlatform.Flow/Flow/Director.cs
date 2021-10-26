using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Flow
{
    public class Director : AppDirectorBase
    {
        public Director(IFlowManager flowManager, IRulesCache rulesCache)
        {
            this._flowManager = flowManager;
            this._rulesCache = rulesCache;
        }

        #region Fields
        private readonly IFlowManager _flowManager;
        private readonly IRulesCache _rulesCache;
        #endregion Fields

        #region Properties
        protected override Progress Progress => this._flowManager.Progress;
        protected override IRulesCache RulesCache => this._rulesCache;
        protected override IFlowActivity FlowActivity => this._flowManager.FlowActivity;
        #endregion Properties

        #region Methods
        public override void SetCurrentBusinessBackupData() => this._flowManager.SetCurrentBusinessBackupData();
        #endregion Methods
    }
}

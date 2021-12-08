using AutoMapper;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.Forms.Configuration.ListForm;
using Enrollment.Forms.Configuration.SearchForm;
using Enrollment.Forms.Configuration.TextForm;
using Enrollment.Forms.Parameters.EditForm;
using Enrollment.Forms.Parameters.ListForm;
using Enrollment.Forms.Parameters.SearchForm;
using Enrollment.Forms.Parameters.TextForm;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Flow.Settings.Screen;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Flow
{
    public class DialogFunctions : IDialogFunctions
    {
        public DialogFunctions(ScreenData screenData, IMapper mapper)
        {
            this.screenData = screenData;
            this.mapper = mapper;
        }

        #region Fields
        private readonly ScreenData screenData;
        private readonly IMapper mapper;
        #endregion Fields

        public void DisplayEditCollection([Comments("Configuration details for the form.")] SearchFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<SearchFormSettingsDescriptor>
            (
                mapper.Map<SearchFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.SearchPage
            );
        }

        public void DisplayEditForm([Comments("Configuration details for the form.")] EditFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<EditFormSettingsDescriptor>
            (
                mapper.Map<EditFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.EditForm
            );
        }

        public void DisplayDetailForm([Comments("Configuration details for the form.")] EditFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<EditFormSettingsDescriptor>
            (
                mapper.Map<EditFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.DetailForm
            );
        }

        public void DisplayReadOnlyCollection([Comments("Configuration details for the form.")] ListFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<ListFormSettingsDescriptor>
            (
                mapper.Map<ListFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.ListPage
            );
        }

        public void DisplayTextForm([Comments("Configuration details for the form.")] TextFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<TextFormSettingsDescriptor>
            (
                mapper.Map<TextFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.TextPage
            );
        }
    }
}

﻿using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Enrollment.XPlatform.Services
{
    public class EntityUpdater : IEntityUpdater
    {
        private readonly IMapper mapper;

        public EntityUpdater(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TModel ToModelObject<TModel>(ObservableCollection<IValidatable> properties, List<FormItemSettingsDescriptor> fieldSettings, TModel destination = null) where TModel : class
        {
            return properties.ToModelObject(this.mapper, fieldSettings, destination);
        }

        public object ToModelObject(Type modelType, ObservableCollection<IValidatable> properties, List<FormItemSettingsDescriptor> fieldSettings, object destination = null)
        {
            return properties.ToModelObject(modelType, this.mapper, fieldSettings, destination);
        }
    }
}

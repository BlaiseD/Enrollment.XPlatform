using Enrollment.Forms.Configuration.EditForm;
using System;
using System.Reflection;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    internal static class ValidatableObjectFactory
    {
        private static readonly DateTime DefaultDateTime = new DateTime(1900, 1, 1);

        public static IValidatable GetValidatable(object validatable, FormControlSettingsDescriptor setting)
        {
            ((IValidatable)validatable).Value = GetValue(setting);
            return (IValidatable)validatable;
        }

        public static object GetValue(FormControlSettingsDescriptor setting)
            => typeof(ValidatableObjectFactory)
                .GetMethod
                (
                    "_GetValue",
                    1,
                    BindingFlags.NonPublic | BindingFlags.Static,
                    null,
                    new Type[]
                    {
                        typeof(FormControlSettingsDescriptor)
                    },
                    null
                )
                .MakeGenericMethod(Type.GetType(setting.Type))
                .Invoke(null, new object[] { setting });

        private static T _GetValue<T>(FormControlSettingsDescriptor setting, object defaultValue)
        {
            if (setting.ValidationSetting?.DefaultValue != null 
                && setting.ValidationSetting.DefaultValue.GetType() != typeof(T))
                throw new ArgumentException($"{nameof(setting.ValidationSetting.DefaultValue)}: 323DA51E-BCA1-4017-A32F-A9FEF6477393");

            return (T)(setting.ValidationSetting?.DefaultValue ?? defaultValue);
        }

        private static T _GetValue<T>(FormControlSettingsDescriptor setting) 
            => _GetValue<T>
            (
                setting, 
                typeof(T) == typeof(DateTime) ? DefaultDateTime : default(T)
            );

        //public static IValidatable GetValidatable(object validatable, object validationDefaultValue, string type)
        //{
        //    ((IValidatable)validatable).Value = GetValue(validationDefaultValue, type);
        //    return (IValidatable)validatable;
        //}

        //private static object GetValue(object validationDefaultValue, string type)
        //    => typeof(ValidatableObjectFactory)
        //        .GetMethod
        //        (
        //            "_GetValue",
        //            1,
        //            BindingFlags.NonPublic | BindingFlags.Static,
        //            null,
        //            new Type[]
        //            {
        //                typeof(object)
        //            },
        //            null
        //        )
        //        .MakeGenericMethod(Type.GetType(type))
        //        .Invoke(null, new object[] { validationDefaultValue, });

        //private static T _GetValue<T>(object validationDefaultValue)
        //{
        //    if (validationDefaultValue != null
        //        && validationDefaultValue.GetType() != typeof(T))
        //        throw new ArgumentException($"{nameof(validationDefaultValue)}: 3564E480-C947-489B-BF04-812CE35BAD74");

        //    return (T)GetValue(typeof(T) == typeof(DateTime) ? DefaultDateTime : default(T));

        //    T GetValue(object typeDefault) 
        //        => (T)(validationDefaultValue ?? typeDefault);
        //}
    }
}

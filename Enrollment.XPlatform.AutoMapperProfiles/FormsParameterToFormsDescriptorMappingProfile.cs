using AutoMapper;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.DetailForm;
using Enrollment.Forms.Configuration.Directives;
using Enrollment.Forms.Configuration.EditForm;
using Enrollment.Forms.Configuration.ListForm;
using Enrollment.Forms.Configuration.Navigation;
using Enrollment.Forms.Configuration.SearchForm;
using Enrollment.Forms.Configuration.TextForm;
using Enrollment.Forms.Configuration.Validation;
using Enrollment.Forms.Parameters;
using Enrollment.Forms.Parameters.Bindings;
using Enrollment.Forms.Parameters.DetailForm;
using Enrollment.Forms.Parameters.Directives;
using Enrollment.Forms.Parameters.EditForm;
using Enrollment.Forms.Parameters.ListForm;
using Enrollment.Forms.Parameters.Navigation;
using Enrollment.Forms.Parameters.SearchForm;
using Enrollment.Forms.Parameters.TextForm;
using Enrollment.Forms.Parameters.Validation;

namespace Enrollment.XPlatform.AutoMapperProfiles
{
    public class FormsParameterToFormsDescriptorMappingProfile : Profile
    {
        public FormsParameterToFormsDescriptorMappingProfile()
        {
			CreateMap<DetailControlSettingsParameters, DetailControlSettingsDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<DetailFormSettingsParameters, DetailFormSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<DetailGroupArraySettingsParameters, DetailGroupArraySettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<DetailGroupBoxSettingsParameters, DetailGroupBoxSettingsDescriptor>();
			CreateMap<DetailGroupSettingsParameters, DetailGroupSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<DirectiveArgumentParameters, DirectiveArgumentDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<DirectiveDefinitionParameters, DirectiveDefinitionDescriptor>();
			CreateMap<DirectiveParameters, DirectiveDescriptor>();
			CreateMap<DropDownTemplateParameters, DropDownTemplateDescriptor>();
			CreateMap<EditFormSettingsParameters, EditFormSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<FieldValidationSettingsParameters, FieldValidationSettingsDescriptor>();
			CreateMap<FormattedLabelItemParameters, FormattedLabelItemDescriptor>();
			CreateMap<FormControlSettingsParameters, FormControlSettingsDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<FormGroupArraySettingsParameters, FormGroupArraySettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<FormGroupSettingsParameters, FormGroupSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<FormGroupTemplateParameters, FormGroupTemplateDescriptor>();
			CreateMap<FormRequestDetailsParameters, FormRequestDetailsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
				.ForMember(dest => dest.DataType, opts => opts.MapFrom(x => x.DataType.AssemblyQualifiedName));
			CreateMap<FormsCollectionDisplayTemplateParameters, FormsCollectionDisplayTemplateDescriptor>();
			CreateMap<FormGroupBoxSettingsParameters, FormGroupBoxSettingsDescriptor>();
			CreateMap<HyperLinkLabelItemParameters, HyperLinkLabelItemDescriptor>();
			CreateMap<HyperLinkSpanItemParameters, HyperLinkSpanItemDescriptor>();
			CreateMap<ItemBindingParameters, ItemBindingDescriptor>();
			CreateMap<LabelItemParameters, LabelItemDescriptor>();
			CreateMap<ListFormSettingsParameters, ListFormSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<MultiBindingParameters, MultiBindingDescriptor>();
			CreateMap<MultiSelectDetailControlSettingsParameters, MultiSelectDetailControlSettingsDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<MultiSelectFormControlSettingsParameters, MultiSelectFormControlSettingsDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<MultiSelectTemplateParameters, MultiSelectTemplateDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<NavigationBarParameters, NavigationBarDescriptor>();
			CreateMap<NavigationMenuItemParameters, NavigationMenuItemDescriptor>();
			CreateMap<RequestDetailsParameters, RequestDetailsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
				.ForMember(dest => dest.DataType, opts => opts.MapFrom(x => x.DataType.AssemblyQualifiedName))
				.ForMember(dest => dest.ModelReturnType, opts => opts.MapFrom(x => x.ModelReturnType.AssemblyQualifiedName))
				.ForMember(dest => dest.DataReturnType, opts => opts.MapFrom(x => x.DataReturnType.AssemblyQualifiedName));
			CreateMap<SearchFilterGroupParameters, SearchFilterGroupDescriptor>();
			CreateMap<SearchFilterParameters, SearchFilterDescriptor>();
			CreateMap<SearchFormSettingsParameters, SearchFormSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<SpanItemParameters, SpanItemDescriptor>();
			CreateMap<TextFieldTemplateParameters, TextFieldTemplateDescriptor>();
			CreateMap<TextFormSettingsParameters, TextFormSettingsDescriptor>();
			CreateMap<TextGroupParameters, TextGroupDescriptor>();
			CreateMap<ValidationMessageParameters, ValidationMessageDescriptor>();
			CreateMap<ValidationRuleParameters, ValidationRuleDescriptor>();
			CreateMap<ValidatorArgumentParameters, ValidatorArgumentDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<ValidatorDefinitionParameters, ValidatorDefinitionDescriptor>();
			CreateMap<VariableDirectivesParameters, VariableDirectivesDescriptor>();

            CreateMap<FormItemSettingsParameters, FormItemSettingsDescriptor>()
				.Include<FormControlSettingsParameters, FormControlSettingsDescriptor>()
				.Include<FormGroupArraySettingsParameters, FormGroupArraySettingsDescriptor>()
				.Include<FormGroupSettingsParameters, FormGroupSettingsDescriptor>()
				.Include<FormGroupBoxSettingsParameters, FormGroupBoxSettingsDescriptor>()
				.Include<MultiSelectFormControlSettingsParameters, MultiSelectFormControlSettingsDescriptor>();

            CreateMap<DetailItemSettingsParameters, DetailItemSettingsDescriptor>()
				.Include<DetailControlSettingsParameters, DetailControlSettingsDescriptor>()
				.Include<DetailGroupArraySettingsParameters, DetailGroupArraySettingsDescriptor>()
				.Include<DetailGroupBoxSettingsParameters, DetailGroupBoxSettingsDescriptor>()
				.Include<DetailGroupSettingsParameters, DetailGroupSettingsDescriptor>()
				.Include<MultiSelectDetailControlSettingsParameters, MultiSelectDetailControlSettingsDescriptor>();

            CreateMap<SearchFilterParametersBase, SearchFilterDescriptorBase>()
				.Include<SearchFilterGroupParameters, SearchFilterGroupDescriptor>()
				.Include<SearchFilterParameters, SearchFilterDescriptor>();

            CreateMap<LabelItemParametersBase, LabelItemDescriptorBase>()
				.Include<FormattedLabelItemParameters, FormattedLabelItemDescriptor>()
				.Include<HyperLinkLabelItemParameters, HyperLinkLabelItemDescriptor>()
				.Include<LabelItemParameters, LabelItemDescriptor>();

            CreateMap<SpanItemParametersBase, SpanItemDescriptorBase>()
				.Include<HyperLinkSpanItemParameters, HyperLinkSpanItemDescriptor>()
				.Include<SpanItemParameters, SpanItemDescriptor>();
        }
    }
}
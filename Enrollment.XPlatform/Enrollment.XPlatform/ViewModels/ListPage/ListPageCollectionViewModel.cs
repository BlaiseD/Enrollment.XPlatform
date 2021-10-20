﻿using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Forms.Configuration.ListForm;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Utils;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Enrollment.XPlatform.ViewModels.ListPage
{
    public class ListPageCollectionViewModel<TModel> : ListPageCollectionViewModelBase where TModel : Domain.EntityModelBase
    {
        public ListPageCollectionViewModel(ScreenSettings<ListFormSettingsDescriptor> screenSettings, IHttpService httpService) : base(screenSettings)
        {
            this.httpService = httpService;
            GetItems();
        }

        private readonly IHttpService httpService;

        private ObservableCollection<TModel> _items;
        public ObservableCollection<TModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private Task<BaseResponse> GetList()
            => BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => httpService.GetList
                (
                    new GetTypedListRequest
                    {
                        Selector = this.FormSettings.FieldsSelector,
                        ModelType = this.FormSettings.RequestDetails.ModelType,
                        DataType = this.FormSettings.RequestDetails.DataType,
                        ModelReturnType = this.FormSettings.RequestDetails.ModelReturnType,
                        DataReturnType = this.FormSettings.RequestDetails.DataReturnType
                    }
                )
            );

        private async void GetItems()
        {
            BaseResponse baseResponse = await GetList();

            if (baseResponse.Success == false)
                return;

            GetListResponse getListResponse = (GetListResponse)baseResponse;
            this.Items = new ObservableCollection<TModel>(getListResponse.List.Cast<TModel>());
        }
    }
}

﻿using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Domain;
using Enrollment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment.XPlatform.Services
{
    public class HttpServiceMock : IHttpService
    {
        public Task<BaseResponse> GetObjectDropDown(GetTypedListRequest request, string url = null)
        {
            return Task.FromResult<BaseResponse>(new GetListResponse { Success = true, List = new List<EntityModelBase> { } });
        }

        public Task<BaseResponse> GetList(GetTypedListRequest request, string url = null)
        {
            return Task.FromResult<BaseResponse>(new GetListResponse { Success = true, List = new List<EntityModelBase> { } });
        }

        public Task<BaseResponse> GetEntity(GetEntityRequest request, string url = null)
        {
            return Task.FromResult<BaseResponse>(new GetEntityResponse { Success = true });
        }

        public Task<BaseResponse> SaveEntity(SaveEntityRequest request, string url = null)
        {
            return Task.FromResult<BaseResponse>(new SaveEntityResponse { Success = true });
        }

        public Task<BaseResponse> DeleteEntity(DeleteEntityRequest request, string url = null)
        {
            return Task.FromResult<BaseResponse>(new DeleteEntityResponse { Success = true });
        }
    }
}

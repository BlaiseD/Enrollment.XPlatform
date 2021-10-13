﻿using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Enrollment.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Entity")]
    public class EntityController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public EntityController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("GetEntity")]
        public async Task<BaseResponse> GetList([FromBody] GetEntityRequest request)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Entity/GetEntity",
                JsonSerializer.Serialize(request),
                this.configurationOptions.BaseBslUrl
            );
    }
}

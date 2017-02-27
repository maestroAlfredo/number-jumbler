using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NumberJumbler.Models;
using System.IO;

namespace NumberJumbler.Controllers
{
    public class MathsController : ApiController
    {
        private INumberJumblerService _numberJumblerLogic;

        public MathsController(INumberJumblerService numberJumblerLogic)
        {
            _numberJumblerLogic = numberJumblerLogic;
        }

        [HttpPost]
        public async Task<NumberJumblerResult> FindLeader(bool validate=false)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            if (!(provider.Contents.Count > 1))
            {
                using (var stream = await provider.Contents[0].ReadAsStreamAsync())
                {
                    return validate ? _numberJumblerLogic.FindLeader(stream) : _numberJumblerLogic.FindLeaderNoValidation(stream);
                }

            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    RequestMessage = Request,
                    Content = new StringContent("<h2>Multiple files not supported</h2>")
                });
            }
        }
    }

    //public class FindLeaderActionResult : IHttpActionResult
    //{
    //    public FindLeaderActionResult()
    //    {

    //    }
    //    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    //    {
    //        HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.)
    //    }
    //}
}

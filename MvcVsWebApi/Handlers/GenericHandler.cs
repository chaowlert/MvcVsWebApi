using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MvcVsWebApi.Handlers
{
	public class GenericHandler : DelegatingHandler
	{
		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request.Content.Headers.ContentType == null)
				request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
			var queryMethod = request.RequestUri.ParseQueryString()["method"];
			if (!string.IsNullOrEmpty(queryMethod))
				request.Method = new HttpMethod(queryMethod);

			var response = await base.SendAsync(request, cancellationToken);

			response.Headers.Add("X-Written-By", "Chaowlert");
			return response;
		}
	}
}
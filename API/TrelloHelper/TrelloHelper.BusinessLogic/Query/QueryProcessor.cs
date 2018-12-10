using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Intent;
using BusinessLogic.Intent.Models;
using BusinessLogic.Query;
using BusinessLogic.Query.Models;
using Infrastructure.LUIS;
using Infrastructure.LUIS.Models;

namespace TrelloHelper.BusinessLogic.Query
{
	public class QueryProcessor : IQueryProcessor
	{
		private readonly ILUISClient _luisClient;
		private readonly IIntentExecutor _intentExecutor;

		public QueryProcessor(
			ILUISClient luisClient,
			IIntentExecutor intentExecutor)
		{
			_luisClient = luisClient;
			_intentExecutor = intentExecutor;
		}

		public async Task<Response> Process(Request request)
		{
			request = request ?? throw new ArgumentNullException(nameof(request));

			var luisResponse = await GetLUISResponse(request).ConfigureAwait(false);

			// TODO: add mapping
			var data = new IntentData()
			{
				Name = luisResponse?.TopScoringIntent?.Name,
				Entities = luisResponse.Entities.Select(e => new IntentEntity()
				{
					Name = e.Name
				}).ToList()
			};
			var intentResult = _intentExecutor.Execute(data).ConfigureAwait(false);
			
			// TODO: add mapping
			return new Response();
		}

		public Task<LUISResponse> GetLUISResponse(Request request)
		{
			// TODO: add mapping
			var luisRequest = new LUISRequest()
			{
				Query = request.Query
			};

			return _luisClient.GetResponse(luisRequest);
		}
	}
}

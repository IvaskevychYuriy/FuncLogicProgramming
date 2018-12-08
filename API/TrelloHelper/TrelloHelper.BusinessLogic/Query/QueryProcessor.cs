using System;
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
		private readonly IIntentHandlersContext _intentsContext;

		public QueryProcessor(
			ILUISClient luisClient,
			IIntentHandlersContext intentsContext)
		{
			_luisClient = luisClient;
			_intentsContext = intentsContext;
		}

		public async Task<Response> Process(Request request)
		{
			request = request ?? throw new ArgumentNullException(nameof(request));

			var luisResponse = await GetLUISResponse(request).ConfigureAwait(false);

			// TODO: add mapping
			var intent = new IntentData()
			{
				Name = luisResponse?.TopScoringIntent?.Name
			};
			var intentResult = _intentsContext.Execute(intent).ConfigureAwait(false);
			
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

using System;
using System.Threading.Tasks;
using AutoMapper;
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
		private readonly IMapper _mapper;

		public QueryProcessor(
			ILUISClient luisClient,
			IIntentExecutor intentExecutor,
			IMapper mapper)
		{
			_luisClient = luisClient;
			_intentExecutor = intentExecutor;
			_mapper = mapper;
		}

		public async Task<Response> Process(Request request)
		{
			request = request ?? throw new ArgumentNullException(nameof(request));

			var luisRequest = _mapper.Map<LUISRequest>(request);
			var luisResponse = await _luisClient.GetResponse(luisRequest).ConfigureAwait(false);
			
			var data = _mapper.Map<IntentData>(luisResponse);
			var intentResult = await _intentExecutor.Execute(data).ConfigureAwait(false);

			return _mapper.Map<Response>(intentResult);
		}
	}
}

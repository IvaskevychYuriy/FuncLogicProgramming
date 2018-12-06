using BusinessLogic.Query;
using BusinessLogic.Query.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TrelloHelper.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QueryController : ControllerBase
    {
		private readonly IQueryProcessor _queryProcessor;

		public QueryController(IQueryProcessor queryProcessor)
		{
			_queryProcessor = queryProcessor;
		}

		// POST api/query/performAction
		[HttpPost("performAction")]
		public async Task<ActionResult<Response>> PerformAction(Request model)
		{
			var result = await _queryProcessor.Process(model);
			return result;
		}
	}
}

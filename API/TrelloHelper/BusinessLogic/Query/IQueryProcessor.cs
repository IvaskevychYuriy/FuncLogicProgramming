using BusinessLogic.Query.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Query
{
	public interface IQueryProcessor
    {
		Task<Response> Process(Request request);
    }
}

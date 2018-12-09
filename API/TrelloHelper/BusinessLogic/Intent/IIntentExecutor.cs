using BusinessLogic.Intent.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Intent
{
	public interface IIntentExecutor
    {
		Task<IntentResult> Execute(IntentData intent);
    }
}

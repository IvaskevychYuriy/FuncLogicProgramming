using BusinessLogic.Intent.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Intent
{
	public interface IIntentHandlersContext
    {
		Task<IntentResult> Execute(IntentData intent);
    }
}

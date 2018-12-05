using BusinessLogic.Intent.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Intent
{
	public interface IIntentHandler
    {
		bool CanHandle(IntentData intent);

		Task<IntentResult> Handle(IntentData intent);
    }
}

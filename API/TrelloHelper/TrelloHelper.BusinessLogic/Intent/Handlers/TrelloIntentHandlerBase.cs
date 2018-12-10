using BusinessLogic.Intent;
using BusinessLogic.Intent.Models;
using Infrastructure.Trello;
using System;
using System.Threading.Tasks;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public abstract class TrelloIntentHandlerBase<TIntent> : IIntentHandler
		where TIntent : IntentBase, new()
	{
		protected readonly ITrelloClient _trelloClient;
		
		public TrelloIntentHandlerBase(ITrelloClient trelloClient)
		{
			_trelloClient = trelloClient;
		}

		protected abstract string IntentName { get; }

		public bool CanHandle(IntentData intent)
		{
			return intent?.Name == IntentName;
		}

		public Task<IntentResult> Handle(IntentData data)
		{
			if (!CanHandle(data))
			{
				throw new ArgumentException($"Cannot handle requested intent: '{data?.Name}'");
			}
			
			var intent = CreateIntentFromData(data);
			return HandleInternal(intent);
		}

		protected abstract Task<IntentResult> HandleInternal(TIntent intent);
		
		protected virtual TIntent CreateIntentFromData(IntentData data)
		{
			var intent = new TIntent();
			intent.Data = data;
			return intent;
		}

		protected virtual void ValidateName(string value, string message)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(message);
			}
		}
	}
}

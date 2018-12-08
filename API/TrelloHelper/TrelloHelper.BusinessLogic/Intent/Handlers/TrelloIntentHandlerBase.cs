using BusinessLogic.Intent;
using BusinessLogic.Intent.Models;
using Infrastructure.Trello;
using System;
using System.Threading.Tasks;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public abstract class TrelloIntentHandlerBase : IIntentHandler
	{
		protected readonly ITrelloClient _trelloClient;
		
		public TrelloIntentHandlerBase(ITrelloClient trelloClient)
		{
			_trelloClient = trelloClient;
		}

		protected abstract string IntentName { get; }

		public bool CanHandle(IntentData intent)
		{
			return !string.IsNullOrEmpty(intent?.Name) && intent.Name == IntentName;
		}

		public Task<IntentResult> Handle(IntentData intent)
		{
			if (!CanHandle(intent))
			{
				throw new ArgumentException($"Cannot handle requested intent: '{intent?.Name}'");
			}

			return HandleInternal(intent);
		}

		protected abstract Task<IntentResult> HandleInternal(IntentData intent);
	}
}

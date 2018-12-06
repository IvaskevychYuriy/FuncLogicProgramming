using BusinessLogic.Intent;
using BusinessLogic.Intent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloHelper.BusinessLogic.Intent
{
	public class IntentHandlersContext : IIntentHandlersContext
	{
		private readonly IEnumerable<IIntentHandler> _handlers;

		public IntentHandlersContext(IEnumerable<IIntentHandler> handlers)
		{
			_handlers = handlers;
		}

		public Task<IntentResult> Execute(IntentData intent)
		{
			var handler = _handlers.FirstOrDefault(h => h.CanHandle(intent))
				?? throw new ArgumentException($"Cannot find a handler for intent: '{intent?.Name}'");

			return handler.Handle(intent);
		}
	}
}

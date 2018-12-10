using BusinessLogic.Intent;
using BusinessLogic.Intent.Models;
using System;
using System.Threading.Tasks;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public abstract class TrelloIntentHandlerBase<TIntent> : IIntentHandler
		where TIntent : IntentBase
	{
		protected readonly IntentHandlerAggregateService _aggregateService;
		
		public TrelloIntentHandlerBase(IntentHandlerAggregateService aggregateService)
		{
			_aggregateService = aggregateService;
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

			var intent = _aggregateService.Mapper.Map<TIntent>(data);
			return HandleInternal(intent);
		}

		protected abstract Task<IntentResult> HandleInternal(TIntent intent);

		protected virtual void ValidateName(string value, string message)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(message);
			}
		}
	}
}

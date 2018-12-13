using BusinessLogic.Intent;
using BusinessLogic.Intent.Models;
using Infrastructure.Trello.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public abstract class TrelloIntentHandlerBase<TIntent> : IIntentHandler
		where TIntent : IntentBase, new()
	{
		protected readonly IntentHandlerAggregateService _aggregateService;

		protected TrelloIntentHandlerBase(IntentHandlerAggregateService aggregateService)
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

			var intent = new TIntent();
			intent.Data = data;
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

        protected async Task<Card> FindTaskByName(List list, string taskName)
        {
            var tasks = await _aggregateService.TrelloClient.GetCardsForList(list);
            var task = tasks.FirstOrDefault(x => x.Name.Equals(taskName, StringComparison.InvariantCultureIgnoreCase));

            if (task == null)
            {
                throw new ArgumentException($"Task \"{taskName}\" does not exist");
            }
            return task;
        }

        protected async Task<List> FindListByName(Board board, string listName)
        {
            var lists = await _aggregateService.TrelloClient.GetListsForBoard(board);
            var list = lists.FirstOrDefault(x => x.Name.Equals(listName, StringComparison.InvariantCultureIgnoreCase));

            if (list == null)
            {
                throw new ArgumentException($"List \"{listName}\" does not exist");
            }
            return list;
        }

        protected Task<List> FindListByName(string boardId, string listName)
        {
            var board = new Board
            {
                Id = boardId
            };
            return FindListByName(board, listName);
        }
	}
}

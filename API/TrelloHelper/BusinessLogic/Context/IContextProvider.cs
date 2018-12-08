using BusinessLogic.Context.Models;

namespace BusinessLogic.Context
{
	public interface IContextProvider
    {
		ContextCacheEntry Get(ContextCacheKeyWrapper key);

		void AddOrUpdate(ContextCacheKeyWrapper key, ContextCacheEntry value);

		bool Exists(ContextCacheKeyWrapper key);
	}
}

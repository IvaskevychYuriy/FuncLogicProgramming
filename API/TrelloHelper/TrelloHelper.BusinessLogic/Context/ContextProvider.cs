using BusinessLogic.Context;
using BusinessLogic.Context.Models;
using Common.Configurations.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;

namespace TrelloHelper.BusinessLogic.Context
{
	public class ContextProvider : IContextProvider
	{
		private readonly ContextEntryOptions _entryOptions;
		private readonly IMemoryCache _cache;

		public ContextProvider(
			IOptions<ContextEntryOptions> entryOptions,
			IMemoryCache cache)
		{
			_entryOptions = entryOptions.Value;
			_cache = cache;
		}

		public ContextCacheEntry Get(ContextCacheKeyWrapper key)
		{
			key = key ?? throw new ArgumentNullException(nameof(key));

			ContextCacheEntry entry = null;
			ExistsAndGet(key, out entry);
			return entry;
		}

		public void AddOrUpdate(ContextCacheKeyWrapper key, ContextCacheEntry value)
		{
			key = key ?? throw new ArgumentNullException(nameof(key));
			value = value ?? throw new ArgumentNullException(nameof(value));

			var options = GetEntryOptions();
			_cache.Set(key.GetKey(), value, options);
		}

		public bool Exists(ContextCacheKeyWrapper key)
		{
			key = key ?? throw new ArgumentNullException(nameof(key));

			return ExistsAndGet(key, out ContextCacheEntry _);
		}

		private bool ExistsAndGet(ContextCacheKeyWrapper key, out ContextCacheEntry value)
		{
			return _cache.TryGetValue(key.GetKey(), out value);
		}

		private MemoryCacheEntryOptions GetEntryOptions() => new MemoryCacheEntryOptions()
		{
			Size = _entryOptions.Size,
			SlidingExpiration = TimeSpan.FromMinutes(_entryOptions.ExpirationMinutes)
		};
	}
}

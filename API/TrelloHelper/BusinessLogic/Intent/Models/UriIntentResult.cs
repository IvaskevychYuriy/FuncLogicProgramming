using System;

namespace BusinessLogic.Intent.Models
{
    public class UriIntentResult : IntentResult
    {
        public UriIntentResult(Uri uri)
        {
            Uri = uri;
        }

        public UriIntentResult(string uri) : this(new Uri(uri))
        {
        }

        public Uri Uri { get; }
    }
}

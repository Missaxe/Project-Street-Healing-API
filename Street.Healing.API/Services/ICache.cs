namespace Street.Healing.API.Services
{
    public interface ICache<TKey, TValue>
    {
        public void Store(TKey key, TValue value, TimeSpan expiresAfter);
        public TValue Get(TKey key);
    }
}

using StackExchange.Redis;

namespace Repositories.Redis
{

    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}
namespace todoserver
{
    using System.Collections.Generic;
    using ServiceStack.Redis;
    using ServiceStack.Redis.Generic;

    public class TodoService
    {
        private PooledRedisClientManager _redisManager;
        private IRedisClient _redis;
        private IRedisTypedClient<Todo> _redisTodos;

        public TodoService()
        {
            #if DEBUG
            this._redisManager = new PooledRedisClientManager("localhost");
            #else
            this._redisManager = new PooledRedisClientManager("4lEL8GyQRI9grcsO1fm9URhsNBRxRD8pbHeBuvIeN7Q=@todo.redis.cache.windows.net:6379");
            #endif

            this._redis = this._redisManager.GetClient();
            this._redisTodos = _redis.As<Todo>();
        }

        public IList<Todo> GetTodos() {
            return this._redisTodos.GetAll();
        }

        public Todo AddTodo(Todo todo) {
            todo.Id = this._redisTodos.GetNextSequence();

            return this._redisTodos.Store(todo);
        }

        public Todo ToggleTodo(int id) {
            Todo todo = this._redisTodos.GetById(id);
            todo.Done = !todo.Done;

            return this._redisTodos.Store(todo);
        }
    }
}

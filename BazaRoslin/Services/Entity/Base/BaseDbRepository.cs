using System;
using System.Reflection;
using System.Threading.Tasks;

namespace BazaRoslin.Services.Entity.Base {
    public abstract class BaseDbRepository<TContext> where TContext : BaseDbContext {
        protected TContext Context {
            get {
                MethodInfo? meth = null;
                var type = typeof(TContext);
                while (meth == null && type != null) {
                    meth = type.GetMethod("Current", BindingFlags.Static | BindingFlags.NonPublic,
                        Type.DefaultBinder, Type.EmptyTypes, null);
                    if (meth != null) break;
                    type = type.BaseType;
                }
                return (TContext)meth!.Invoke(null, null);
            }
        }

        protected Task<T> UseContext<T>(Func<TContext, T> func) => Task.Run(() => func(Context));
        protected Task UseContext(Action<TContext> action) => Task.Run(() => action(Context));
    }
}
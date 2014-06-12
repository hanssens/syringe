using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Syringe
{
    /// <summary>
    /// Initializes a new instance of the SyringeContainer.
    /// </summary>
    /// <remarks>
    /// Original source and inspiration by Rob Fonseca-Ensor: http://stackoverflow.com/a/2598839
    /// </remarks>
    public class SyringeContainer
    {
        protected readonly Dictionary<string, Func<object>> services = new Dictionary<string, Func<object>>();
        protected readonly Dictionary<Type, string> serviceNames = new Dictionary<Type, string>();

        public DependencyManager Register<S, C>() where C : S
        {
            return Register<S, C>(Guid.NewGuid().ToString());
        }

        public DependencyManager Register<S, C>(string name) where C : S
        {
            if (!serviceNames.ContainsKey(typeof(S)))
            {
                serviceNames[typeof(S)] = name;
            }
            return new DependencyManager(this, name, typeof(C));
        }

        public T Resolve<T>(string name) where T : class
        {
            return (T)services[name]();
        }

        public T Resolve<T>() where T : class
        {
            return Resolve<T>(serviceNames[typeof(T)]);
        }

        public class DependencyManager
        {
            private readonly SyringeContainer container;
            private readonly Dictionary<string, Func<object>> args;
            private readonly string name;

            internal DependencyManager(SyringeContainer container, string name, Type type)
            {
                this.container = container;
                this.name = name;

                ConstructorInfo c = type.GetConstructors().First();
                args = c.GetParameters()
                    .ToDictionary<ParameterInfo, string, Func<object>>(
                    x => x.Name,
                    x => (() => container.services[container.serviceNames[x.ParameterType]]())
                    );

                container.services[name] = () => c.Invoke(args.Values.Select(x => x()).ToArray());
            }

            public DependencyManager AsSingleton()
            {
                object value = null;
                Func<object> service = container.services[name];
                container.services[name] = () => value ?? (value = service());
                return this;
            }

            public DependencyManager WithDependency(string parameter, string component)
            {
                args[parameter] = () => container.services[component]();
                return this;
            }

            public DependencyManager WithValue(string parameter, object value)
            {
                args[parameter] = () => value;
                return this;
            }
        }
    }
}

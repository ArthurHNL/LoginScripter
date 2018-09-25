using System;
using System.Collections.Generic;
using System.Linq;

namespace LoginScripter.Lib
{
    public abstract class ScriptContainer
    {
        private static readonly List<ILoginScript> _scripts;
        public static IReadOnlyList<ILoginScript> Scripts => _scripts;

        static ScriptContainer()
        {
            Type type = typeof(ILoginScript);
            IEnumerable<Type> scriptTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p =>
                type.IsAssignableFrom(p)
                && p.Name != "ILoginScript"
                && !(p.IsInterface)
                && !(p.IsAbstract));
            _scripts = new List<ILoginScript>();
            foreach (Type st in scriptTypes)
            {
                _scripts.Add((ILoginScript)Activator.CreateInstance(st));
            }
        }

    }
}

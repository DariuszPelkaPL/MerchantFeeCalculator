using System;
using System.IO;
using System.Reflection;

namespace Common.SimpleDependencyInjector
{
    public class DependencyInjector
    {
        /// <summary>
        /// Creates an instance of a class implementing a specific interface in a given assembly.
        /// </summary>
        /// <param name="interfaceType">The type of the interface to get. For interfaces.</param>
        /// <returns>Reference to the created instance if found; otherwise, null.</returns>
        public static object CreateInstance(Type interfaceType)
        {
            // ensure interface name is not null
            // check if assembly file exists
            Assembly asm = interfaceType.Assembly;

            // Next we'll loop through all the Types found in the assembly
            foreach (Type pluginType in asm.GetTypes())
            {
                // Only look at public types
                if (pluginType.IsPublic)
                {
                    // Only look at non-abstract types
                    if (!pluginType.IsAbstract)
                    {
                        // Make sure the interface we want to use actually exists
                        return Activator.CreateInstance(asm.GetType(pluginType.ToString()));
                    }
                }
            }

            return null;
        }
    }
}

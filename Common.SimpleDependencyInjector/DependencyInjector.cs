﻿using System;
using System.Reflection;

namespace DankseBank.Common
{
    using System.Collections.Generic;

    public class DependencyInjector
    {
        private static IDictionary<Type, Type> assignments = new Dictionary<Type, Type>();

        public static void Assign(Type interfaceType, Type implementationClass)
        {
            assignments.Add(interfaceType, implementationClass);
        }

        /// <summary>
        /// Creates an instance of a class implementing a specific interface in a given assembly.
        /// </summary>
        /// <param name="interfaceType">The type of the interface to get. For interfaces.</param>
        /// <returns>Reference to the created instance if found; otherwise, null.</returns>
        public static object CreateInstance(Type interfaceType)
        {
            var classType = assignments[interfaceType];
            Assembly asm = classType.Assembly;
            return Activator.CreateInstance(asm.GetType(classType.ToString()));
        }
    }
}

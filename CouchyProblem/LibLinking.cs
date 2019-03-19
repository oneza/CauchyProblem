using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CouchyProblem
{
    public class LibLinking<T>
    {
        public T FindLib(string file)
        {
            Type[] assembly = Assembly.LoadFrom(file).GetTypes();
            int i;
            for (i = 0; i < assembly.Length; i++)
            {
                Type intType = assembly[i].GetInterface(typeof(T).FullName);
                if (intType != null) break;
            }

            return (T) Activator.CreateInstance(assembly[i]);
        }
    }
}
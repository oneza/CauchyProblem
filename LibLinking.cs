using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CouchyProblem
{
    public class LibLinking<T>
    {
        public List<T> FindLib(string path)
        {
            List<T> res = new List<T>();
            foreach (string file in Directory.GetFiles(path, "*.dll"))
            {
                foreach (Type assemblyType in Assembly.LoadFrom(file).GetTypes())
                {
                    Type intType = assemblyType.GetInterface(typeof(T).FullName);

                    if (intType != null)
                    {
                        res.Add((T) Activator.CreateInstance(assemblyType));
                    }
                }
            }
            return res;
        }
    }
}
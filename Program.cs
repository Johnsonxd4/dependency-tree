using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace dependency_tree
{
    class Program
    {
        static void Main(string[] args)
        {
            var assemblyName = args[0];
            var desiredClass = args[1];
            var assemblyPatterns = args[2];
            var basePath = Path.GetDirectoryName(assemblyName);
            
            var assembly =  Assembly.LoadFrom(assemblyName);
            var allAssemblies = GetRemainingAssemblies(basePath, assemblyPatterns);
            var type = assembly.ExportedTypes.FirstOrDefault(x => x.Name == desiredClass);
            if(type == null){
                Console.WriteLine("Class not found");
            }
            else{
                Console.WriteLine("found");
                Console.WriteLine(type.FullName);
            }
           var dependency = new Dependency(type,allAssemblies);
          dependency.Draw(1);
        }


        public static List<Assembly> GetRemainingAssemblies(string path,string assemblyPatterns){
            var files = Directory.GetFiles(path,assemblyPatterns);
            var assemblies = new List<Assembly>();
            foreach (var item in files)
            {
                Console.WriteLine(item);
                assemblies.Add(Assembly.LoadFrom(item));
            }
            return assemblies;
        }
    }
}
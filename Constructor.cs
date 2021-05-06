using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace dependency_tree {
    public class Constructor {
        private ConstructorInfo _info ;
        public IList<Dependency> Dependencies;
        
        public Constructor(ConstructorInfo info, List<Assembly> assemblies)
        {
            Dependencies = new List<Dependency>();
            _info = info;
            GetDependencies(assemblies);
        }


        public void GetDependencies(List<Assembly> assemblies){
            Dependencies = _info
            .GetParameters()
            .Select(x => new Dependency(x.ParameterType, assemblies))
            .ToList();
        }

        public void Draw(int increment){
            increment = increment ++;
            foreach (var item in Dependencies)
            {
                item.Draw(increment);
                Console.WriteLine("--------------------------------");
            }
        }
    }
}
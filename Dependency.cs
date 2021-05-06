using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace dependency_tree {
    public class Dependency {
        public Dependency(Type type, List<Assembly> assemblies) {
            DependencyType = type;
            Implementations = new List<Dependency>();
            Constructors = new List<Constructor>();
            fillImplementations(assemblies);
            fillConstructors(assemblies);
        }

        private void fillConstructors(List<Assembly> assemblies){
            Constructors = DependencyType
            .GetConstructors()
            .Select(x => new Constructor(x,assemblies))
            .ToList();
        }

        private void fillImplementations (List<Assembly> assemblies) {
            assemblies
                .ForEach(assembly => {
                        assembly
                        .GetTypes()
                        .ToList()
                        .Where(className => 
                            className.GetInterfaces()
                            .Contains(DependencyType) && className.IsClass && !className.Namespace.Contains("System")
                        )
                        .ToList()
                        .ForEach(x => Implementations.Add(new Dependency(x, assemblies)));
                });
        }

        public IList<Constructor> Constructors;
        public Type DependencyType;
        public IList<Dependency> Implementations;

        public void Draw(int increment){
            increment = increment++;
            foreach (var item in Implementations)
            {
                Console.WriteLine($"{string.Concat(Enumerable.Repeat("\t", increment))}{DependencyType} -> {item.DependencyType}");
                item.Draw(increment);
            }
            foreach (var item in Constructors)
            {
                item.Draw(increment);
            }
        }
    }
}
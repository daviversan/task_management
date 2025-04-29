using System;
using System.Collections.Generic;

namespace Task_Management.Models
{
    public class Professional
    {
        public string Name { get; set; } = string.Empty;
        public List<Project> Projects { get; set; } = new();
        public List<Task> Tasks { get; set; } = new();

        // Construtor padrão
        public Professional()
        {
        }

        public Professional(string name)
        {
            Name = name;
        }

        public void ListTasks()
        {
            Console.WriteLine($"Tarefas atribuídas a {Name}:");

            foreach (var task in Tasks)
            {
                Console.WriteLine($"- {task.Title} (Status: {task.Status})");
            }
        }

        public static Professional CreateProfessional(string name, List<Project> projects, List<Task> tasks)
        {
            var professional = new Professional(name)
            {
                Projects = projects,
                Tasks = tasks
            };
            return professional;
        }

        public void DeleteProfessional()
        {
            // Em um sistema real, isso envolveria remoção de uma lista ou do banco
            Console.WriteLine($"Profissional '{Name}' deletado.");
        }
        
    }
}
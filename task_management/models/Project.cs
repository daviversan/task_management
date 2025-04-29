using System;
using System.Collections.Generic;

namespace Task_Management.Models
{
    public class Project
    {
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public List<Task> Tasks { get; set; } = new();
        public List<Professional> AssignedProfessionals { get; set; } = new();

        // Construtor padrão
        public Project()
        {
        }

        // Construtor com parâmetros
        public Project(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public void RemoveTask(Task task)
        {
            Tasks.Remove(task);
        }

        public void AssignProfessional(Professional professional)
        {
            if (!AssignedProfessionals.Contains(professional))
            {
                AssignedProfessionals.Add(professional);
                professional.Projects.Add(this);
            }
        }

        public void DismissProfessional(Professional professional)
        {
            if (AssignedProfessionals.Remove(professional))
            {
                professional.Projects.Remove(this);
            }
        }

        public static Project CreateProject(string name, string description)
        {
            Console.WriteLine($"Projeto '{name}' criado.");
            return new Project(name, description);
        }

        public void DeleteProject()
        {
            // Em um sistema real, isso envolveria remoção de uma lista ou do banco
            Console.WriteLine($"Projeto '{name}' deletado.");
        }
    }
}
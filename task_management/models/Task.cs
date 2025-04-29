using System;
using System.Collections.Generic;

namespace Task_Management.Models
{
    public class Task
    {
        // Atributos conforme o diagrama
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.Pendente;
        public TaskPriority Priority { get; set; } = TaskPriority.Media;

        // Muitos profissionais podem estar atribuídos à tarefa
        public List<Professional> AssignedProfessionals { get; set; } = new();

        // Construtor padrão
        public Task()
        {
        }

        // Métodos representados no diagrama
        public static Task CreateTask(string title, string description, DateTime? dueDate, TaskPriority priority)
        {
            return new Task
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                Priority = priority
            };
        }

        public void DeleteTask()
        {
            // Em um sistema real, isso envolveria remoção de uma lista ou do banco
            Console.WriteLine($"Tarefa '{Title}' deletada.");
        }

        public void AssignProfessional(Professional professional)
        {
            if (!AssignedProfessionals.Contains(professional))
            {
                AssignedProfessionals.Add(professional);
                Console.WriteLine($"Profissional '{professional.Name}' atribuído à tarefa '{Title}'.");
            }
        }

        public void DismissProfessional(Professional professional)
        {
            if (AssignedProfessionals.Contains(professional))
            {
                AssignedProfessionals.Remove(professional);
                Console.WriteLine($"Profissional '{professional.Name}' removido da tarefa '{Title}'.");
            }
        }

        public void EditTask(string newTitle, string newDescription, DateTime? newDueDate, TaskPriority newPriority)
        {
            Title = newTitle;
            Description = newDescription;
            DueDate = newDueDate;
            Priority = newPriority;
            Console.WriteLine($"Tarefa '{Title}' atualizada.");
        }
    }
}
namespace Task_Management.Models
{
    public class Professional
    {
        // Propriedade Name iniciada como string vazia
        public string Name { get; set; } = string.Empty;

        // Lista de objetos do tipo Project e Task
        // inicializadas como listas vazias
        public List<Project> Projects { get; set; } = new();
        public List<Task> Tasks { get; set; } = new();

        // Construtor padrão
        // Cria um objeto Professional sem parâmetros
        public Professional()
        {
        }

        // Permite criar um profissional já atribuindo o nome
        public Professional(string name)
        {
            Name = name;
        }

        // Imprime no console todas as tasks atribuídas ao profissional
        public void ListTasks()
        {
            Console.WriteLine($"Tarefas atribuídas a {Name}:");

            // Percorre cada task na lista de tasks
            foreach (var task in Tasks)
            {
                Console.WriteLine($"- {task.Title} (Status: {task.Status})");
            }
        }

        // Método para criar um profissional com nome, projetos e tarefas
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
namespace Task_Management.Models
{
    public class Project
    {

        // Inicialização das propriedades da classe Project
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

        // Método para adicionar uma task ao projeto
        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        // Método para remover uma task do projeto
        public void RemoveTask(Task task)
        {
            Tasks.Remove(task);
        }

        // Método para atribuir um profissional ao projeto
        public void AssignProfessional(Professional professional)
        {
            // Se não houver profissional atribuído ao projeto
            if (!AssignedProfessionals.Contains(professional))
            {
                // Adiciona o profissional à lista de profissionais atribuídos
                AssignedProfessionals.Add(professional);
                // Adiciona o projeto à lista de projetos do profissional
                professional.Projects.Add(this);
            }
        }

        // Método para remover um profissional do projeto
        public void DismissProfessional(Professional professional)
        {
            if (AssignedProfessionals.Remove(professional))
            {
                professional.Projects.Remove(this);
            }
        }

        // Méotodo para criar um projeto com nome e descrição
        public static Project CreateProject(string name, string description)
        {
            Console.WriteLine($"Projeto '{name}' criado.");
            return new Project(name, description);
        }

        // Método para deletar um projeto
        public void DeleteProject()
        {
            // Em um sistema real, isso envolveria remoção de uma lista ou do banco
            Console.WriteLine($"Projeto '{name}' deletado.");
        }
    }
}
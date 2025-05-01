using Task_Management.Models;

// Variáveis para armazenar instâncias das entidades
// Podem conter valores nulos
Professional? professional = null;
Project? project = null;
Task_Management.Models.Task? task = null;

// Loop principal do sistema
// Mantém o programa em execução enquanto o usuário não escolher sair 
bool exit = false;

while (!exit)
{

    // Menu de opções
    Console.Clear();
    Console.WriteLine("=== Sistema de Gerenciamento de Tarefas ===");

    // Exibe status atual
    DisplayCurrentStatus();

    Console.WriteLine("\nEscolha uma opção:");
    Console.WriteLine("1. Criar profissional");
    Console.WriteLine("2. Criar projeto");
    Console.WriteLine("3. Criar tarefa");
    Console.WriteLine("4. Associar profissional à tarefa");
    Console.WriteLine("5. Adicionar tarefa ao projeto");
    Console.WriteLine("6. Adicionar profissional ao projeto");
    Console.WriteLine("7. Deletar entidades");
    Console.WriteLine("0. Sair");

    Console.Write("\nOpção: ");
#pragma warning disable CS8600 
    string option = Console.ReadLine();
#pragma warning restore CS8600 // Converte valores nulos em tipos não nulos

    switch (option)
    {
        case "1":
            CreateProfessional();
            break;
        case "2":
            CreateProject();
            break;
        case "3":
            CreateTask();
            break;
        case "4":
            AssignProfessionalToTask();
            break;
        case "5":
            AddTaskToProject();
            break;
        case "6":
            AddProfessionalToProject();
            break;
        case "7":
            DeleteEntities();
            break;
        case "0":
            exit = true;
            break;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }

    if (!exit)
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}

void DisplayCurrentStatus()
{
    Console.WriteLine("\nStatus atual:");
    Console.WriteLine($"- Profissional: {(professional != null ? professional.Name : "Nenhum")}");
    Console.WriteLine($"- Projeto: {(project != null ? project.name : "Nenhum")}");
    Console.WriteLine($"- Tarefa: {(task != null ? task.Title : "Nenhuma")}");

    if (professional != null && task != null)
    {
        bool isAssigned = task.AssignedProfessionals.Contains(professional);
        Console.WriteLine($"- Profissional associado à tarefa: {(isAssigned ? "Sim" : "Não")}");
    }

    if (professional != null && project != null)
    {
        bool isAssigned = project.AssignedProfessionals.Contains(professional);
        Console.WriteLine($"- Profissional associado ao projeto: {(isAssigned ? "Sim" : "Não")}");
    }

    if (project != null && task != null)
    {
        bool isAdded = project.Tasks.Contains(task);
        Console.WriteLine($"- Tarefa adicionada ao projeto: {(isAdded ? "Sim" : "Não")}");
    }
}

void CreateProfessional()
{
    Console.Clear();
    Console.WriteLine("=== Criar Profissional ===");

    Console.Write("Nome do profissional: ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string name = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8604 // Possible null reference argument.
    professional = new Professional(name);
#pragma warning restore CS8604 // Possible null reference argument.
    Console.WriteLine($"\nProfissional '{professional.Name}' criado com sucesso!");
}

void CreateProject()
{
    Console.Clear();
    Console.WriteLine("=== Criar Projeto ===");

    Console.Write("Nome do projeto: ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string name = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    Console.Write("Descrição do projeto: ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string description = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8604 // Possible null reference argument.
    project = Project.CreateProject(name, description);
#pragma warning restore CS8604 // Possible null reference argument.
    Console.WriteLine($"\nProjeto '{project.name}' criado com sucesso!");
}

void CreateTask()
{
    Console.Clear();
    Console.WriteLine("=== Criar Tarefa ===");

    Console.Write("Título da tarefa: ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string title = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    Console.Write("Descrição da tarefa: ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string description = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    Console.Write("Data de prazo (dd/mm/aaaa ou deixe em branco): ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string dateInput = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    DateTime? deadline = null;
    if (!string.IsNullOrWhiteSpace(dateInput))
    {
        if (DateTime.TryParse(dateInput, out DateTime parsedDate))
        {
            deadline = parsedDate;
        }
        else
        {
            Console.WriteLine("Formato de data inválido. Continuando sem data de prazo.");
        }
    }

    Console.WriteLine("\nPrioridade da tarefa:");
    Console.WriteLine("1. Baixa");
    Console.WriteLine("2. Média");
    Console.WriteLine("3. Alta");
    Console.Write("Escolha: ");

    TaskPriority priority = TaskPriority.Media;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string priorityChoice = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    switch (priorityChoice)
    {
        case "1":
            priority = TaskPriority.Baixa;
            break;
        case "2":
            priority = TaskPriority.Media;
            break;
        case "3":
            priority = TaskPriority.Alta;
            break;
        default:
            Console.WriteLine("Opção inválida! Usando prioridade Média como padrão.");
            break;
    }

#pragma warning disable CS8604 // Possible null reference argument.
    task = Task_Management.Models.Task.CreateTask(title, description, deadline, priority);
#pragma warning restore CS8604 // Possible null reference argument.
    Console.WriteLine($"\nTarefa '{task.Title}' criada com sucesso!");
}

void AssignProfessionalToTask()
{
    Console.Clear();
    Console.WriteLine("=== Associar Profissional à Tarefa ===");

    if (professional == null)
    {
        Console.WriteLine("Erro: Nenhum profissional criado ainda!");
        return;
    }

    if (task == null)
    {
        Console.WriteLine("Erro: Nenhuma tarefa criada ainda!");
        return;
    }

    task.AssignProfessional(professional);
    professional.Tasks.Add(task);

    Console.WriteLine($"Profissional '{professional.Name}' associado à tarefa '{task.Title}' com sucesso!");
}

void AddTaskToProject()
{
    Console.Clear();
    Console.WriteLine("=== Adicionar Tarefa ao Projeto ===");

    if (project == null)
    {
        Console.WriteLine("Erro: Nenhum projeto criado ainda!");
        return;
    }

    if (task == null)
    {
        Console.WriteLine("Erro: Nenhuma tarefa criada ainda!");
        return;
    }

    project.AddTask(task);

    Console.WriteLine($"Tarefa '{task.Title}' adicionada ao projeto '{project.name}' com sucesso!");
}

void AddProfessionalToProject()
{
    Console.Clear();
    Console.WriteLine("=== Adicionar Profissional ao Projeto ===");

    if (project == null)
    {
        Console.WriteLine("Erro: Nenhum projeto criado ainda!");
        return;
    }

    if (professional == null)
    {
        Console.WriteLine("Erro: Nenhum profissional criado ainda!");
        return;
    }

    project.AssignProfessional(professional);

    Console.WriteLine($"Profissional '{professional.Name}' adicionado ao projeto '{project.name}' com sucesso!");
}

void DeleteEntities()
{
    Console.Clear();
    Console.WriteLine("=== Deletar Entidades ===");
    Console.WriteLine("O que você deseja deletar?");
    Console.WriteLine("1. Profissional");
    Console.WriteLine("2. Projeto");
    Console.WriteLine("3. Tarefa");
    Console.WriteLine("4. Tudo");
    Console.WriteLine("0. Voltar");

    Console.Write("\nOpção: ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string option = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    switch (option)
    {
        case "1":
            if (professional != null)
            {
                professional.DeleteProfessional();
                professional = null;
                Console.WriteLine("Profissional deletado com sucesso!");
            }
            else
            {
                Console.WriteLine("Não há profissional para deletar.");
            }
            break;
        case "2":
            if (project != null)
            {
                project.DeleteProject();
                project = null;
                Console.WriteLine("Projeto deletado com sucesso!");
            }
            else
            {
                Console.WriteLine("Não há projeto para deletar.");
            }
            break;
        case "3":
            if (task != null)
            {
                task.DeleteTask();
                task = null;
                Console.WriteLine("Tarefa deletada com sucesso!");
            }
            else
            {
                Console.WriteLine("Não há tarefa para deletar.");
            }
            break;
        case "4":
            if (professional != null)
            {
                professional.DeleteProfessional();
                professional = null;
            }
            if (project != null)
            {
                project.DeleteProject();
                project = null;
            }
            if (task != null)
            {
                task.DeleteTask();
                task = null;
            }
            Console.WriteLine("Todas as entidades foram deletadas com sucesso!");
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
}

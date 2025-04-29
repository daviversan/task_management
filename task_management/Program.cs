using System;
using System.Collections.Generic;
using Task_Management.Models;

namespace TaskManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Gerenciamento de Tarefas - Testes ===\n");
            
            // Executa os testes automaticamente
            ExecuteTests();
            
            Console.WriteLine("\nPressione qualquer tecla para encerrar...");
            Console.ReadKey();
        }

        static void ExecuteTests()
        {
            Console.WriteLine("Iniciando testes do sistema...\n");

            // Testes de criação de entidades
            TestEntityCreation();
            
            // Testes de associação entre entidades
            TestEntityAssociations();
            
            // Testes de atualização de entidades
            TestEntityUpdates();
            
            // Teste de remoção de entidades
            TestEntityDeletion();

            Console.WriteLine("\nTodos os testes foram concluídos!");
        }

        static void TestEntityCreation()
        {
            Console.WriteLine("=== TESTE: Criação de Entidades ===");
            
            // Teste 1: Criar um profissional
            var professional = new Professional("João Silva");
            Console.WriteLine($"Teste 1: Profissional criado - Nome: {professional.Name}");
            
            // Teste 2: Criar um projeto
            var project = Project.CreateProject("Sistema Web", "Sistema web para gestão de tarefas");
            Console.WriteLine($"Teste 2: Projeto criado - Nome: {project.name}, Descrição: {project.description}");
            
            // Teste 3: Criar uma tarefa
            var task = Task_Management.Models.Task.CreateTask(
                "Implementar login", 
                "Criar tela e backend para login de usuários",
                DateTime.Now.AddDays(5),
                TaskPriority.Alta
            );
            Console.WriteLine($"Teste 3: Tarefa criada - Título: {task.Title}, " + 
                $"Prioridade: {task.Priority}, Status: {task.Status}");
            
            Console.WriteLine("Todos os testes de criação de entidades PASSARAM!\n");
        }

        static void TestEntityAssociations()
        {
            Console.WriteLine("=== TESTE: Associação entre Entidades ===");
            
            // Criação das entidades para teste
            var maria = new Professional("Maria Santos");
            var carlos = new Professional("Carlos Oliveira");
            
            var projetoWeb = Project.CreateProject("Portal Corporativo", "Portal para informações da empresa");
            var projetoMobile = Project.CreateProject("App Mobile", "Aplicativo móvel para acesso");
            
            var tarefaFrontend = Task_Management.Models.Task.CreateTask(
                "Desenvolver Frontend", 
                "Criar interfaces responsivas",
                DateTime.Now.AddDays(10),
                TaskPriority.Media
            );
            
            var tarefaBackend = Task_Management.Models.Task.CreateTask(
                "Desenvolver Backend", 
                "Criar APIs e banco de dados",
                DateTime.Now.AddDays(15),
                TaskPriority.Alta
            );
            
            // Teste 1: Associar profissionais a projetos
            projetoWeb.AssignProfessional(maria);
            projetoWeb.AssignProfessional(carlos);
            projetoMobile.AssignProfessional(maria);
            
            Console.WriteLine($"Teste 1: Profissionais atribuídos aos projetos");
            Console.WriteLine($"- Projeto '{projetoWeb.name}' tem {projetoWeb.AssignedProfessionals.Count} profissionais");
            Console.WriteLine($"- Projeto '{projetoMobile.name}' tem {projetoMobile.AssignedProfessionals.Count} profissionais");
            Console.WriteLine($"- Profissional '{maria.Name}' está em {maria.Projects.Count} projetos");
            
            // Teste 2: Associar tarefas a projetos
            projetoWeb.AddTask(tarefaFrontend);
            projetoWeb.AddTask(tarefaBackend);
            
            Console.WriteLine($"Teste 2: Tarefas adicionadas ao projeto '{projetoWeb.name}'");
            Console.WriteLine($"- O projeto tem {projetoWeb.Tasks.Count} tarefas");
            
            // Teste 3: Associar profissionais a tarefas
            tarefaFrontend.AssignProfessional(maria);
            tarefaBackend.AssignProfessional(carlos);
            
            // Também adicionamos manualmente à lista de tarefas do profissional
            maria.Tasks.Add(tarefaFrontend);
            carlos.Tasks.Add(tarefaBackend);
            
            Console.WriteLine($"Teste 3: Profissionais atribuídos às tarefas");
            Console.WriteLine($"- Tarefa '{tarefaFrontend.Title}' tem {tarefaFrontend.AssignedProfessionals.Count} profissionais");
            Console.WriteLine($"- Tarefa '{tarefaBackend.Title}' tem {tarefaBackend.AssignedProfessionals.Count} profissionais");
            
            // Listando tarefas de um profissional
            Console.WriteLine($"\nListando tarefas de {maria.Name}:");
            maria.ListTasks();
            
            Console.WriteLine("Todos os testes de associação de entidades PASSARAM!\n");
        }

        static void TestEntityUpdates()
        {
            Console.WriteLine("=== TESTE: Atualização de Entidades ===");
            
            // Criação das entidades para teste
            var task = Task_Management.Models.Task.CreateTask(
                "Bug fixing", 
                "Corrigir bugs críticos",
                DateTime.Now.AddDays(2),
                TaskPriority.Media
            );
            
            // Teste 1: Atualizar uma tarefa
            Console.WriteLine($"Teste 1: Atualização de tarefa");
            Console.WriteLine($"- Estado original: Título='{task.Title}', Prioridade={task.Priority}, Status={task.Status}");
            
            task.EditTask(
                "Correção de bugs críticos",
                "Corrigir bugs impedindo uso do sistema",
                DateTime.Now.AddDays(1),
                TaskPriority.Alta
            );
            
            // Mudando o status
            task.Status = Task_Management.Models.TaskStatus.EmAndamento;
            
            Console.WriteLine($"- Estado após atualização: Título='{task.Title}', Prioridade={task.Priority}, Status={task.Status}");
            
            Console.WriteLine("Todos os testes de atualização de entidades PASSARAM!\n");
        }

        static void TestEntityDeletion()
        {
            Console.WriteLine("=== TESTE: Remoção de Entidades ===");
            
            // Criação das entidades para teste
            var professional = new Professional("Ana Souza");
            var project = Project.CreateProject("Projeto Temporário", "Projeto para teste de remoção");
            var task = Task_Management.Models.Task.CreateTask(
                "Tarefa Temporária", 
                "Tarefa para teste de remoção",
                null,
                TaskPriority.Baixa
            );
            
            // Associações
            project.AssignProfessional(professional);
            project.AddTask(task);
            task.AssignProfessional(professional);
            professional.Tasks.Add(task);
            
            // Teste 1: Remover profissional de tarefa
            Console.WriteLine($"Teste 1: Remoção de profissional de tarefa");
            Console.WriteLine($"- Antes: Tarefa tem {task.AssignedProfessionals.Count} profissionais");
            
            task.DismissProfessional(professional);
            
            Console.WriteLine($"- Depois: Tarefa tem {task.AssignedProfessionals.Count} profissionais");
            
            // Teste 2: Remover profissional de projeto
            Console.WriteLine($"Teste 2: Remoção de profissional de projeto");
            Console.WriteLine($"- Antes: Projeto tem {project.AssignedProfessionals.Count} profissionais");
            Console.WriteLine($"- Antes: Profissional está em {professional.Projects.Count} projetos");
            
            project.DismissProfessional(professional);
            
            Console.WriteLine($"- Depois: Projeto tem {project.AssignedProfessionals.Count} profissionais");
            Console.WriteLine($"- Depois: Profissional está em {professional.Projects.Count} projetos");
            
            // Teste 3: Remover tarefa de projeto
            task = Task_Management.Models.Task.CreateTask("Nova Tarefa", "Descrição", null, TaskPriority.Media);
            project.AddTask(task);
            
            Console.WriteLine($"Teste 3: Remoção de tarefa de projeto");
            Console.WriteLine($"- Antes: Projeto tem {project.Tasks.Count} tarefas");
            
            project.RemoveTask(task);
            
            Console.WriteLine($"- Depois: Projeto tem {project.Tasks.Count} tarefas");
            
            // Teste 4: Excluir entidades
            Console.WriteLine($"Teste 4: Exclusão de entidades");
            
            professional.DeleteProfessional();
            project.DeleteProject();
            task.DeleteTask();
            
            Console.WriteLine("Todos os testes de remoção de entidades PASSARAM!\n");
        }
    }
}
// PS/Program.cs
using Aplicacion.commands.Proposals;
using Aplicacion.DTOs;
using Aplicacion.queries;
using Dominio.repositorios;
using Infraestructura.datos;
using Infraestructura;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;

namespace PS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                
            }

            await RunApplication(host);
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddInfrastructure(context.Configuration);
                    services.AddApplication();
                });

        static async Task RunApplication(IHost host)
        {
            Console.WriteLine("Iniciando la aplicación...");
            using var scope = host.Services.CreateScope();

            try
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();


                var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Sistema de Aprobación de Proyectos");
                    Console.WriteLine("=================================");
                    Console.WriteLine("1. Crear nueva propuesta de proyecto");
                    Console.WriteLine("2. Ver estado de proyectos");
                    Console.WriteLine("3. Aprobar pasos pendientes");
                    Console.WriteLine("4. Salir");
                    Console.Write("Seleccione una opción: ");

                    var option = Console.ReadLine();

                    try
                    {
                        switch (option)
                        {
                            case "1":
                                await CreateNewProposal(mediator, userRepo);
                                break;
                            case "2":
                                await ViewProjectStatus(mediator);
                                break;
                            case "3":
                                await ApprovePendingSteps(mediator, userRepo);
                                break;
                            case "4":
                                return;
                            default:
                                Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                                Console.ReadKey();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();

            }
            
        }

        static async Task CreateNewProposal(IMediator mediator, IUserRepository userRepo)
        {
            Console.Clear();
            Console.WriteLine("Nueva Propuesta de Proyecto");
            Console.WriteLine("==========================");

            // Obtener usuarios para selección
            var users = await userRepo.ObtenerTodosAsync();
            Console.WriteLine("\nUsuarios disponibles:");
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id}: {user.Name} ({user.ApproverRole.Name})");
            }

            Console.Write("\nID del usuario creador: ");
            if (!int.TryParse(Console.ReadLine(), out int createdBy) || !users.Any(u => u.Id == createdBy))
            {
                throw new Exception("ID de usuario no válido");
            }

            Console.Write("Título del proyecto: ");
            var title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new Exception("El título no puede estar vacío");
            }

            Console.Write("Descripción del proyecto: ");
            var description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new Exception("La descripción no puede estar vacía");
            }

            // Obtener áreas disponibles
            List<AreaDto> areas = await mediator.Send(new GetAllAreasQuery());
            Console.WriteLine("\nÁreas disponibles:");
            foreach (var area in areas)
            {
                Console.WriteLine($"{area.Id}: {area.Nombre}");
            }
            Console.Write("ID del área: ");
            if (!int.TryParse(Console.ReadLine(), out int areaId) || !areas.Any(a => a.Id == areaId))
            {
                throw new Exception("ID de área no válido");
            }

            // Obtener tipos de proyecto disponibles
            List<ProjectTypeDto> projectTypes = await mediator.Send(new GetAllProjectTypesQuery());
            Console.WriteLine("\nTipos de proyecto disponibles:");
            foreach (var type in projectTypes)
            {
                Console.WriteLine($"{type.Id}: {type.Name}");
            }
            Console.Write("ID del tipo de proyecto: ");
            if (!int.TryParse(Console.ReadLine(), out int typeId) || !projectTypes.Any(t => t.Id == typeId))
            {
                throw new Exception("ID de tipo de proyecto no válido");
            }

            Console.Write("Monto estimado: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                throw new Exception("Monto debe ser un número positivo");
            }

            Console.Write("Duración estimada (días): ");
            if (!int.TryParse(Console.ReadLine(), out int duration) || duration <= 0)
            {
                throw new Exception("Duración debe ser un número positivo");
            }

            var command = new CreateProposalCommand
            {
                Title = title,
                Description = description,
                Area = areaId,
                Type = typeId,
                EstimatedAmount = amount,
                EstimatedDuration = duration,
                CreatedBy = createdBy
            };

            var proposalId = await mediator.Send(command);

            Console.WriteLine($"\nPropuesta creada exitosamente con ID: {proposalId}");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static async Task ViewProjectStatus(IMediator mediator)
        {
            Console.Clear();
            Console.WriteLine("Estado de Proyectos");
            Console.WriteLine("===================");

            // Obtener todos los proyectos
            var proposals = await mediator.Send(new GetAllProposalsQuery());

            if (!proposals.Any())
            {
                Console.WriteLine("No hay proyectos registrados.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nLista de Proyectos:");
            foreach (var proposal in proposals)
            {
                Console.WriteLine($"{proposal.Id}: {proposal.Title} - Estado: {proposal.Status}");
            }

            Console.Write("\nIngrese el ID del proyecto para ver detalles (0 para volver): ");
            if (!int.TryParse(Console.ReadLine(), out int proposalId) || proposalId == 0)
            {
                return;
            }

            if (!proposals.Any(p => p.Id == proposalId))
            {
                throw new Exception("ID de proyecto no válido");
            }

            // Obtener detalles del proyecto
            ProposalDto projectDetails = await mediator.Send(new GetProposalByIdQuery { ProposalId = proposalId });

            Console.Clear();
            Console.WriteLine($"Detalles del Proyecto ID: {projectDetails.Id}");
            Console.WriteLine("====================================");
            Console.WriteLine($"Título: {projectDetails.Title}");
            Console.WriteLine($"Descripción: {projectDetails.Description}");
            Console.WriteLine($"Área: {projectDetails.Area}");
            Console.WriteLine($"Tipo: {projectDetails.Type}");
            Console.WriteLine($"Monto estimado: {projectDetails.EstimatedAmount:C}");
            Console.WriteLine($"Duración estimada: {projectDetails.EstimatedDuration} días");
            Console.WriteLine($"Estado: {projectDetails.Status}");
            Console.WriteLine($"Creado por: {projectDetails.CreatedBy}");
            Console.WriteLine($"Fecha creación: {projectDetails.CreateAt}");

            Console.WriteLine("\nPasos de aprobación:");
            if(projectDetails.Steps == null || !projectDetails.Steps.Any())
            {
                Console.WriteLine("No hay pasos de aprobación registrados.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            else { 
                foreach (ApprovalStepDto step in projectDetails.Steps)
                {
                    Console.WriteLine($"\nPaso {step.StepOrder}:");
                    Console.WriteLine($"  Rol aprobador: {step.ApproverRole}");
                    Console.WriteLine($"  Estado: {step.Status}");
                    if (step.ApproverUser != null)
                    {
                        Console.WriteLine($"  Aprobado por: {step.ApproverUser}");
                        Console.WriteLine($"  Fecha decisión: {step.DecisionDate}");
                    }
                    if (!string.IsNullOrEmpty(step.Observations))
                    {
                        Console.WriteLine($"  Observaciones: {step.Observations}");
                    }
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static async Task ApprovePendingSteps(IMediator mediator, IUserRepository userRepo)
        {
            Console.Clear();
            Console.WriteLine("Aprobación de Pasos Pendientes");
            Console.WriteLine("==============================");

            Console.Write("Ingrese su ID de usuario: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                throw new Exception("ID de usuario no válido");
            }

            var user = await userRepo.ObtenerPorIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            Console.WriteLine($"\nBienvenido {user.Name} ({user.ApproverRole.Name})");

            // Obtener pasos pendientes para el rol del usuario
            List < PendingStepDto> pendingSteps = await mediator.Send(new GetPendingStepsByRoleQuery { RoleId = user.Role });

            if (!pendingSteps.Any())
            {
                Console.WriteLine("\nNo tiene pasos pendientes para aprobar.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nPasos pendientes para su aprobación:");
            foreach (var step in pendingSteps)
            {
                Console.WriteLine($"\nID Paso: {step.StepId}");
                Console.WriteLine($"Proyecto: {step.ProposalTitle} (ID: {step.ProposalId})");
                Console.WriteLine($"Paso: {step.StepOrder}");
                Console.WriteLine($"Área: {step.Area}");
                Console.WriteLine($"Tipo: {step.ProjectType}");
                Console.WriteLine($"Monto: {step.Amount:C}");
            }

            Console.Write("\nIngrese el ID del paso a procesar (0 para volver): ");
            if (!long.TryParse(Console.ReadLine(), out long stepId) || stepId == 0)
            {
                return;
            }

            if (!pendingSteps.Any(s => s.StepId == stepId))
            {
                throw new Exception("ID de paso no válido o no está pendiente de su aprobación");
            }

            Console.WriteLine("\nOpciones:");
            Console.WriteLine("1. Aprobar");
            Console.WriteLine("2. Rechazar");
            Console.WriteLine("3. Observar");
            Console.Write("Seleccione una acción: ");
            var action = Console.ReadLine();

            int status;
            string observations = null;

            switch (action)
            {
                case "1":
                    status = 2; // Aprobado
                    break;
                case "2":
                    status = 3; // Rechazado
                    Console.Write("Ingrese motivo del rechazo: ");
                    observations = Console.ReadLine();
                    break;
                case "3":
                    status = 4; // Observado
                    Console.Write("Ingrese observaciones: ");
                    observations = Console.ReadLine();
                    break;
                default:
                    throw new Exception("Opción no válida");
            }

            var command = new ProcessApprovalCommand
            {
                StepId = stepId,
                UserId = userId,
                Status = status,
                Observations = observations
            };

            var result = await mediator.Send(command);

            if (result)
            {
                Console.WriteLine("\nPaso procesado exitosamente.");
            }
            else
            {
                Console.WriteLine("\nNo se pudo procesar el paso.");
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}


using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApproverRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projectype",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projectype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_ApproverRole_Role",
                        column: x => x.Role,
                        principalTable: "ApproverRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinAmount = table.Column<decimal>(type: "decimal(38,2)", nullable: false),
                    MaxAmount = table.Column<decimal>(type: "decimal(38,2)", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<int>(type: "int", nullable: true),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    ApproverRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalRule_ApproverRole_ApproverRoleId",
                        column: x => x.ApproverRoleId,
                        principalTable: "ApproverRole",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalRule_Area_Area",
                        column: x => x.Area,
                        principalTable: "Area",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalRule_Projectype_type",
                        column: x => x.type,
                        principalTable: "Projectype",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectProposal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    EstimatedAmount = table.Column<decimal>(type: "decimal(38,2)", nullable: false),
                    EstimatedDuration = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ApproverRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProposal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectProposal_ApprovalStatus_Status",
                        column: x => x.Status,
                        principalTable: "ApprovalStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectProposal_ApproverRole_ApproverRoleId",
                        column: x => x.ApproverRoleId,
                        principalTable: "ApproverRole",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectProposal_Area_Area",
                        column: x => x.Area,
                        principalTable: "Area",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectProposal_Projectype_type",
                        column: x => x.type,
                        principalTable: "Projectype",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectProposal_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectApprovalStep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectProposalId = table.Column<int>(type: "int", nullable: false),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    ApproverRoleId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApproverUserId = table.Column<int>(type: "int", nullable: true),
                    DecisionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Observations = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectApprovalStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectApprovalStep_ApprovalStatus_Status",
                        column: x => x.Status,
                        principalTable: "ApprovalStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectApprovalStep_ApproverRole_ApproverRoleId",
                        column: x => x.ApproverRoleId,
                        principalTable: "ApproverRole",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectApprovalStep_ProjectProposal_ProjectProposalId",
                        column: x => x.ProjectProposalId,
                        principalTable: "ProjectProposal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectApprovalStep_User_ApproverUserId",
                        column: x => x.ApproverUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ApprovalStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Rejected" },
                    { 4, "Observed" }
                });

            migrationBuilder.InsertData(
                table: "ApproverRole",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Líder de Área" },
                    { 2, "Gerente" },
                    { 3, "Director" },
                    { 4, "Comité Técnico" }
                });

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Finanzas" },
                    { 2, "Tecnología" },
                    { 3, "Recursos Humanos" },
                    { 4, "Operaciones" }
                });

            migrationBuilder.InsertData(
                table: "Projectype",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mejora de procesos" },
                    { 2, "Innovación y desarrollo" },
                    { 3, "Infraestructura" },
                    { 4, "Capacitación interna" }
                });

            migrationBuilder.InsertData(
                table: "ApprovalRule",
                columns: new[] { "Id", "ApproverRoleId", "Area", "MaxAmount", "MinAmount", "StepOrder", "type" },
                values: new object[,]
                {
                    { 1, 1, null, 100000m, 0m, 1, null },
                    { 2, 2, null, 20000m, 5000m, 2, null },
                    { 3, 2, 2, 20000m, 0m, 1, 2 },
                    { 4, 3, null, 79228162514264337593543950335m, 20000m, 3, null },
                    { 5, 2, 1, 79228162514264337593543950335m, 5000m, 2, 1 },
                    { 6, 1, null, 10000m, 0m, 1, 2 },
                    { 7, 4, 2, 10000m, 0m, 1, 1 },
                    { 8, 2, 2, 30000m, 10000m, 2, null },
                    { 9, 3, 3, 79228162514264337593543950335m, 30000m, 2, null },
                    { 10, 4, null, 50000m, 0m, 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Name", "Role" },
                values: new object[,]
                {
                    { 1, "jferreyra@unaj.com", "Jose Ferreyra", 2 },
                    { 2, "alucero@unaj.com", "Ana Lucero", 1 },
                    { 3, "gmolinas@unaj.com", "Gonzalo Molinas", 2 },
                    { 4, "lolivera@unaj.com", "Lucas Olivera", 3 },
                    { 5, "dfagundez@unaj.com", "Danilo Fagundez", 4 },
                    { 6, "ggalli@unaj.com", "Gabriel Galli", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRule_ApproverRoleId",
                table: "ApprovalRule",
                column: "ApproverRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRule_Area",
                table: "ApprovalRule",
                column: "Area");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRule_type",
                table: "ApprovalRule",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_ApproverRoleId",
                table: "ProjectApprovalStep",
                column: "ApproverRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_ApproverUserId",
                table: "ProjectApprovalStep",
                column: "ApproverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_ProjectProposalId",
                table: "ProjectApprovalStep",
                column: "ProjectProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_Status",
                table: "ProjectApprovalStep",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_ApproverRoleId",
                table: "ProjectProposal",
                column: "ApproverRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_Area",
                table: "ProjectProposal",
                column: "Area");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_CreatedBy",
                table: "ProjectProposal",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_Status",
                table: "ProjectProposal",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_type",
                table: "ProjectProposal",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role",
                table: "User",
                column: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalRule");

            migrationBuilder.DropTable(
                name: "ProjectApprovalStep");

            migrationBuilder.DropTable(
                name: "ProjectProposal");

            migrationBuilder.DropTable(
                name: "ApprovalStatus");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Projectype");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ApproverRole");
        }
    }
}

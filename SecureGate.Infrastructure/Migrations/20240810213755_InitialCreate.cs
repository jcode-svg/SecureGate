using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SecureGate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BioData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BioData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    AccessLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    AccessType = table.Column<int>(type: "INTEGER", nullable: false),
                    AccessLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    OfficeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doors_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    RegistrationApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: true),
                    BioDataId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_BioData_BioDataId",
                        column: x => x.BioDataId,
                        principalTable: "BioData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessRule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DoorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRule_Doors_DoorId",
                        column: x => x.DoorId,
                        principalTable: "Doors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessRule_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DoorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccessGranted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventLogs_Doors_DoorId",
                        column: x => x.DoorId,
                        principalTable: "Doors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BioData",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "FirstName", "LastName" },
                values: new object[] { new Guid("c5b37ff9-9d69-4b2f-980a-f146ea9cc4c1"), new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2591), new DateTime(1990, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adam", "Smith" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AccessLevel", "CreatedAt", "Name" },
                values: new object[,]
                {
                { new Guid("800e5e11-4937-4c23-ad8f-39e67120a86b"), 2, new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2380), "Office Manager" },
                { new Guid("8d9ba907-ab15-4a47-ba59-7964656b885e"), 2, new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2365), "Director" },
                { new Guid("be3189ee-8bf0-432d-bfaf-a0db0cc8e61d"), 1, new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2383), "Regular Employee" },
                { new Guid("ffc8fc55-c46e-447a-856e-7b9231c0aba7"), 3, new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2339), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BioDataId", "CreatedAt", "PasswordHash", "RegistrationApproved", "RoleId", "Username" },
                values: new object[] { new Guid("1088469d-5fb6-401e-9dac-f31a68097489"), new Guid("c5b37ff9-9d69-4b2f-980a-f146ea9cc4c1"), new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2630), "ba8ffafd98cd9d08d78c753d8c0ed0bbe0aab3fc3640a8c3ffe5459151578ac2101d5382424e1a6e584da2f116d84590b31a1e4ded82c453f370958d695ccbb8", true, new Guid("ffc8fc55-c46e-447a-856e-7b9231c0aba7"), "admin@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRule_DoorId",
                table: "AccessRule",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRule_EmployeeId",
                table: "AccessRule",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Doors_OfficeId",
                table: "Doors",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BioDataId",
                table: "Employees",
                column: "BioDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventLogs_DoorId",
                table: "EventLogs",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLogs_EmployeeId",
                table: "EventLogs",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRule");

            migrationBuilder.DropTable(
                name: "EventLogs");

            migrationBuilder.DropTable(
                name: "Doors");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "BioData");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

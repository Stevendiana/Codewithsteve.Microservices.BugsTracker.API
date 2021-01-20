using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codewithsteve.Microservices.BugsTrackerAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    UserCreatedId = table.Column<string>(nullable: true),
                    UserCreatedEmail = table.Column<string>(nullable: true),
                    UserCreatedFullName = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    UserModifiedId = table.Column<string>(nullable: true),
                    UserModifiedEmail = table.Column<string>(nullable: true),
                    UserModifiedFullName = table.Column<string>(nullable: true),
                    ClientCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    BugId = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    UserCreatedId = table.Column<string>(nullable: true),
                    UserCreatedEmail = table.Column<string>(nullable: true),
                    UserCreatedFullName = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    UserModifiedId = table.Column<string>(nullable: true),
                    UserModifiedEmail = table.Column<string>(nullable: true),
                    UserModifiedFullName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Status = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: false),
                    ProjectName = table.Column<string>(nullable: true),
                    ResolveByDate = table.Column<DateTime>(nullable: false),
                    ClientName = table.Column<string>(nullable: true),
                    BugCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Priority = table.Column<string>(nullable: false),
                    Impact = table.Column<string>(nullable: false),
                    Severity = table.Column<int>(nullable: false),
                    severitylevel = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    ResolvedBy = table.Column<string>(nullable: true),
                    SignedoffBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.BugId);
                    table.ForeignKey(
                        name: "FK_Bugs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ClientId",
                table: "Bugs",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}

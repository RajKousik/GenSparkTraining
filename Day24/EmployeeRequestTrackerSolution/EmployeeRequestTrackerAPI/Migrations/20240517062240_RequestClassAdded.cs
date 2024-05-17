using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeRequestTrackerAPI.Migrations
{
    public partial class RequestClassAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestRaisedBy = table.Column<int>(type: "int", nullable: false),
                    RequestClosedBy = table.Column<int>(type: "int", nullable: true),
                    RequestRaisedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestClosedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isRequestSolved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Employees_RequestClosedBy",
                        column: x => x.RequestClosedBy,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Employees_RequestRaisedBy",
                        column: x => x.RequestRaisedBy,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestClosedBy",
                table: "Requests",
                column: "RequestClosedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestRaisedBy",
                table: "Requests",
                column: "RequestRaisedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}

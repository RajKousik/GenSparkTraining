using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestTrackerModelLibrary.Migrations
{
    public partial class MinorFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolutionFeedback_Employees_FeedbackBy",
                table: "SolutionFeedback");

            migrationBuilder.DropForeignKey(
                name: "FK_SolutionFeedback_RequestSolutions_SolutionId",
                table: "SolutionFeedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolutionFeedback",
                table: "SolutionFeedback");

            migrationBuilder.RenameTable(
                name: "SolutionFeedback",
                newName: "SolutionFeedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_SolutionFeedback_SolutionId",
                table: "SolutionFeedbacks",
                newName: "IX_SolutionFeedbacks_SolutionId");

            migrationBuilder.RenameIndex(
                name: "IX_SolutionFeedback_FeedbackBy",
                table: "SolutionFeedbacks",
                newName: "IX_SolutionFeedbacks_FeedbackBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolutionFeedbacks",
                table: "SolutionFeedbacks",
                column: "FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolutionFeedbacks_Employees_FeedbackBy",
                table: "SolutionFeedbacks",
                column: "FeedbackBy",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolutionFeedbacks_RequestSolutions_SolutionId",
                table: "SolutionFeedbacks",
                column: "SolutionId",
                principalTable: "RequestSolutions",
                principalColumn: "SolutionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolutionFeedbacks_Employees_FeedbackBy",
                table: "SolutionFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_SolutionFeedbacks_RequestSolutions_SolutionId",
                table: "SolutionFeedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolutionFeedbacks",
                table: "SolutionFeedbacks");

            migrationBuilder.RenameTable(
                name: "SolutionFeedbacks",
                newName: "SolutionFeedback");

            migrationBuilder.RenameIndex(
                name: "IX_SolutionFeedbacks_SolutionId",
                table: "SolutionFeedback",
                newName: "IX_SolutionFeedback_SolutionId");

            migrationBuilder.RenameIndex(
                name: "IX_SolutionFeedbacks_FeedbackBy",
                table: "SolutionFeedback",
                newName: "IX_SolutionFeedback_FeedbackBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolutionFeedback",
                table: "SolutionFeedback",
                column: "FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolutionFeedback_Employees_FeedbackBy",
                table: "SolutionFeedback",
                column: "FeedbackBy",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolutionFeedback_RequestSolutions_SolutionId",
                table: "SolutionFeedback",
                column: "SolutionId",
                principalTable: "RequestSolutions",
                principalColumn: "SolutionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

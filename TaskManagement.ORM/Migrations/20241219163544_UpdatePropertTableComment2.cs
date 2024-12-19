using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.ORM.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertTableComment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Tasks_Id",
                table: "Comentarios");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Comentarios",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_TTaskId",
                table: "Comentarios",
                column: "TTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Tasks_TTaskId",
                table: "Comentarios",
                column: "TTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Tasks_TTaskId",
                table: "Comentarios");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_TTaskId",
                table: "Comentarios");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Comentarios",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Tasks_Id",
                table: "Comentarios",
                column: "Id",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

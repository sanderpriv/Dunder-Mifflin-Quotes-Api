using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dunder.Mifflin.Api.Migrations
{
    /// <inheritdoc />
    public partial class SplitQuotesAndLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Episode",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Scene",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "Season",
                table: "Quotes",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "LineText",
                table: "Quotes",
                newName: "Quote");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Quotes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Season = table.Column<int>(type: "INTEGER", nullable: false),
                    Episode = table.Column<int>(type: "INTEGER", nullable: false),
                    Scene = table.Column<int>(type: "INTEGER", nullable: false),
                    LineText = table.Column<string>(type: "TEXT", nullable: true),
                    Speaker = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    QuoteDbEntityId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lines_Quotes_QuoteDbEntityId",
                        column: x => x.QuoteDbEntityId,
                        principalTable: "Quotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lines_QuoteDbEntityId",
                table: "Lines",
                column: "QuoteDbEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Quotes",
                newName: "Season");

            migrationBuilder.RenameColumn(
                name: "Quote",
                table: "Quotes",
                newName: "LineText");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Quotes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Quotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Episode",
                table: "Quotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Scene",
                table: "Quotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}

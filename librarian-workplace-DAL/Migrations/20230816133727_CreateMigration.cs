using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace librarian_workplace_DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    article_number = table.Column<Guid>(type: "uuid", nullable: false),
                    inset_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    author = table.Column<string>(type: "varchar(100)", nullable: false),
                    publication_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    instances_number = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.article_number);
                });

            migrationBuilder.CreateTable(
                name: "reader",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fio = table.Column<string>(type: "varchar(100)", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp", nullable: false),
                    date_updated = table.Column<DateTime>(type: "timestamp", nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reader", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reader_book",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    reader_id = table.Column<Guid>(type: "UUID", nullable: false),
                    articule_number = table.Column<Guid>(type: "UUID", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reader_book", x => x.id);
                    table.ForeignKey(
                        name: "book_and_reader_book_id_fkey",
                        column: x => x.articule_number,
                        principalTable: "book",
                        principalColumn: "article_number");
                    table.ForeignKey(
                        name: "reader_and_reader_book_id_fkey",
                        column: x => x.reader_id,
                        principalTable: "reader",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_reader_book_articule_number",
                table: "reader_book",
                column: "articule_number");

            migrationBuilder.CreateIndex(
                name: "IX_reader_book_reader_id",
                table: "reader_book",
                column: "reader_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reader_book");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "reader");
        }
    }
}

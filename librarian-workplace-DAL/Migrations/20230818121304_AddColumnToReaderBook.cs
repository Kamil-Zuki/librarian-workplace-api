using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace librarian_workplace_DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnToReaderBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "articule_number",
                table: "reader_book",
                type: "UUID",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "UUID",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBorrowed",
                table: "reader_book",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBorrowed",
                table: "reader_book");

            migrationBuilder.AlterColumn<Guid>(
                name: "articule_number",
                table: "reader_book",
                type: "UUID",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "UUID");
        }
    }
}

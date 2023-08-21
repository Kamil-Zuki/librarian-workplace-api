using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace librarian_workplace_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnDateBorrowed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateBorrowed",
                table: "reader_book",
                newName: "date_borrowed");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_borrowed",
                table: "reader_book",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date_borrowed",
                table: "reader_book",
                newName: "DateBorrowed");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateBorrowed",
                table: "reader_book",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }
    }
}

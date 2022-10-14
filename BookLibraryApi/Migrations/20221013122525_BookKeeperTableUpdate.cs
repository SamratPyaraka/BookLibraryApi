using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibraryApi.Migrations
{
    public partial class BookKeeperTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Expiry",
                table: "BookKeeper",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expiry",
                table: "BookKeeper");
        }
    }
}

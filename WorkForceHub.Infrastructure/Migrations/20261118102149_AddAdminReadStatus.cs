using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkForceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminReadStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AdminReadAt",
                table: "Notifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadByAdmin",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminReadAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsReadByAdmin",
                table: "Notifications");
        }
    }
}

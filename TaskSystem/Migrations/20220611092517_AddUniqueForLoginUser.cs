using Microsoft.EntityFrameworkCore.Migrations;
using TaskSystem.Models;

#nullable disable

namespace TaskSystem.Migrations
{
    public partial class AddUniqueForLoginUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint("User_UN", "User", "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint("User_UN", "User");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetaLottery.Migrations
{
    public partial class BetaLottery11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Myimage",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Myimage",
                table: "Products");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bl.Migrations
{
    /// <inheritdoc />
    public partial class add_TbSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Copyright = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Facebook_Link = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Googol_Link = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Twitter_Link = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Instagram_Link = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    LinkedIn_Link = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSettings", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbSettings");
        }
    }
}

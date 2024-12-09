using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkDev.Talabat.Core.Domain.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnNormalizedNameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "products");
        }
    }
}

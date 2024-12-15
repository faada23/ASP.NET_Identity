using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityPersistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmailToName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Email");
        }
    }
}

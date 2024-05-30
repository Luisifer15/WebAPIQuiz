using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIQuiz.Migrations
{
    /// <inheritdoc />
    public partial class faveColTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFavourite",
                table: "TBL_Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFavourite",
                table: "TBL_Books");
        }
    }
}

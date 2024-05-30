using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIQuiz.Migrations
{
    /// <inheritdoc />
    public partial class faveColTblUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFavourite",
                table: "TBL_Books");

            migrationBuilder.CreateTable(
                name: "TBL_Faves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    bookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Faves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Faves_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TBL_Faves_TBL_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "TBL_Books",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Faves_bookId",
                table: "TBL_Faves",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Faves_UserID",
                table: "TBL_Faves",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_Faves");

            migrationBuilder.AddColumn<bool>(
                name: "isFavourite",
                table: "TBL_Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

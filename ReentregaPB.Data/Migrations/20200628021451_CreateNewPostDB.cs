using Microsoft.EntityFrameworkCore.Migrations;

namespace ReentregaPB.Data.Migrations
{
    public partial class CreateNewPostDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<string>(nullable: true),
                    Texto = table.Column<string>(nullable: true),
                    UrlFoto = table.Column<string>(nullable: true),
                    Poster = table.Column<string>(nullable: true),
                    PostEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Posts_PostEntityId",
                        column: x => x.PostEntityId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostEntityId",
                table: "Posts",
                column: "PostEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}

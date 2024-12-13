using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexagon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_KeyWord_ClassComment_ClassCommentReaction_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportClassId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassComments_SportClasses_SportClassId",
                        column: x => x.SportClassId,
                        principalTable: "SportClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassComments_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KeyWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyWords_SportClasses_ClassId",
                        column: x => x.ClassId,
                        principalTable: "SportClasses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClassCommentReactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportClassId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    ReactionType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCommentReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassCommentReactions_ClassComments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "ClassComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassCommentReactions_SportClasses_SportClassId",
                        column: x => x.SportClassId,
                        principalTable: "SportClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassCommentReactions_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassCommentReactions_CommentId",
                table: "ClassCommentReactions",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassCommentReactions_CreatedBy",
                table: "ClassCommentReactions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ClassCommentReactions_SportClassId",
                table: "ClassCommentReactions",
                column: "SportClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassComments_CreatedBy",
                table: "ClassComments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ClassComments_SportClassId",
                table: "ClassComments",
                column: "SportClassId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyWords_ClassId",
                table: "KeyWords",
                column: "ClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassCommentReactions");

            migrationBuilder.DropTable(
                name: "KeyWords");

            migrationBuilder.DropTable(
                name: "ClassComments");
        }
    }
}

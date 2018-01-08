using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ideal.Web.Data.Migrations
{
    public partial class Newstuff4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Teams_TeamId1",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_TeamId1",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "TeamId1",
                table: "Ideas");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Ideas",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_TeamId",
                table: "Ideas",
                column: "TeamId",
                unique: true,
                filter: "[TeamId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Teams_TeamId",
                table: "Ideas",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Teams_TeamId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_TeamId",
                table: "Ideas");

            migrationBuilder.AlterColumn<string>(
                name: "TeamId",
                table: "Ideas",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId1",
                table: "Ideas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_TeamId1",
                table: "Ideas",
                column: "TeamId1",
                unique: true,
                filter: "[TeamId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Teams_TeamId1",
                table: "Ideas",
                column: "TeamId1",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

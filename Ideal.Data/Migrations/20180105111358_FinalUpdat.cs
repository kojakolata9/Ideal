using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ideal.Web.Data.Migrations
{
    public partial class FinalUpdat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Teams_TeamId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_TeamId",
                table: "Ideas");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Ideas",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_TeamId",
                table: "Ideas",
                column: "TeamId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Teams_TeamId",
                table: "Ideas",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Teams_TeamId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_TeamId",
                table: "Ideas");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Ideas",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}

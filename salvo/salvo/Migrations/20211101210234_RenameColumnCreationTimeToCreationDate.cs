﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace salvo.Migrations
{
    public partial class RenameColumnCreationTimeToCreationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Games",
                newName: "CreationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Games",
                newName: "CreationTime");
        }
    }
}

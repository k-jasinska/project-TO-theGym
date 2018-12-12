using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymSystem.Db.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    street = table.Column<string>(nullable: true),
                    code = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntranceTypeSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Price = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntranceTypeSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Pesel = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    AdressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonSet_AddressSet_AdressId",
                        column: x => x.AdressId,
                        principalTable: "AddressSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntranceSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<int>(nullable: true),
                    EntranceTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntranceSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntranceSet_EntranceTypeSet_EntranceTypeId",
                        column: x => x.EntranceTypeId,
                        principalTable: "EntranceTypeSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntranceSet_PersonSet_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PersonSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntranceLogSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<string>(nullable: true),
                    EntranceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntranceLogSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntranceLogSet_EntranceSet_EntranceId",
                        column: x => x.EntranceId,
                        principalTable: "EntranceSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntranceLogSet_EntranceId",
                table: "EntranceLogSet",
                column: "EntranceId");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceSet_EntranceTypeId",
                table: "EntranceSet",
                column: "EntranceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceSet_PersonId",
                table: "EntranceSet",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSet_AdressId",
                table: "PersonSet",
                column: "AdressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntranceLogSet");

            migrationBuilder.DropTable(
                name: "EntranceSet");

            migrationBuilder.DropTable(
                name: "EntranceTypeSet");

            migrationBuilder.DropTable(
                name: "PersonSet");

            migrationBuilder.DropTable(
                name: "AddressSet");
        }
    }
}

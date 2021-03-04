using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KS.FiksProtokollValidator.WebAPI.Data.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestCases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestName = table.Column<string>(nullable: false),
                    MessageType = table.Column<string>(nullable: false),
                    PayloadFileName = table.Column<string>(nullable: false),
                    PayloadAttachmentFileNames = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    RecipientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FiksResponseTest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayloadQuery = table.Column<string>(nullable: false),
                    ExpectedValue = table.Column<string>(nullable: false),
                    ValueType = table.Column<int>(nullable: false),
                    TestCaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiksResponseTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiksResponseTest_TestCases_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiksRequest",
                columns: table => new
                {
                    MessageGuid = table.Column<Guid>(nullable: false),
                    SentAt = table.Column<DateTime>(nullable: false),
                    TestCaseId = table.Column<int>(nullable: true),
                    TestSessionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiksRequest", x => x.MessageGuid);
                    table.ForeignKey(
                        name: "FK_FiksRequest_TestCases_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FiksRequest_TestSessions_TestSessionId",
                        column: x => x.TestSessionId,
                        principalTable: "TestSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiksResponse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivedAt = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Payload = table.Column<string>(nullable: true),
                    PayloadContent = table.Column<string>(nullable: true),
                    FiksRequestMessageGuid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiksResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiksResponse_FiksRequest_FiksRequestMessageGuid",
                        column: x => x.FiksRequestMessageGuid,
                        principalTable: "FiksRequest",
                        principalColumn: "MessageGuid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiksRequest_TestCaseId",
                table: "FiksRequest",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_FiksRequest_TestSessionId",
                table: "FiksRequest",
                column: "TestSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_FiksResponse_FiksRequestMessageGuid",
                table: "FiksResponse",
                column: "FiksRequestMessageGuid");

            migrationBuilder.CreateIndex(
                name: "IX_FiksResponseTest_TestCaseId",
                table: "FiksResponseTest",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCases_TestName",
                table: "TestCases",
                column: "TestName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiksResponse");

            migrationBuilder.DropTable(
                name: "FiksResponseTest");

            migrationBuilder.DropTable(
                name: "FiksRequest");

            migrationBuilder.DropTable(
                name: "TestCases");

            migrationBuilder.DropTable(
                name: "TestSessions");
        }
    }
}

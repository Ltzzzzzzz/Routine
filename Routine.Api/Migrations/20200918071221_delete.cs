using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routine.Api.Migrations
{
    public partial class delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("1b2a0599-788e-4898-a3b0-33cac2c397ea"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("3e3b794c-b675-4aac-b400-a43ffb9b78f4"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("65182599-9944-4185-a8ad-eed47025cd7a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("b54d6ea3-55df-4708-8b6e-940c985a27d6"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("b5681ff7-50fa-438c-a92e-5f8c49152238"));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[,]
                {
                    { new Guid("eaafdff3-cf19-4158-b225-8574a215b0f0"), new Guid("9b6ff3aa-d91d-4086-baea-5ec3564a31be"), new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT231", "Nick", 1, "Carter" },
                    { new Guid("7d9e080d-e8ce-4e70-9d63-a3cccc93efa1"), new Guid("9b6ff3aa-d91d-4086-baea-5ec3564a31be"), new DateTime(1998, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT001", "Lisa", 2, "Jame" },
                    { new Guid("e0c9f6d6-d36d-4544-95ae-6fb0c38a5f1f"), new Guid("beeba841-f929-4f56-bfef-644bbc0e58d7"), new DateTime(1988, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "G001", "Dick", 1, "Harden" },
                    { new Guid("fb46e09f-6bd7-4a00-a50f-67c20f340cf9"), new Guid("9a2193e3-2c2d-4625-b346-fbc5e789f0ad"), new DateTime(1982, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "A001", "马", 1, "云" },
                    { new Guid("fcecb660-ca39-46fe-8917-d4f032795821"), new Guid("9a2193e3-2c2d-4625-b346-fbc5e789f0ad"), new DateTime(1986, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "A002", "王", 2, "尼玛" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7d9e080d-e8ce-4e70-9d63-a3cccc93efa1"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("e0c9f6d6-d36d-4544-95ae-6fb0c38a5f1f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("eaafdff3-cf19-4158-b225-8574a215b0f0"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("fb46e09f-6bd7-4a00-a50f-67c20f340cf9"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("fcecb660-ca39-46fe-8917-d4f032795821"));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[,]
                {
                    { new Guid("3e3b794c-b675-4aac-b400-a43ffb9b78f4"), new Guid("9b6ff3aa-d91d-4086-baea-5ec3564a31be"), new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT231", "Nick", 1, "Carter" },
                    { new Guid("b54d6ea3-55df-4708-8b6e-940c985a27d6"), new Guid("9b6ff3aa-d91d-4086-baea-5ec3564a31be"), new DateTime(1998, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT001", "Lisa", 2, "Jame" },
                    { new Guid("b5681ff7-50fa-438c-a92e-5f8c49152238"), new Guid("beeba841-f929-4f56-bfef-644bbc0e58d7"), new DateTime(1988, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "G001", "Dick", 1, "Harden" },
                    { new Guid("65182599-9944-4185-a8ad-eed47025cd7a"), new Guid("9a2193e3-2c2d-4625-b346-fbc5e789f0ad"), new DateTime(1982, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "A001", "马", 1, "云" },
                    { new Guid("1b2a0599-788e-4898-a3b0-33cac2c397ea"), new Guid("9a2193e3-2c2d-4625-b346-fbc5e789f0ad"), new DateTime(1986, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "A002", "王", 2, "尼玛" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

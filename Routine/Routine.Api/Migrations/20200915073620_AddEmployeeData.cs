using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routine.Api.Migrations
{
    public partial class AddEmployeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InsertDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Users (Id, UserName, Name, Email, Password, RoleType ) VALUES ('" + Guid.NewGuid().ToString() + "', 'Rein', 'Rein Atian', 'rein.atian@example.com', '$2b$12$RJpcWe9kAUNJV3hHrQ5DBOBARyqjP6YUyA4g0KYJW5v00zB/LK752', 2);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ease.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Roles",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>("TEXT", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Roles", x => x.Id); });

        migrationBuilder.CreateTable(
            "Users",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", maxLength: 255, nullable: true),
                Created = table.Column<DateTime>("TEXT", nullable: false),
                Updated = table.Column<DateTime>("TEXT", nullable: false),
                UserName = table.Column<string>("TEXT", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>("TEXT", maxLength: 256, nullable: true),
                Email = table.Column<string>("TEXT", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>("TEXT", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>("INTEGER", nullable: false),
                PasswordHash = table.Column<string>("TEXT", nullable: true),
                SecurityStamp = table.Column<string>("TEXT", nullable: true),
                ConcurrencyStamp = table.Column<string>("TEXT", nullable: true),
                PhoneNumber = table.Column<string>("TEXT", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>("INTEGER", nullable: false),
                TwoFactorEnabled = table.Column<bool>("INTEGER", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>("TEXT", nullable: true),
                LockoutEnabled = table.Column<bool>("INTEGER", nullable: false),
                AccessFailedCount = table.Column<int>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

        migrationBuilder.CreateTable(
            "RoleClaims",
            table => new
            {
                Id = table.Column<int>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                RoleId = table.Column<Guid>("TEXT", nullable: false),
                ClaimType = table.Column<string>("TEXT", nullable: true),
                ClaimValue = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RoleClaims", x => x.Id);
                table.ForeignKey(
                    "FK_RoleClaims_Roles_RoleId",
                    x => x.RoleId,
                    "Roles",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "UserClaims",
            table => new
            {
                Id = table.Column<int>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<Guid>("TEXT", nullable: false),
                ClaimType = table.Column<string>("TEXT", nullable: true),
                ClaimValue = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserClaims", x => x.Id);
                table.ForeignKey(
                    "FK_UserClaims_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "UserLogins",
            table => new
            {
                LoginProvider = table.Column<string>("TEXT", nullable: false),
                ProviderKey = table.Column<string>("TEXT", nullable: false),
                ProviderDisplayName = table.Column<string>("TEXT", nullable: true),
                UserId = table.Column<Guid>("TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    "FK_UserLogins_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "UserRoles",
            table => new
            {
                UserId = table.Column<Guid>("TEXT", nullable: false),
                RoleId = table.Column<Guid>("TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    "FK_UserRoles_Roles_RoleId",
                    x => x.RoleId,
                    "Roles",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_UserRoles_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "UserTokens",
            table => new
            {
                UserId = table.Column<Guid>("TEXT", nullable: false),
                LoginProvider = table.Column<string>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", nullable: false),
                Value = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    "FK_UserTokens_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_RoleClaims_RoleId",
            "RoleClaims",
            "RoleId");

        migrationBuilder.CreateIndex(
            "RoleNameIndex",
            "Roles",
            "NormalizedName",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_UserClaims_UserId",
            "UserClaims",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_UserLogins_UserId",
            "UserLogins",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_UserRoles_RoleId",
            "UserRoles",
            "RoleId");

        migrationBuilder.CreateIndex(
            "EmailIndex",
            "Users",
            "NormalizedEmail");

        migrationBuilder.CreateIndex(
            "UserNameIndex",
            "Users",
            "NormalizedUserName",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "RoleClaims");

        migrationBuilder.DropTable(
            "UserClaims");

        migrationBuilder.DropTable(
            "UserLogins");

        migrationBuilder.DropTable(
            "UserRoles");

        migrationBuilder.DropTable(
            "UserTokens");

        migrationBuilder.DropTable(
            "Roles");

        migrationBuilder.DropTable(
            "Users");
    }
}

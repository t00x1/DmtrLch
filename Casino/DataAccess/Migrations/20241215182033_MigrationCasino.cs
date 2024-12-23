using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MigrationCasino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cupons",
                columns: table => new
                {
                    ID_of_cupon = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Reusable = table.Column<bool>(type: "bit", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(dateadd(hour,(1),getdate()))"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cupons__B703844AC53B3A36", x => x.ID_of_cupon);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    ID_of_Game = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Type_Of_Game = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Game__F187176F416BA7CC", x => x.ID_of_Game);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id_of_image = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Directory = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Images__9347264A4B05F199", x => x.Id_of_image);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id_Of_Room = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Id_Of_Game = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Name_Of_Room = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Room__8B8A9C144CAB0E88", x => x.Id_Of_Room);
                    table.ForeignKey(
                        name: "FK__Room__Id_Of_Game__6774552F",
                        column: x => x.Id_Of_Game,
                        principalTable: "Game",
                        principalColumn: "ID_of_Game");
                });

            migrationBuilder.CreateTable(
                name: "WonBet",
                columns: table => new
                {
                    ID_of_Game = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    WonWeb = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WonBet__F187176FFF10317D", x => x.ID_of_Game);
                    table.ForeignKey(
                        name: "FK__WonBet__ID_of_Ga__62AFA012",
                        column: x => x.ID_of_Game,
                        principalTable: "Game",
                        principalColumn: "ID_of_Game");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID_of_user = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    UserName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Surname = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Patronymic = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Bio = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Admin = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    Avatar = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Location = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Male = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    ConfirmedEmail = table.Column<bool>(type: "bit", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__23D22D0A380B4478", x => x.ID_of_user);
                    table.ForeignKey(
                        name: "FK__Users__Avatar__46136164",
                        column: x => x.Avatar,
                        principalTable: "Images",
                        principalColumn: "Id_of_image");
                });

            migrationBuilder.CreateTable(
                name: "Balance",
                columns: table => new
                {
                    Id_Of_User = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Balance__93F2FC91C637AF8A", x => x.Id_Of_User);
                    table.ForeignKey(
                        name: "FK__Balance__Id_Of_U__6D2D2E85",
                        column: x => x.Id_Of_User,
                        principalTable: "Users",
                        principalColumn: "ID_of_user");
                });

            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    ID_of_user = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Notifications = table.Column<bool>(type: "bit", nullable: false),
                    Save_History = table.Column<bool>(type: "bit", nullable: false),
                    Dark_theme = table.Column<bool>(type: "bit", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Config__23D22D0A03C2C6DD", x => x.ID_of_user);
                    table.ForeignKey(
                        name: "FK__Config__ID_of_us__5A1A5A11",
                        column: x => x.ID_of_user,
                        principalTable: "Users",
                        principalColumn: "ID_of_user");
                });

            migrationBuilder.CreateTable(
                name: "Cupons_Used",
                columns: table => new
                {
                    ID_of_cupon = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    ID_of_user = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cupons_U__053EA69A1C16D58C", x => new { x.ID_of_cupon, x.ID_of_user });
                    table.ForeignKey(
                        name: "FK__Cupons_Us__ID_of__4AD81681",
                        column: x => x.ID_of_cupon,
                        principalTable: "Cupons",
                        principalColumn: "ID_of_cupon");
                    table.ForeignKey(
                        name: "FK__Cupons_Us__ID_of__4BCC3ABA",
                        column: x => x.ID_of_user,
                        principalTable: "Users",
                        principalColumn: "ID_of_user");
                });

            migrationBuilder.CreateTable(
                name: "EmailConfirmation",
                columns: table => new
                {
                    ID_req = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    ID_of_user = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Expire = table.Column<DateTime>(type: "datetime", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EmailCon__182A6452D3D54077", x => x.ID_req);
                    table.ForeignKey(
                        name: "FK__EmailConf__ID_of__5090EFD7",
                        column: x => x.ID_of_user,
                        principalTable: "Users",
                        principalColumn: "ID_of_user");
                });

            migrationBuilder.CreateTable(
                name: "Participation",
                columns: table => new
                {
                    Id_Of_User = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Id_Of_Room = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Particip__CB4A5550C652ACCB", x => new { x.Id_Of_User, x.Id_Of_Room });
                    table.ForeignKey(
                        name: "FK__Participa__Id_Of__7993056A",
                        column: x => x.Id_Of_User,
                        principalTable: "Users",
                        principalColumn: "ID_of_user");
                    table.ForeignKey(
                        name: "FK__Participa__Id_Of__7A8729A3",
                        column: x => x.Id_Of_Room,
                        principalTable: "Room",
                        principalColumn: "Id_Of_Room");
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    ID_of_session = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    ID_of_user = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Token = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sessions__7599B219643FFE3C", x => x.ID_of_session);
                    table.ForeignKey(
                        name: "FK__Sessions__ID_of___5555A4F4",
                        column: x => x.ID_of_user,
                        principalTable: "Users",
                        principalColumn: "ID_of_user");
                });

            migrationBuilder.CreateTable(
                name: "Statistic",
                columns: table => new
                {
                    Id_Of_User = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Chips_Won = table.Column<int>(type: "int", nullable: false),
                    Bets_Won = table.Column<int>(type: "int", nullable: false),
                    Bet = table.Column<int>(type: "int", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Statisti__93F2FC91D05D0875", x => x.Id_Of_User);
                    table.ForeignKey(
                        name: "FK__Statistic__Id_Of__74CE504D",
                        column: x => x.Id_Of_User,
                        principalTable: "Users",
                        principalColumn: "ID_of_user");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cupons_Used_ID_of_user",
                table: "Cupons_Used",
                column: "ID_of_user");

            migrationBuilder.CreateIndex(
                name: "IX_EmailConfirmation_ID_of_user",
                table: "EmailConfirmation",
                column: "ID_of_user");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_Id_Of_Room",
                table: "Participation",
                column: "Id_Of_Room");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Id_Of_Game",
                table: "Room",
                column: "Id_Of_Game");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ID_of_user",
                table: "Sessions",
                column: "ID_of_user");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Avatar",
                table: "Users",
                column: "Avatar");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__C9F2845639396034",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balance");

            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "Cupons_Used");

            migrationBuilder.DropTable(
                name: "EmailConfirmation");

            migrationBuilder.DropTable(
                name: "Participation");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Statistic");

            migrationBuilder.DropTable(
                name: "WonBet");

            migrationBuilder.DropTable(
                name: "Cupons");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}

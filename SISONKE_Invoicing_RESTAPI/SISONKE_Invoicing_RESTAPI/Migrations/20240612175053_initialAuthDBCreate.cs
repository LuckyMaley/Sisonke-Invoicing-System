using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SISONKE_Invoicing_RESTAPI.Migrations
{
    public partial class initialAuthDBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12c368f6-813e-405b-8491-f136055968bf", null, "Employee", "EMPLOYEE" },
                    { "7df19f08-19a3-46fe-a902-d865a0dd8a33", null, "Administrator", "ADMINISTRATOR" },
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "address", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "first_name", "last_name", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0b4fbee5-66b9-4362-bc07-14050eee108f", 0, "3113 Clyde Gallagher Crossing", "a18efd63-dba4-4ea6-b681-7ea8324cca5e", "ApplicationUser", "kivashkin7@salon.com", true, "Kerr", "Ivashkin", false, null, null, "KIVASHKIN7", "AQAAAAEAACcQAAAAEPH4N54IJQUsx+nldKfMEPgh6FIEb7uO+MFP90REihOdzkXGgWuLJIqVX+GfwLVriw==", "733-331-0434", false, "1227b9b9-eccc-4571-af40-fbdb8b357772", false, "kivashkin7" },
                    { "1619013e-539a-49a7-8eea-9720de11e11c", 0, "24 Amoth Street", "a3487c28-4927-45cf-93eb-d7016f0e3b10", "ApplicationUser", "gturpin1@si.edu", true, "Gasparo", "Turpin", false, null, null, "GTURPIN1", "AQAAAAEAACcQAAAAEPC1MBn5Ww3jQLi8noX9y7s+HUGIYa9kBU0EE0BR6rTJjyNGbskMxaZD8E18D02omw==", "0969508155", false, "575487c7-243b-4bf7-8c9a-d7f46d48080d", false, "gturpin1" },
                    { "17902040-2c5c-4577-9028-775f96cb0c18", 0, "100 Everton street, 6001", "58adfb48-b8de-4719-be6d-01f8502a8cb8", "ApplicationUser", "Fodenp47@gmail.com", true, "Phil", "Foden", false, null, null, "FODENP47ADMIN", "AQAAAAEAACcQAAAAEB+9apa2Dq04effzrTpZD/zEinE5ST3zjeXkiXJ+pycJr7iZTV1/G9BWwtNq9cdDJg==", "0746277885", false, "f7550995-7bb2-4211-be85-9f45aaed34f0", false, "Fodenp47Admin" },
                    { "3b19e2bf-c46a-4763-ae42-d172bc40040a", 0, "230 West street, 4001", "03994cd5-dc88-49e6-85d6-8f3dcec40169", "ApplicationUser", "Harrionj01@gmail.com", true, "Jack", "Harrison", false, null, null, "HARRISON01ADMIN", "AQAAAAEAACcQAAAAECMUz7kg6TjbS3tv/FIko2yZENvhB+jerfaHQiGrCxf5lxMH/QcFX6rsMablj4oC/A==", "0675647385", false, "9867d518-d6b1-4d58-a449-1653550b491b", false, "Harrison01Admin" },
                    { "469cb03e-60af-4902-bbb3-729ac89eec36", 0, "28 Sandton avenue, 2001", "350efc73-a786-4310-847a-3b05ad3ba393", "ApplicationUser", "Wakez007@gmail.com", true, "Zack", "Wake", false, null, null, "WAKEZ007ADMIN", "AQAAAAEAACcQAAAAELH/US7395eGWxdyl8GfpERXm4ZYiTA1IeDK7QI0P8yL90FQh7zVaMV1BARvTOhWWA==", "0745667386", false, "134e9694-fd44-4abc-9a3a-f6f9b6221ae8", false, "Wakez007Admin" },
                    { "4bc919ea-e389-40d3-8871-2dc18176688a", 0, "8 Merchant Point", "b77519b3-2521-49fb-9d00-b7ace86fcb3a", "ApplicationUser", "ecantopher0@biblegateway.com", true, "Elga", "Cantopher", false, null, null, "ECANTOPHER0", "AQAAAAEAACcQAAAAECbooVspU/mZ3MFfEUZ+L87eu48zRatmuecBbEY78tOgUtaFRenyjxo7FJYytGE6mQ==", "0896572674", false, "87179c44-5f73-40b7-8416-02a033995694", false, "ecantopher0" },
                    { "5ee05b06-3a4f-45cd-a10a-9192dc4c44f9", 0, "207 Delladonna Drive", "c24efb1c-56fa-4a42-990c-3eb34734364e", "ApplicationUser", "rmaiden3@vimeo.com", true, "Roshelle", "Maiden", false, null, null, "RMAIDEN3", "AQAAAAEAACcQAAAAELO1ggXoHYtFf69X17aacZCUqLX76WUZMuuRtJAVl5oH9RFLlizvW5Z+QGysNlS0zw==", "0101459782", false, "202412a0-580a-4657-9e06-118164570912", false, "rmaiden3" },
                    { "65891127-c413-4801-aa16-498245cc944c", 0, "18 Jack avenue, 2001", "cfb249ec-af4b-4796-a21a-2776c09f931e", "ApplicationUser", "Zzimela@gmail.com", true, "Zandile", "Zimela", false, null, null, "ZZIMELAADMIN", "AQAAAAEAACcQAAAAEJzNDsV+MKXou79djTV4NA0yafwFC1hogZdwB/WyLmrm6ISkioaFCcmoCcg8DpXUEw==", "0743244345", false, "08de0d83-94ae-47db-bfe0-2db0d9ef69e5", false, "ZzimelaAdmin" },
                    { "65ff9d7e-ae39-4a2a-9019-247334ae953a", 0, "656 Menomonie Hill", "58584bf0-5b12-42de-9684-b1ddb6f376fb", "ApplicationUser", "eguinnane8@discovery.com", true, "Emelda", "Guinnane", false, null, null, "EGUINNANE8", "AQAAAAEAACcQAAAAEGmFdrH5SQepwKnAJJZjbWhfRa19B1HJBeEPaqYxvIRrHxjcHx3EYqSvrXWP8Q/tKw==", "656-188-2034", false, "6d352ca7-edb5-4226-b35c-1b1b5b5b01f5", false, "eguinnane8" },
                    { "799eabc0-0f76-4c49-97bb-8489fe009bdd", 0, "36383 Hallows Place", "961a4496-a826-418e-ba2c-b465c81d31f9", "ApplicationUser", "tmacginney5@goodreads.com", true, "Tarra", "MacGinney", false, null, null, "TMACGINNEY5", "AQAAAAEAACcQAAAAEJ3tOQd8sMwQuysrC2y1jF6FBHSf/3RwW9l4bB+e7qoivhiWeHlInqLflSi3yqRABw==", "122-564-5165", false, "8eef8ae2-025a-4d9a-a940-f87de1e027c9", false, "tmacginney5" },
                    { "7bf00129-f294-450f-9cdd-3f34a45117c2", 0, "5268 Division Parkway", "eaad9fce-bc1d-4b2b-ae8f-d4805b274ecd", "ApplicationUser", "olittley8@gmail.com", true, "Orazio", "Littley", false, null, null, "OLITTLEY8", "AQAAAAEAACcQAAAAEHle3b0Eqkw1deClWTJgIsfS6wbd2qUFrDaqRSu/6BJhRnHQCw4RhXUert3mZFS0jA==", "2684070285", false, "eb67f48d-8c42-4116-9e0a-dc652eff8d68", false, "olittley8" },
                    { "7e22efcf-5f89-47d1-aaaf-0e8fd251c13a", 0, "15 Zimbali avenue, 4501", "362a778b-2da8-495d-af3a-b047b9dad9c6", "ApplicationUser", "Sanchoh@gmail.com", true, "Hannah", "Sancho", false, null, null, "SANCHOHADMIN", "AQAAAAEAACcQAAAAEK/ULLx509gvjVJsARYr6bRcWePnF2TX6dVTLQzYBmyYsB1A1rwS5AEvcbRAYUW1eQ==", "0673004545", false, "b6581fcc-db9c-4ae2-b223-1fdf874af96c", false, "SanchohAdmin" },
                    { "8004e687-9851-4912-b5c3-123c842d95b4", 0, "20 Jack avenue, 2001", "5a4b7c99-ec31-4a79-8e16-b1c00462e97f", "ApplicationUser", "Kcarrick@gmail.com", true, "Kylian", "Carrick", false, null, null, "KCARRICKADMIN", "AQAAAAEAACcQAAAAEOsCO9OnMVpNEEl5MFdzbQcgrluWZmY1B7hhJOY+bdINgeDhQI7etSnVCgYJ4KsJDg==", "0675249849", false, "44b1d249-6871-4522-8a60-56206a50bb15", false, "KcarrickAdmin" },
                    { "832671a4-b958-4500-9933-21508a9a9722", 0, "15 Harrison avenue, 3001", "be02ae79-d5b3-4a99-b405-bc4d4ce35d06", "ApplicationUser", "Walkerc07@gmail.com", true, "Cristiano", "Walker", false, null, null, "WALKERC07ADMIN", "AQAAAAEAACcQAAAAEBBXgfXVj6jDqM0P75X/hE1ZL9nUrSobl6nnu4NaBR8h7iX4+nCM4Vxfc2t53PpIBg==", "0743264748", false, "dca7dce0-28e3-40f5-99c2-74bce6ec4614", false, "Walkerc07Admin" },
                    { "846d87b3-a553-4eb7-bfbc-6941c562c402", 0, "8 Saint Paul Pass", "ad35737a-b053-4fe3-8c08-9e20690fbe8e", "ApplicationUser", "mwoolaston2@soup.io", true, "Laying", "Woolaston", false, null, null, "MWOOLASTON2", "AQAAAAEAACcQAAAAEGucY5dvQlfGEbGZlm1+/2lSKaIbfn8UVNRVqreBk4ZiQpFdDpTvhpT8SpM6B4gClg==", "404-725-6084", false, "d51de964-294f-4a77-ac84-ffb0d14aa135", false, "mwoolaston2" },
                    { "8639092a-33d4-43b2-a621-cf6239a1e665", 0, "6 Kim Place", "259674a1-f2eb-4dd6-8b81-49bea581370f", "ApplicationUser", "lmeasom1@networkadvertising.org", true, "Lanni", "Measom", false, null, null, "LMEASOM1", "AQAAAAEAACcQAAAAEFIzjCogGLF0uOyUPWTUM/IYTrob24OiyLDwMN3rd5Cw3EpLN0GLN9eFbmgM61ItMQ==", "991-219-0321", false, "e095e110-3abb-4d18-9ff6-6f3032215afa", false, "lmeasom1" },
                    { "8a1c3018-9743-4cdc-86b8-39e86f240b42", 0, "0148 Little Fleur Hill", "bac302a3-bbd1-49b5-baca-e9b19a9c062f", "ApplicationUser", "crhodef6@shutterfly.com", true, "Cacilia", "Rhodef", false, null, null, "CRHODEF6", "AQAAAAEAACcQAAAAEH4YtTLl3Gu9wWx38lR80GbggJ5D4JzyBGoYDiTXNGzk1S4xDXWB4YlhYqALZOyf9Q==", "640-914-6862", false, "e21b30ab-44d5-47b9-b584-61cf7c35c572", false, "crhodef6" },
                    { "a4729d4c-efa6-47c6-8dff-70bfe516f6ce", 0, "15 Zuma avenue, 2001", "29e54802-4b93-4c6b-8f95-96cd1c85a4e7", "ApplicationUser", "Efronz@gmail.com", true, "Zack", "Efron", false, null, null, "EFRONZ", "AQAAAAEAACcQAAAAEH+ockmwVAauowaOgEmTvjD/nKnJv+P7oqbjfF5if4w6SxGxvcwFhwzLhHPI/O9Fwg==", "074396748", false, "f7f023ab-b1c6-42ee-96f2-e43ce7ee48a2", false, "Efronz" },
                    { "a74790f6-4d41-4015-a56d-186c82f85d1e", 0, "14 Mayer Junction", "e6aeefe4-b367-41c8-945b-1250807d65c2", "ApplicationUser", "kabatelli4@canalblog.com", true, "Kesley", "Abatelli", false, null, null, "KABATELLI4", "AQAAAAEAACcQAAAAEL3Kg07b+A+xi+nIGUM4cIU+P4Voi+uorncs6guUgtJ8s8zzvNJ5Cg1dQupdGoKKqw==", "0977259204", false, "9a382467-6e93-4bb8-b5cd-f3646f1253e0", false, "kabatelli4" },
                    { "ac5b10e4-fe27-42d5-8ac5-81c3eb3b2fb4", 0, "19 Jack avenue, 2001", "9e245d54-657e-451f-9a92-8b76271e4208", "ApplicationUser", "Luisd7@gmail.com", true, "Luis", "Diaz", false, null, null, "LUIS7ADMIN", "AQAAAAEAACcQAAAAELwuibpBeQePTM4c7UEf+VCRn/GaGw9AhzPJmIOXsKicfpyQ1qz7JxbtI++dh/FQeQ==", "0742687829", false, "e1be7e7b-23f3-49bd-a81e-50985be0ef71", false, "Luis7Admin" },
                    { "d82b8577-befc-469a-bd2d-f122b5b5badd", 0, "0 Garrison Crossing", "063f572c-dcea-4e2e-85aa-0696a363cc5c", "ApplicationUser", "ebartaloni4@intel.com", true, "Ernesto", "Bartaloni", false, null, null, "EBARTALONI4", "AQAAAAEAACcQAAAAENJV0HCpl0X6+WfbbmaUtewrofFGkTOijLv9tP3WBl/pvOpa1jR3SvwI4JvPdfyYpQ==", "799-505-6110", false, "7b361bf0-3fa1-4343-824e-4db8c394e886", false, "ebartaloni4" },
                    { "dbce6a3a-0249-4b1a-9072-e6a56a359a19", 0, "35347 Eliot Place", "906f0762-1864-4e7b-8801-ee37f7cc1681", "ApplicationUser", "bchavrin7@gmail.com", true, "Bekki", "Chavrin", false, null, null, "BCHAVRIN7", "AQAAAAEAACcQAAAAEGBTegu32KvAAZotJOwLJ+Kq3Hgf0vISmBDIc7Rk7fIqqZTDM1XjCQye+LSgMuPHAg==", "0059209719", false, "726fd921-b4dc-4cd0-b4bd-290c19359db3", false, "bchavrin7" },
                    { "e29021ec-2930-42e3-8e70-6869b5163e16", 0, "20 King Edwards avenue, 3201", "736f469e-be1a-492a-a42c-a86c28dbd9fc", "ApplicationUser", "Bruno22@gmail.com", true, "Bruno", "Sterling", false, null, null, "BRUNOS22ADMIN", "AQAAAAEAACcQAAAAENzf168kC+IhW3yqEljg9saLCENV3UMpp1r8H5U6mMSniLHYzDvH3FgMQsXS4xEXtQ==", "0843778347", false, "f5feb652-2ac1-4ade-943f-4437837d852f", false, "Brunos22Admin" },
                    { "e66500b8-6d77-4934-9a0d-319aa7d63138", 0, "06 Paget Crossing", "08b0ffe6-33ad-43a2-802d-5834ade3a268", "ApplicationUser", "mbucky5@house.gov", true, "Merridie", "Bucky", false, null, null, "MBUCKY5", "AQAAAAEAACcQAAAAEAvnxLEZT3a9kb3YNuWZ7XZnFgtpr+ObjktYteDE/FBm06YceB804Jxx4Fhd7dZKTA==", "0977259204", false, "8e8e3940-f733-44d8-b6d3-24e5a59b0e73", false, "mbucky5" },
                    { "eb647f73-c7a9-4994-8fc0-e46a6c4d7e0b", 0, "09060 Hanover Trail", "0dbf3036-a82a-4dcf-8511-98eb99c2d1cf", "ApplicationUser", "lhamby3@a8.net", true, "Linda", "Hamby", false, null, null, "LHAMBY3", "AQAAAAEAACcQAAAAELK0MiOCBvD1+ouEiJ7EI2HOV5AIdNJXciLPNI4Dz2lZdbXPZRDHEbYq1b3VVHYt7g==", "879-880-7372", false, "2054a043-98d4-412f-a06d-e7eb5f802dc6", false, "lhamby3" },
                    { "ef9769db-1425-47a4-bd9a-6fb2a34b81bd", 0, "0 Prairieview Point", "5882b23d-32c5-4b14-90b7-8711893b38e3", "ApplicationUser", "lpurdom6@kickstarter.com", true, "Lawry", "Purdom", false, null, null, "LPURDOM6", "AQAAAAEAACcQAAAAEAbiEDsbv4r95wKF7buEVhsMTmE9tx8p7mam/ors7OVOx3Aqw/3Ayp7Ubdx2cw+xJA==", "0669797870", false, "57c875bd-4009-4ff7-9293-d1f2b108f945", false, "lpurdom6" },
                    { "f10e8fbe-21d1-484e-b68d-d3a8669525c6", 0, "25 Zuma avenue, 2001", "4a0cb380-5c68-4e4c-97f0-1ad364601174", "ApplicationUser", "Jkhalid@gmail.com", true, "Jordan", "Khalid", false, null, null, "JKHALID", "AQAAAAEAACcQAAAAEB6n/+hqeQ7ApuqaPW9Jm6NV7t1+FarrT9VjrUWsn8QQcuWWDN7ZSGB/YYZq4DWdpQ==", "074396748", false, "9c4b999f-250c-488f-a18d-d7c48cc4e7f1", false, "Jkhalid" },
                    { "fab4a8ed-08d5-4180-a56c-8814f39ccec8", 0, "290571 Sunbrook Alley", "64c3f692-c3cd-4dad-8b25-c3545c316133", "ApplicationUser", "hvenny2@hao123.com", true, "Harlin", "Venny", false, null, null, "HVENNY2", "AQAAAAEAACcQAAAAEMSmZDaNHolkEJfClQwHuMVlbAwqfXsggKZaezG9IQUjTAGPLx+dnyvNJ8VXNfAt3g==", "0969508155", false, "93b59212-fdbb-48f9-8a97-0bc06f796409", false, "hvenny2" },
                    { "faf95f6d-9d75-4f52-b2f1-51ce2f41b6df", 0, "22310 Schmedeman Parkway", "7009ad1a-8ddd-40be-8d23-c3030c5a3de9", "ApplicationUser", "kmaybey0@gmail.com", true, "Katharyn", "Maybey", false, null, null, "KMAYBEY0", "AQAAAAEAACcQAAAAEPO4epHpEQhOsWzIPeGBHuxEAaBLizUC6IvWwfUph49ly8lPAX0IBvAE1ITrvNmPOA==", "368-346-0788", false, "8acba5f4-1132-46be-a022-ffe7f03105ce", false, "kmaybey0" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", "0b4fbee5-66b9-4362-bc07-14050eee108f" },
                    { "12c368f6-813e-405b-8491-f136055968bf", "1619013e-539a-49a7-8eea-9720de11e11c" },
                    { "7df19f08-19a3-46fe-a902-d865a0dd8a33", "17902040-2c5c-4577-9028-775f96cb0c18" },
                    { "7df19f08-19a3-46fe-a902-d865a0dd8a33", "3b19e2bf-c46a-4763-ae42-d172bc40040a" },
                    { "7df19f08-19a3-46fe-a902-d865a0dd8a33", "469cb03e-60af-4902-bbb3-729ac89eec36" },
                    { "12c368f6-813e-405b-8491-f136055968bf", "4bc919ea-e389-40d3-8871-2dc18176688a" },
                    { "12c368f6-813e-405b-8491-f136055968bf", "5ee05b06-3a4f-45cd-a10a-9192dc4c44f9" },
                    { "7df19f08-19a3-46fe-a902-d865a0dd8a33", "65891127-c413-4801-aa16-498245cc944c" },
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", "65ff9d7e-ae39-4a2a-9019-247334ae953a" },
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", "799eabc0-0f76-4c49-97bb-8489fe009bdd" },
                    { "12c368f6-813e-405b-8491-f136055968bf", "7bf00129-f294-450f-9cdd-3f34a45117c2" },
                    { "7df19f08-19a3-46fe-a902-d865a0dd8a33", "7e22efcf-5f89-47d1-aaaf-0e8fd251c13a" },
                    { "7df19f08-19a3-46fe-a902-d865a0dd8a33", "8004e687-9851-4912-b5c3-123c842d95b4" },
                    { "7df19f08-19a3-46fe-a902-d865a0dd8a33", "832671a4-b958-4500-9933-21508a9a9722" },
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", "846d87b3-a553-4eb7-bfbc-6941c562c402" },
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", "8639092a-33d4-43b2-a621-cf6239a1e665" },
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", "8a1c3018-9743-4cdc-86b8-39e86f240b42" },
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", "a4729d4c-efa6-47c6-8dff-70bfe516f6ce" },
                    { "12c368f6-813e-405b-8491-f136055968bf", "a74790f6-4d41-4015-a56d-186c82f85d1e" },
                    { "7df19f08-19a3-46fe-a902-d865a0dd8a33", "ac5b10e4-fe27-42d5-8ac5-81c3eb3b2fb4" },
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", "d82b8577-befc-469a-bd2d-f122b5b5badd" },
                    { "12c368f6-813e-405b-8491-f136055968bf", "dbce6a3a-0249-4b1a-9072-e6a56a359a19" },
                    { "7df19f08-19a3-46fe-a902-d865a0dd8a33", "e29021ec-2930-42e3-8e70-6869b5163e16" },
                    { "12c368f6-813e-405b-8491-f136055968bf", "e66500b8-6d77-4934-9a0d-319aa7d63138" },
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", "eb647f73-c7a9-4994-8fc0-e46a6c4d7e0b" },
                    { "12c368f6-813e-405b-8491-f136055968bf", "ef9769db-1425-47a4-bd9a-6fb2a34b81bd" },
                    { "12c368f6-813e-405b-8491-f136055968bf", "f10e8fbe-21d1-484e-b68d-d3a8669525c6" },
                    { "12c368f6-813e-405b-8491-f136055968bf", "fab4a8ed-08d5-4180-a56c-8814f39ccec8" },
                    { "d9c533e4-ab78-4336-9304-55838cc8bb29", "faf95f6d-9d75-4f52-b2f1-51ce2f41b6df" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

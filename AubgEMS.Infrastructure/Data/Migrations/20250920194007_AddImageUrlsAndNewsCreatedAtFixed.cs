using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AubgEMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlsAndNewsCreatedAtFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "NewsPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Body = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPosts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrganizerId = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubs_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    OrganizerId = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    EventTypeId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "EventAttendances",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAttending = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAttendances", x => new { x.EventId, x.UserId });
                    table.ForeignKey(
                        name: "FK_EventAttendances_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a3f8c34-3e7d-4b11-9d4f-aaa000000001", null, "Admin", "ADMIN" },
                    { "6a3f8c34-3e7d-4b11-9d4f-aaa000000002", null, "Organizer", "ORGANIZER" },
                    { "6a3f8c34-3e7d-4b11-9d4f-aaa000000003", null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "5d9d7e56-1279-4d2b-ba3d-871b68434ee6", "organizer@mail.com", true, false, null, "ORGANIZER@MAIL.COM", "ORGANIZER@MAIL.COM", "AQAAAAIAAYagAAAAEExXcG1SLzfcL/TBDGcpGkgo97GjEiRAkHxHaLSc7YGN1ScSYpRlBbWtOFGzVGc5fQ==", null, false, "b7103ea0-d289-435d-8b01-ba739a1adc44", false, "organizer@mail.com" },
                    { "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9", 0, "afbe7bc2-5c79-4e2d-b906-060e110f467c", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAIAAYagAAAAEHc+8XXTDy2I3gDU03fk/wBOchMrW5clW2Mookpa2mM6eWvs9G9G1BwJ66XMHA8c2g==", null, false, "be122832-9527-4fc6-8ebb-12cea37b82c0", false, "admin@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "DepartmentId", "Description", "Name", "OrganizerId" },
                values: new object[] { 5, null, null, "Other", "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Business Department" },
                    { 2, "Computer Science Department" },
                    { 3, "Economics Department" },
                    { 4, "History and Civilizations Department" },
                    { 5, "Journalism, Media, and Communication Department" },
                    { 6, "Literature and Theater Department" },
                    { 7, "Mathematics and Science Department" },
                    { 8, "Modern Languages and Arts Department" },
                    { 9, "Philosophy and Psychology Department" },
                    { 10, "Politics and European Studies Department" }
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Workshop" },
                    { 2, "Seminar" },
                    { 3, "Lecture" },
                    { 4, "Meeting" },
                    { 5, "Challenge" },
                    { 6, "Sports" },
                    { 7, "Theatre Performance" },
                    { 8, "Dance Performance" },
                    { 9, "Movie Screening" },
                    { 10, "Conference" },
                    { 11, "Video Shooting" },
                    { 12, "Concert" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, 100, "BAC Auditorium" },
                    { 2, 36, "BAC Room 001" },
                    { 3, 35, "BAC Room 002" },
                    { 4, 33, "BAC Room 003" },
                    { 5, 39, "BAC Room 004" },
                    { 6, 24, "BAC Room 101" },
                    { 7, 26, "BAC Room 103" },
                    { 8, 25, "BAC Room 201" },
                    { 9, 39, "BAC Room 202" },
                    { 10, 41, "BAC Room 203" },
                    { 11, 33, "BAC Room 204" },
                    { 12, 24, "BAC Room 206" },
                    { 13, 14, "BAC Room 207" },
                    { 14, 50, "Panitza Library" },
                    { 15, 300, "Dr. Carl Djerassi Theater Hall " },
                    { 16, 200, "Sports Hall" },
                    { 17, 30, "Aspire" },
                    { 18, 50, "Ground Floor Lobby" },
                    { 19, 30, "6306 - Board Room" },
                    { 20, 30, "6307 - Postbank Room" },
                    { 21, 40, "6307 - Restaurant" },
                    { 22, 20, "6307 - Café" },
                    { 23, 108, "MB Auditorium" },
                    { 24, 40, "Skaptopara I MPR" },
                    { 25, 40, "Skaptopara II MPR" },
                    { 26, 40, "Skaptopara III MPR" },
                    { 27, 40, "Outdoors" }
                });

            migrationBuilder.InsertData(
                table: "NewsPosts",
                columns: new[] { "Id", "Body", "CreatedAt", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { 1, "The academic year has officially begun, and the campus is buzzing with energy as students return. From classes to social events, there’s something for everyone. The Semester Kickoff marks the start of a new journey filled with opportunities, challenges, and experiences that will shape the year ahead.", new DateTime(2025, 9, 15, 8, 0, 0, 0, DateTimeKind.Utc), "https://www.aubg.edu/wp-content/uploads/2022/05/about-hero-bg.jpg", "Semester Kickoff" },
                    { 2, "This week marks the start of club recruitment, where students can sign up for organizations that match their passions and interests. Whether you’re into finance, debating, arts, or volunteering, there’s a place for you. Recruitment Week is the best time to connect with peers and discover your community.", new DateTime(2025, 9, 18, 9, 30, 0, 0, DateTimeKind.Utc), "https://iee.bg/wp-content/uploads/2023/08/DSC02107-1-2-scaled.jpg", "Clubs Recruitment Week" },
                    { 3, "Applications are now open for Pitch Night, the signature event where students showcase innovative startup ideas. Teams will compete for recognition and prizes, while gaining valuable feedback from mentors and judges. Don’t miss your chance to present your project and take the first step toward entrepreneurial success.", new DateTime(2025, 9, 20, 14, 0, 0, 0, DateTimeKind.Utc), "https://www.aubg.edu/wp-content/uploads/2022/05/students-life-hero.jpg", "Pitch Night Applications Open" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6a3f8c34-3e7d-4b11-9d4f-aaa000000002", "dea12856-c198-4129-b3f3-b893d8395082" },
                    { "6a3f8c34-3e7d-4b11-9d4f-aaa000000001", "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9" }
                });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "DepartmentId", "Description", "Name", "OrganizerId" },
                values: new object[,]
                {
                    { 1, 2, "Entrepreneurship & innovation hub", "The Hub AUBG", "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9" },
                    { 2, 3, "Student-led investing", "Investment Management Club", "dea12856-c198-4129-b3f3-b893d8395082" },
                    { 3, 1, null, "Business Club AUBG", "dea12856-c198-4129-b3f3-b893d8395082" },
                    { 4, 10, null, "MEU AUBG", "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "ClubId", "Description", "EventTypeId", "ImageUrl", "LocationId", "OrganizerId", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, 200, 1, "The Welcome Fair is one of the most anticipated events of the semester, bringing together all student clubs and organizations under one roof. Students have the chance to explore the different opportunities AUBG offers beyond academics, from entrepreneurship and debating to sports and arts. The event is designed to showcase the diverse extracurricular activities available and to encourage newcomers to join and become active members of the community. Informational booths, short presentations, and networking opportunities make this fair both informative and exciting, setting the tone for an engaging and dynamic academic year ahead.", 4, "https://www.aubg.edu/wp-content/uploads/2024/09/DSC07164-1.jpg", 23, "dea12856-c198-4129-b3f3-b893d8395082", new DateTime(2025, 10, 1, 16, 0, 0, 0, DateTimeKind.Utc), "Welcome Fair" },
                    { 2, 150, 2, "This introductory session on Artificial Intelligence aims to provide students from all backgrounds with a comprehensive overview of what AI is, how it works, and why it matters. The lecture will cover the basics of machine learning, neural networks, and everyday AI applications, while also addressing the ethical concerns surrounding automation and data usage. It is an open and beginner-friendly talk designed to spark curiosity and inspire further exploration in the field. The event will conclude with an interactive Q&A session where attendees can discuss potential projects, research ideas, and applications of AI in real-world contexts.", 3, "https://aubgdaily.com/wp-content/uploads/2017/03/Optimized-_DSC1111.jpg", 1, "dea12856-c198-4129-b3f3-b893d8395082", new DateTime(2025, 10, 5, 18, 30, 0, 0, DateTimeKind.Utc), "AI 101: Intro Talk" },
                    { 3, 120, 1, "Pitch Night is the ultimate stage for students with entrepreneurial ambitions to present their innovative ideas before a panel of faculty, alumni, and external experts. Each team will have a limited time to pitch their concept, explain the problem it solves, and highlight its market potential. The event is not only a competition but also an opportunity for mentorship and constructive feedback. Even teams that don’t win will walk away with valuable insights to refine their ideas. Pitch Night fosters creativity, teamwork, and problem-solving skills while also connecting aspiring entrepreneurs with potential investors and collaborators within the AUBG network.", 1, "https://www.aubg.edu/wp-content/uploads/2023/05/Elevate-The-Recursive.jpg", 14, "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9", new DateTime(2025, 10, 12, 19, 0, 0, 0, DateTimeKind.Utc), "Pitch Night" },
                    { 4, 100, 3, "The Case Study Challenge is a team-based competition where participants are given a real-world business scenario and tasked with developing practical, data-driven solutions under time constraints. Teams will analyze the problem, brainstorm strategies, and present their solutions to a panel of judges composed of professors and industry professionals. The challenge pushes students to apply classroom knowledge in a fast-paced and highly collaborative environment. It promotes critical thinking, communication, and decision-making skills, while also exposing participants to real issues faced by modern businesses. Whether winning or not, all teams gain experience that is invaluable for future academic and career pursuits.", 5, "https://aubgbusinessclub.com/wp-content/uploads/2022/11/Web-59-1-scaled.jpg", 15, "dea12856-c198-4129-b3f3-b893d8395082", new DateTime(2025, 10, 20, 17, 0, 0, 0, DateTimeKind.Utc), "Case Study Challenge" },
                    { 5, 180, 4, "The Model European Union (MEU) Simulation Conference is a multi-day event designed to replicate the workings of European institutions. Students will take on the roles of parliament members, ministers, or diplomats and engage in debates, negotiations, and policy-making activities. The simulation provides a unique, immersive experience that combines academic knowledge with practical diplomatic skills. Participants will prepare resolutions, argue positions, and attempt to build consensus in a politically diverse environment. It is both intellectually stimulating and socially engaging, as students interact with peers from different disciplines while exploring complex issues that mirror the challenges faced by policymakers today.", 10, "https://www.aubg.edu/wp-content/uploads/2025/04/AUBG-Today-Hero-Image-3.jpg", 25, "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9", new DateTime(2025, 10, 26, 10, 0, 0, 0, DateTimeKind.Utc), "MEU Simulation Conference" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_DepartmentId",
                table: "Clubs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_OrganizerId",
                table: "Clubs",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_ClubId",
                table: "Events",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTypes_Name",
                table: "EventTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventAttendances");

            migrationBuilder.DropTable(
                name: "NewsPosts");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a3f8c34-3e7d-4b11-9d4f-aaa000000003");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6a3f8c34-3e7d-4b11-9d4f-aaa000000002", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6a3f8c34-3e7d-4b11-9d4f-aaa000000001", "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a3f8c34-3e7d-4b11-9d4f-aaa000000001");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a3f8c34-3e7d-4b11-9d4f-aaa000000002");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9");
        }
    }
}

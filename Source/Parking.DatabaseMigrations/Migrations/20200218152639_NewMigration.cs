using Microsoft.EntityFrameworkCore.Migrations;

namespace Parking.DatabaseMigrations.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MqttServerConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    TCPServer = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UseTls = table.Column<bool>(nullable: false),
                    CleanSession = table.Column<bool>(nullable: false),
                    KeepAlive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MqttServerConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MqttTopicConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(nullable: true),
                    QoS = table.Column<int>(nullable: false),
                    MqttServerConfigurationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MqttTopicConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MqttTopicConfigurations_MqttServerConfigurations_MqttServerConfigurationId",
                        column: x => x.MqttServerConfigurationId,
                        principalTable: "MqttServerConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MqttTopicConfigurations_MqttServerConfigurationId",
                table: "MqttTopicConfigurations",
                column: "MqttServerConfigurationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MqttTopicConfigurations");

            migrationBuilder.DropTable(
                name: "MqttServerConfigurations");
        }
    }
}

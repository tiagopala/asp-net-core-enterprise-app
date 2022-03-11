using Microsoft.EntityFrameworkCore.Migrations;

namespace EnterpriseApp.Pedido.Infrastructure.Migrations
{
    public partial class Three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Pedidos_OrderId",
                table: "PedidoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Vouchers_VoucherId",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoItems",
                table: "PedidoItems");

            migrationBuilder.RenameTable(
                name: "Pedidos",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "PedidoItems",
                newName: "OrderItems");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_VoucherId",
                table: "Orders",
                newName: "IX_Orders_VoucherId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoItems_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Vouchers_VoucherId",
                table: "Orders",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Vouchers_VoucherId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Pedidos");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "PedidoItems");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_VoucherId",
                table: "Pedidos",
                newName: "IX_Pedidos_VoucherId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "PedidoItems",
                newName: "IX_PedidoItems_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoItems",
                table: "PedidoItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Pedidos_OrderId",
                table: "PedidoItems",
                column: "OrderId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Vouchers_VoucherId",
                table: "Pedidos",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

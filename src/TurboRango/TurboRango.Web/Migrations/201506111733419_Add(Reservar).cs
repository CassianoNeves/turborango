namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReservar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QtdPessoas = c.Int(nullable: false),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Turno = c.String(),
                        Restaurante_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurantes", t => t.Restaurante_Id)
                .Index(t => t.Restaurante_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "Restaurante_Id", "dbo.Restaurantes");
            DropIndex("dbo.Reservas", new[] { "Restaurante_Id" });
            DropTable("dbo.Reservas");
        }
    }
}

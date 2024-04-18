namespace CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Apellido = c.String(nullable: false, maxLength: 100),
                        CorreoElectronico = c.String(nullable: false, maxLength: 150),
                        Telefono = c.String(nullable: false, maxLength: 100),
                        Direccion = c.String(nullable: false, maxLength: 100),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Reservaciones",
                c => new
                    {
                        ReservacionId = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        TeatroId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        CantidadEntradas = c.Int(nullable: false),
                        PrecioTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReservacionId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .ForeignKey("dbo.Teatros", t => t.TeatroId)
                .Index(t => t.ClienteId)
                .Index(t => t.TeatroId);
            
            CreateTable(
                "dbo.Teatros",
                c => new
                    {
                        TeatroId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Direccion = c.String(nullable: false, maxLength: 150),
                        CapacidadAsientos = c.Int(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TeatroId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservaciones", "TeatroId", "dbo.Teatros");
            DropForeignKey("dbo.Reservaciones", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.Reservaciones", new[] { "TeatroId" });
            DropIndex("dbo.Reservaciones", new[] { "ClienteId" });
            DropTable("dbo.Teatros");
            DropTable("dbo.Reservaciones");
            DropTable("dbo.Clientes");
        }
    }
}

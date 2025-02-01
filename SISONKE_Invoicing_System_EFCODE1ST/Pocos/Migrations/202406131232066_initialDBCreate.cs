namespace Pocos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialDBCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.discounts",
                c => new
                    {
                        discount_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100),
                        rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.discount_id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        invoice_id = c.Int(nullable: false, identity: true),
                        ef_user_id = c.Int(nullable: false),
                        invoice_date = c.DateTime(nullable: false),
                        due_date = c.DateTime(nullable: false),
                        subtotal = c.Double(nullable: false),
                        tax = c.Int(nullable: false),
                        tax_amount = c.Double(nullable: false),
                        discount_id = c.Int(nullable: false),
                        discount_amount = c.Double(nullable: false),
                        total_amount = c.Double(nullable: false),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.invoice_id)
                .ForeignKey("dbo.discounts", t => t.discount_id, cascadeDelete: true)
                .ForeignKey("dbo.ef_users", t => t.ef_user_id, cascadeDelete: true)
                .Index(t => t.ef_user_id)
                .Index(t => t.discount_id);
            
            CreateTable(
                "dbo.ef_users",
                c => new
                    {
                        ef_user_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(nullable: false, maxLength: 50),
                        last_name = c.String(nullable: false, maxLength: 50),
                        email = c.String(nullable: false, maxLength: 100),
                        address = c.String(maxLength: 255),
                        phone_number = c.String(maxLength: 50),
                        identity_username = c.String(nullable: false, maxLength: 100),
                        role = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ef_user_id);
            
            CreateTable(
                "dbo.invoice_items",
                c => new
                    {
                        invoice_item_id = c.Int(nullable: false, identity: true),
                        invoice_id = c.Int(nullable: false),
                        product_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        unit_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        total_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.invoice_item_id)
                .ForeignKey("dbo.Invoices", t => t.invoice_id, cascadeDelete: true)
                .ForeignKey("dbo.products", t => t.product_id, cascadeDelete: true)
                .Index(t => t.invoice_id)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.products",
                c => new
                    {
                        product_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100),
                        description = c.String(maxLength: 300),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        stock_quantity = c.Int(nullable: false),
                        modified_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.product_id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        note_id = c.Int(nullable: false, identity: true),
                        invoice_id = c.Int(nullable: false),
                        invoice_notes = c.String(),
                        created_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.note_id)
                .ForeignKey("dbo.Invoices", t => t.invoice_id, cascadeDelete: true)
                .Index(t => t.invoice_id);
            
            CreateTable(
                "dbo.payments",
                c => new
                    {
                        payment_id = c.Int(nullable: false, identity: true),
                        invoice_id = c.Int(nullable: false),
                        payment_date = c.DateTime(nullable: false),
                        payment_method = c.String(maxLength: 100),
                        amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.payment_id)
                .ForeignKey("dbo.Invoices", t => t.invoice_id, cascadeDelete: true)
                .Index(t => t.invoice_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.payments", "invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.Notes", "invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.invoice_items", "product_id", "dbo.products");
            DropForeignKey("dbo.invoice_items", "invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "ef_user_id", "dbo.ef_users");
            DropForeignKey("dbo.Invoices", "discount_id", "dbo.discounts");
            DropIndex("dbo.payments", new[] { "invoice_id" });
            DropIndex("dbo.Notes", new[] { "invoice_id" });
            DropIndex("dbo.invoice_items", new[] { "product_id" });
            DropIndex("dbo.invoice_items", new[] { "invoice_id" });
            DropIndex("dbo.Invoices", new[] { "discount_id" });
            DropIndex("dbo.Invoices", new[] { "ef_user_id" });
            DropTable("dbo.payments");
            DropTable("dbo.Notes");
            DropTable("dbo.products");
            DropTable("dbo.invoice_items");
            DropTable("dbo.ef_users");
            DropTable("dbo.Invoices");
            DropTable("dbo.discounts");
        }
    }
}

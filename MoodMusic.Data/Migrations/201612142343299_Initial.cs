namespace MoodMusic.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Formats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormatName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Title = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.Title)
                .ForeignKey("dbo.Artists", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Formats", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "Id", "dbo.Genres");
            DropForeignKey("dbo.Tracks", "Id", "dbo.Formats");
            DropForeignKey("dbo.Tracks", "Id", "dbo.Artists");
            DropIndex("dbo.Tracks", new[] { "Id" });
            DropTable("dbo.Tracks");
            DropTable("dbo.Genres");
            DropTable("dbo.Formats");
            DropTable("dbo.Artists");
        }
    }
}

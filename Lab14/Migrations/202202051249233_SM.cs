namespace Lab14.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SM : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Authors", newName: "TableAuthor");
            RenameTable(name: "dbo.AuthorsBooks", newName: "TableAuthorsBooks");
            RenameTable(name: "dbo.Books", newName: "TableBooks");
            RenameTable(name: "dbo.UsersBooks", newName: "TableUsersBooks");
            RenameTable(name: "dbo.Users", newName: "TableUser");
            RenameColumn(table: "dbo.TableAuthor", name: "Name", newName: "AuthorName");
            RenameColumn(table: "dbo.TableBooks", name: "Title", newName: "BookTitle");
            RenameColumn(table: "dbo.TableUser", name: "Name", newName: "UserName");
            AlterColumn("dbo.TableAuthor", "AuthorName", c => c.String(maxLength: 50));
            AlterColumn("dbo.TableBooks", "BookTitle", c => c.String(maxLength: 50));
            AlterColumn("dbo.TableUser", "UserName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TableUser", "UserName", c => c.String());
            AlterColumn("dbo.TableBooks", "BookTitle", c => c.String());
            AlterColumn("dbo.TableAuthor", "AuthorName", c => c.String());
            RenameColumn(table: "dbo.TableUser", name: "UserName", newName: "Name");
            RenameColumn(table: "dbo.TableBooks", name: "BookTitle", newName: "Title");
            RenameColumn(table: "dbo.TableAuthor", name: "AuthorName", newName: "Name");
            RenameTable(name: "dbo.TableUser", newName: "Users");
            RenameTable(name: "dbo.TableUsersBooks", newName: "UsersBooks");
            RenameTable(name: "dbo.TableBooks", newName: "Books");
            RenameTable(name: "dbo.TableAuthorsBooks", newName: "AuthorsBooks");
            RenameTable(name: "dbo.TableAuthor", newName: "Authors");
        }
    }
}

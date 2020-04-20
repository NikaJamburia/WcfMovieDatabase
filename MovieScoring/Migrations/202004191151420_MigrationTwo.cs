namespace MovieScoring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationTwo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ActorMovies", newName: "MovieActors");
            DropPrimaryKey("dbo.MovieActors");
            AddPrimaryKey("dbo.MovieActors", new[] { "Movie_Id", "Actor_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MovieActors");
            AddPrimaryKey("dbo.MovieActors", new[] { "Actor_Id", "Movie_Id" });
            RenameTable(name: "dbo.MovieActors", newName: "ActorMovies");
        }
    }
}

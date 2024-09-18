using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _03_antity_framework
{
    public class AirplaneDBcontext:DbContext
    {
        //Collections
        //Airplane
        //Customers
        //Flight
        public AirplaneDBcontext() 
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"workstation id=Shomiakom3.mssql.somee.com;packet size=4096;user id=Grincchik_SQLLogin_1;pwd=gstgjnpeqp;data source=Shomiakom3.mssql.somee.com;persist security info=False;initial catalog=Shomiakom3;TrustServerCertificate=True");
        }
    }
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Artist> Artists { get; set; }

    }
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //RelationShip type Many to Many (*...*)
        public int CountryId { get; set; }
        public Country Country { get; set; }    
        public ICollection<Semple> Semples { get; set; }
        public ICollection<Album> Album { get; set; }
    }
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Release { get; set; }
        //RelationShip type One to Many (1.....*)
        public int GenreId { get; set; }
        public Genre Genrre { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Semple> Semples { get; set; }
        

    }
    public class PlayList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Semple> Semples { get; set; }
    }
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Semple
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public ICollection<PlayList> PlayLists { get; set; }

    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

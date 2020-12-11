using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyDAL
{
    public class DB : DbContext
    {
        public DB() : base("name=DB")
        {
            Database.CreateIfNotExists();

            Database.SetInitializer(new DBInit());
        }

        public virtual DbSet<RouteDb> Routes { get; set; }

        public virtual DbSet<RouteStationDb> RouteStations { get; set; }

        public virtual DbSet<StationDb> Stations { get; set; }

        public virtual DbSet<TicketDb> Tickets { get; set; }

        public virtual DbSet<DepartureDb> Departures { get; set; }

        public virtual DbSet<PassengerDb> Passengers { get; set; }

        public virtual DbSet<PassengerTypeDb> PassengerTypes { get; set; }

        public virtual DbSet<PriceDb> Prices { get; set; }
        public DbSet<AdminDb> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
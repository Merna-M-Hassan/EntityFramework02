using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_25_EF_Core.Models;
using Assignment_25_EF_Core.Configurations;
using System.Reflection;

namespace Assignment_25_EF_Core.Context
{
    public class EventHubDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server =(localdb)\\MSSQLLocalDB; Database = EventHubDB; Trusted_Connection = true; TrustServerCertificate = true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Whan add migration, how it knows there is an existance for EventConfiguration class, which contains all Event configurations, to deploy.
            //create an object, so in runtime when calling OnModelCreating() and Add-Migration, reach to the EventConfiguration class.
            //modelBuilder.ApplyConfiguration(new EventConfigurations());

            //Advanced Apply Configuration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());         //Reflection
            //Instead of each run-time execution, need to apply modelBuilder.ApplyConfiguration(new configurationClassName()),
            //In run-time will take the project which is installed in it ORM, and go through each class/model/context. Any class implements IEntityTypeConfiguration<className> interface,
            //start to apply its inside configurations.

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            #region OldWay
            //modelBuilder.Entity<Event>().ToTable("EventsTable");     //when applied in DB, what will be its name.

            //////////////////////////////////////////////HasKey()////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //modelBuilder.Entity<Event>().HasKey("EventId");             //to make a property PK.   --> Not safe version. If a property doesn't exist in the class, it will throw an exception.

            //modelBuilder.Entity<Event>().HasKey(nameof(Event.EventId));   //nameof(): go to the class name and get any needed property, and return as a string instead of exception if doesn't exist .

            //modelBuilder.Entity<Event>().HasKey(E => E.EventId);     //more safe          //Unique and not null, also add key identity (1, 1).

            //modelBuilder.Entity<OrganizerProfile>().HasKey(E => E.OrganizerId);
            #endregion

            modelBuilder.Entity<Badge>().HasKey(E => E.BadgeNumber);

            ///////////////////////////////////////////////Property()///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            #region OldWay
            //modelBuilder.Entity<Event>().Property(E => E.EventId)                           //To configure property inside a class.
            //    .UseIdentityColumn(10, 10);                              //start with 10 and add 10 eac new id.


            //modelBuilder.Entity<Event>().Property("EventName");     //Throw ex

            //modelBuilder.Entity<Event>().Property<string>("EventCategory");   //if property is not existed in the class, but use Generic of .Property() function   --> implement Shadow Property.
            ////Shadow Property: EF Core will create it as a column in DB table, its datatype is nvarchar(max), but not existed in  application class.

            //modelBuilder.Entity<Event>().Property(E => E.Title)
            //    .HasColumnName("EventName")
            //    .HasColumnType("varchar")
            //    .HasMaxLength(50)
            //    .IsRequired(true);                    //not allow null

            //modelBuilder.Entity<Event>().Property<decimal>("TicketPrice")
            //    //.HasColumnType("decimal(10,3)")
            //    .HasPrecision(10, 3);
            #endregion

            modelBuilder.Entity<OrganizerProfile>().Property(E => E.Email)
                .IsRequired();                    //Requied 

            modelBuilder.Entity<Badge>().Property(E => E.IssueDate)
                .HasDefaultValueSql("GETDATE()");

            /////////////////////////////////////////////////ToTable()////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            #region OldWay
            ////Pass the table name inside the check constraint method so EF knows which table to apply it to
            //modelBuilder.Entity<Event>()
            //    .ToTable("EventsTable", t => t.HasCheckConstraint("CheckAttendeesNumber", "AttendeesNumber BETWEEN 50 AND 150"));      //DB Apply
            #endregion

            modelBuilder.Entity<OrganizerProfile>()
                .ToTable(t => t.HasCheckConstraint("EmailFormatConstraint", "Email like '%@%.%'"));

            /////////////////////////////////////////////////Ignore()////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            #region OldWay
            //modelBuilder.Entity<OrganizerProfile>()
            //    .Ignore(E => E.UserName);                       //ignore to change property in class into a table in DB.
            #endregion

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<OrganizerProfile>(OP =>
            {
                OP.ToTable("OrganizerProfileTable", "OrganizerUser");      //if schema isn't exist in DB, it wil be created in DB/SQL Server.

                OP.HasKey(O => O.ProfileId);

                OP.Property(O => O.ProfileId)
                .UseIdentityColumn(100, 1);

                OP.Property(O => O.Biography)
                .HasColumnName("OrganizerDescription")
                .HasColumnType("varchar(200)")
                .IsRequired();

                OP.Ignore(O => O.UserName);

                //OP.Property(O => O.CreatedAt)
                //.HasDefaultValue(DateTime.Now);           //if manual value isn't added, the time of migration will be inserted in the column in DB.
                ////the timestamp which is included in migration file name.
                ////if an update occurs, not change the value unless it changes manually.


                //Created At
                OP.Property(O => O.CreatedAt)
                .HasDefaultValueSql("GETDATE()");      //if a manual value is not inserted,  it will call GETDATE() function in SQL Server.
                //SQL Server filling responsibilty.
                //assign the value of time value which an object inserted as a row in SQL Server.
                //Call GETDATE(), calculate current time of SQL Server, and inserted as a value.
                //if an update occurs, nothing will change, unless it changes manually.
                //use when need to know the time which the row is insertes in DB/SQL Server.


                ////Modified At
                //OP.Property(O => O.CreatedAt)
                //.HasComputedColumnSql("GETDATE()");    //manual value isn't allowed, the property will be read-only.
                ////call GETDATE() in SQL Server, calculate current time of SQL Server and inserted as a value.
                ////if an update occurs, will recalculate GETDATE() funtion and the value automatically.
                ////use when need to know the last time an update/modify occurs on the row.                          
            });


            /////////////////////////////////////////////////////// --FluentAPI Relationship (One-to-One) [Partial - Total]-- ////////////////////////////////////////////////////

            modelBuilder.Entity<Badge>()
                .HasOne(B => B.Attendee)
                .WithOne(A => A.HoldedBadge)
                .HasForeignKey<Badge>(B => B.BadgeAttendeeId)
                .OnDelete(DeleteBehavior.Restrict);


            /////////////////////////////////////////////////////// --FluentAPI Relationship (One-to-One) [Total - Total]-- ////////////////////////////////////////////////////

            //OrganizerProfile --> Its properties are stored as columns In Organizer Table
            //modelBuilder.Entity<Organizer>()
            //    .ComplexProperty(O => O.Profile);     //EF 8


            modelBuilder.Entity<OrganizerProfile>()
                 .HasOne(p => p.Organizer)
                 .WithOne(o => o.OrProfile)
                 .HasForeignKey<OrganizerProfile>(p => p.OrganizerId);


            /////////////////////////////////////////////////////// --FluentAPI Relationship (One-to-Many) [Partial - Total]-- ////////////////////////////////////////////////////

            //modelBuilder.Entity<Organizer>()
            //    .HasMany(O => O.Events)
            //    .WithOne(E => E.OrganizeEvent)
            //    .HasForeignKey(E => E.OrganizerId)
            //    .OnDelete(DeleteBehavior.Restrict);


        }


        /////////////////////////////////////////////////////// --Data Annotation-- ////////////////////////////////////////////////////

        public DbSet<Organizer> Organizer {  get; set; }

        public DbSet<Attendees> Attendee { get; set; }
        

    }
}

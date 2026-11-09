using Assignment_25_EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_25_EF_Core.Configurations
{
    internal class EventConfigurations : IEntityTypeConfiguration<Event>
    {

        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("EventsTable");     //when applied in DB, what will be its name.

            builder.HasKey(E => E.EventId);

            builder.Property(E => E.EventId)                           //To configure property inside a class.
               .UseIdentityColumn(10, 10);                              //start with 10 and add 10 eac new id.


            builder.Property<string>("EventCategory");   //if property is not existed in the class, but use Generic of .Property() function   --> implement Shadow Property.
            //Shadow Property: EF Core will create it as a column in DB table, its datatype is nvarchar(max), but not existed in  application class.

            builder.Property(E => E.Title)
                .HasColumnName("EventName")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(true);                    //not allow null

            builder.Property<decimal>("TicketPrice")
                //.HasColumnType("decimal(10,3)")
                .HasPrecision(10, 3);

            //Pass the table name inside the check constraint method so EF knows which table to apply it to
            builder.ToTable("EventsTable", t => t.HasCheckConstraint("CheckAttendeesNumber", "AttendeesNumber BETWEEN 50 AND 150"));      //DB Apply



            /////////////////////////////////////////////////////// --FluentAPI Relationship (One-to-Many) [Partial - Total]-- ////////////////////////////////////////////////////

            builder.HasOne(E => E.OrganizeEvent)
                .WithMany(O => O.Events)
                .HasForeignKey(E => E.OrganizerId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}

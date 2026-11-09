using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_25_EF_Core.Models
{
    //POCO Class    "Plain Old C# Object"
    public class Event
    {

        public int EventId { get; set; }                   //PK

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }    //Optional

        public int AttendeesNumber { get; set; }

        //public int  AgeRestriction { get; set; }


        /// <summary>
        /// ////////////////////////////////// --RelationShip: One-to-Many [Partial - Total]-- //////////////////////////////////////////////
        /// </summary>
        /// 


        //Navigation Property
        public Organizer OrganizeEvent { get; set; } = null!;     //has reference navigation to "Organizer" cause must be managed by an organizer.

        //Foreign Key
        [ForeignKey(nameof (OrganizeEvent))]
        public int OrganizerId { get; set; }
    }
}



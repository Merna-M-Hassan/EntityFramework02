using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_25_EF_Core.Models
{
    //POCO Class
    public class OrganizerProfile
    {

        [Key]     //Mark ProfileId as the ONLY Primary Key
        public int ProfileId { get; set; }

        //[Key, ForeignKey(nameof(Organizer))]  
        public int OrganizerId { get; set; }            //This is JUST the Foreign Key

        /// <summary>
        /// ////////////////////////////////// --RelationShip: One-to-One [Total - Total]-- //////////////////////////////////////////////
        /// </summary>

        // **Navigation Property** //

        //Navigation property is mapping One-to-One [Total-Total]
        public Organizer Organizer { get; set; }



        /// <summary>
        /// /////////////////////////////////Other Properties///////////////////////////////////////
        /// </summary>

        public string Biography { get; set; }

        public string Link { get; set; }

        public string Logo { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_25_EF_Core.Models
{
    [Table("TableOrganizer")]   //To rename the table in DB.
    public class Organizer
    {
        [Key]     //Make PK
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]   //Turn off Identity Constraint. Must insert the vaue manually
        public int OrganizerId { get; set; }


        [MaxLength(100)]
        public required string OrganizerName { get; set; }


        [MaxLength (150)]
        public string? CompanyName { get; set; }


        /// <summary>
        /// //////////////////////////////////Extra Properties//////////////////////////////////////////////
        /// </summary>
        

        [Required]       //DB Apply
        [DataType(DataType.Password)]   //UI Hint: ****
        public string Password { get; set; }


        [Required]    //DB Apply
        [Phone]     //Application Validation
        [DataType(DataType.PhoneNumber)]   //UI Hint
        public string Phone { get; set; }


        //[Column(TypeName = "decimal(10, 3)")]   //DB Apply
        [Precision(10, 3)]    //DB Apply. More specific for decimals.
        public decimal Budget { get; set; }



        /// <summary>
        /// ////////////////////////////////// --RelationShip: One-to-One [Total - Total]-- //////////////////////////////////////////////
        /// </summary>
        /// 

        //Navigation property is mapping One-to-One [Total-Total]
        public OrganizerProfile OrProfile { get; set; } = null!;


        /// <summary>
        /// ////////////////////////////////// --RelationShip: One-to-Many [Partial - Total]-- //////////////////////////////////////////////
        /// </summary>
        /// 

        //Navigation property : Collection Proprty; cause organizer can manage many events
        public ICollection<Event> Events { get; set; } 
    }
}

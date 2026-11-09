using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_25_EF_Core.Models
{
    public class Attendees
    {
        [Key]     //Make PK
        //Add Identity Constraint [1,1]
        public int EventAttendeeId { get; set; }

        //[Required]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 & 150")]
        //[Length(3, 150)]    //Application Validation
        [MaxLength(150, ErrorMessage = "Name cannot exceed 150")]     //MaxLength: is Database Apply, applied in DB
        //ErrorMeassage: is Application Validation  --> when an object is created from this class, and the error is occurred (more than 150),
        //the error displays or occurs in the application. Befor excuted the save operations and add object as a row.
        [MinLength(3, ErrorMessage = "Name at least 3 chars")]        //MinLength() & ErrorMessage: ApplicationValidation. 
        //Its configurations will not impact on DB. Befor inserting this in DB as a row, insert data in c# application, will prevent saving if the error occurs (less than 3 chars). 
        public string FullName { get; set; }



        [MaxLength(200)]
        [Required(ErrorMessage = "Email is required")]       //Required: DB Apply.     //ErrorMessage: Application Validation.
        [EmailAddress]         //Check in application the Email property, when an object is created from the class, is in email format (has @ and domain).   //Application Validation
        [DataType(DataType.EmailAddress)]    //UI Hint: hint to the UI in text box (validate for @ and has domain).  To display email address value  to frontend.
        public string Email { get; set; }



        [MaxLength(150)]
        public string Street { get; set; }


        [MaxLength(100)]
        public string City { get; set; }


        [MaxLength(100)]
        public string Country { get; set; }

        [NotMapped]   //This explicitly tells Entity Framework Core not to generate a column for FullAddress in the SQL database.
        public string FullAddress => $"{Street}, {City}, {Country} "; 


        [Column(name: "ZipCode", TypeName = "varchar")]    //Cloumn(name: "tbName" )   --> Change the table name in DB.
        //Column(TypeName= "datatype of column")  --> Controls the column data type in DB.
        [MaxLength(20)]   //MaxLength: is DB Apply.
        public string PostalCode { get; set; }


        public BadgeTier Tier { get; set; }

        /// <summary>
        /// //////////////////////////////////Extra Properties//////////////////////////////////////////////
        /// </summary>
        

        [Range(18, 50)]   //Application Validation
        //[AllowedValues(24, 26, 28, 30)]   //The allowed number/s from (18) to (50).    //Applicaation Validation.
        //[DeniedValues(27, 29, 31)]     //The inallowed numbers.   //Application Validation
        public int AttendeeAge { get; set; }


        [NotMapped]    //Just a property in application. Not mapped in DB as a table.    //DB Apply
        //Not impact in Migration
        public string UserName => Email.Split('@')[0];   //Computed depends on Email. 
                                                         //Split from @ sign and take the before @ --> [0].
                                                         //Take after split sign --> [1].



        /// <summary>
        /// ////////////////////////////////// --RelationShip: One-to-One-- //////////////////////////////////////////////
        /// </summary>


         
        #region Manage Relationship [Attendee - Badge]  [One-One]  [O-T]

        // **Navigation Property ** //
        public Badge? HoldedBadge { get; set; }      

        //Attendee may holds or may not.    --> So, put ?; because it is optional.


        //[InverseProperty(nameof(Badge.AssignedBadge))]   //If there is an existance for more than one relationship between two entities.


        #endregion
    }
}

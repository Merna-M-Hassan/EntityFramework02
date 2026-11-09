using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_25_EF_Core.Models
{

    public enum BadgeTier
    {
        Standard, 
        VIP
    }

    public class Badge
    {
        public int BadgeNumber { get; set; }

        public String IssueDate { get; set; }

        public BadgeTier Tier { get; set; }


        /// <summary>
        /// ////////////////////////////////// --RelationShip: One-to-One-- //////////////////////////////////////////////
        /// </summary>



        #region Manage Relationship [Attendee - Badge]  [One-One]  [O-T]

        // **Navigation Property with FK** //

        // --Navigation Property
        public Attendees Attendee { get; set; } = null!;     //Must be required.
         // cannot be ?; because it is a total participation.

        // --Foreign Key
        //[ForeignKey(nameof(Attendee))]
        public int BadgeAttendeeId { get; set; }

        //How ORM/EF Core understands the property is FK?
        //  --Convention:  NavigationPropertyName+Id. (AttendeeId)
        //  OR  --Convention: NavigationPropertyName+PKNameInPrincipleTable. (AttendeeEventAttendeeId)
        //  OR --Convention: EntityNameOfPrinciple+Id. (AttendeesId)
        //  OR --Convention: EntityNameOfPrinciple+PrincipleEntityPK. (AttendeesEventAttendeeId)


        //[InverseProperty(nameof(Attendees.AssignedBadge))]   //If there is an existance for more than one relationship between two entities.
        //Each property has its inverse property in the other class.

        #endregion
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITP245_Fall2024_GraysonModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class DimDate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DimDate()
        {
            this.Activities = new HashSet<Activity>();
            this.Tasks = new HashSet<Task>();
            this.Tasks1 = new HashSet<Task>();
        }
    
        public int DateKey { get; set; }
        public System.DateTime CalendarDate { get; set; }
        public int SemesterWeekNumber { get; set; }
        public int DayOfWeek { get; set; }
        public int PigNumber { get; set; }
        public int SprintNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks1 { get; set; }
    }
}

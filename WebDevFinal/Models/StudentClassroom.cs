//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDevFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentClassroom
    {
        public int StudentPersonal_idStudent { get; set; }
        public string month_2 { get; set; }
        public Nullable<int> understanding_verbal_instruction { get; set; }
        public Nullable<int> classroom_assignment_completion { get; set; }
        public Nullable<int> organizational_skills { get; set; }
        public Nullable<int> getting_hw_to_and_from_school { get; set; }
        public Nullable<int> hw_completion { get; set; }
        public Nullable<int> relationship_with_peers { get; set; }
        public Nullable<int> following_directions { get; set; }
        public Nullable<int> disrupting_class { get; set; }
        public Nullable<int> verbal_participation_in_class { get; set; }
    
        public virtual StudentPersonal StudentPersonal { get; set; }
    }
}

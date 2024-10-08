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
    
    public partial class Game
    {
        public int GameId { get; set; }
        public int FieldId { get; set; }
        public System.DateTime DateTime { get; set; }
        public int HomeTeamID { get; set; }
        public int VisitorTeamID { get; set; }
        public Status StatusId { get; set; }
        public int LastModifiedById { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public Nullable<int> HomeScore { get; set; }
        public Nullable<int> VisitorScore { get; set; }
        public string Comment { get; set; }
    
        public virtual Field Field { get; set; }
        public virtual Team Team { get; set; }
        public virtual Team Team1 { get; set; }
        public virtual Player Player { get; set; }
    }
}

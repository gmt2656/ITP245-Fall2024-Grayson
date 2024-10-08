using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITP245_Fall2024_GraysonModel
{
    public interface ISports
    {
        string Name { get; }
    }

    // Player Class with Metadata
    [MetadataType(typeof(Player.PlayerMetadata))]
    public partial class Player : ISports
    {
        public string Name => $"{FirstName} {LastName}"; // Implementing the Name property

        private sealed class PlayerMetadata
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }
        }
    }

    // Team Class with Metadata
    [MetadataType(typeof(Team.TeamMetadata))]
    public partial class Team : ISports
    {
        private sealed class TeamMetadata
        {
            [Display(Name = "Team")]
            public string Name { get; set; }

            [Display(Name = "Division")]
            public int DivisionId { get; set; }

            [Display(Name = "Manager")]
            public int ManagerId { get; set; }

            [Display(Name = "Asst Manager")]
            public int AssistantManagerId { get; set; }

            [Display(Name = "Short Name")]
            public string ShortName { get; set; }
        }
    }

    // Field Class with Metadata
    [MetadataType(typeof(Field.FieldMetadata))]
    public partial class Field : ISports
    {
        private sealed class FieldMetadata
        {
            [Display(Name = "Field")]
            public string Name { get; set; }

            [StringLength(25)]
            public string Color { get; set; }
        }
    }

    // Game Class with Metadata
    [MetadataType(typeof(Game.GameMetadata))]
    public partial class Game
    {
        public string GameDate => DateTime.ToString("MMMM d, yyyy"); // e.g., August 6, 2024
        public string GameTime => DateTime.ToString("h:mm tt"); // e.g., 6:00 PM

        public string LastModifiedDate => LastModified?.ToString("MMMM d, yyyy") ?? "N/A";
        public string LastModifiedTime => LastModified?.ToString("h:mm tt") ?? "N/A";

        public string Winner
        {
            get
            {
                if (HomeScore.HasValue && VisitorScore.HasValue)
                {
                    if (HomeScore > VisitorScore)
                        return Team?.Name;
                    else if (VisitorScore > HomeScore)
                        return Team1?.Name;
                    else if (HomeScore == VisitorScore)
                        return "TIE";
                    else
                        return null; // Shouldn't reach here
                }
                else
                {
                    return null; // Game has not been played
                }
            }
        }

        private sealed class GameMetadata
        {
            [Display(Name = "Field")]
            public int FieldId { get; set; }

            [Display(Name = "Game Date")]
            public string GameDate { get; set; }

            [Display(Name = "Game Time")]
            public string GameTime { get; set; }

            [Display(Name = "Home Team")]
            public int HomeTeamID { get; set; }

            [Display(Name = "Visitor Team")]
            public int VisitorTeamID { get; set; }

            [Display(Name = "Home Score")]
            public int? HomeScore { get; set; }

            [Display(Name = "Visitor Score")]
            public int? VisitorScore { get; set; }

            [Display(Name = "Status")]
            public int StatusId { get; set; }

            [Display(Name = "Comment")]
            [StringLength(100)]
            public string Comment { get; set; }

            [Display(Name = "Last Modified Date")]
            public string LastModifiedDate { get; set; }

            [Display(Name = "Last Modified Time")]
            public string LastModifiedTime { get; set; }
        }
    }
}

﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.Tournament
{
    public class Tournament
    {
        [Key]
        [Required]
        [DisplayName("Tournament Id#")]
        public int TournamentId { get; set; }
        [DisplayName("Tournament Name")]
        public string TournamentName { get; set; }
    }
}

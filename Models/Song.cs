using System;
using System.ComponentModel.DataAnnotations;

namespace PlaylistApp.Models
{
	public class Song
	{
		[Key]
        [Required]
		public int SongId { get; set; }

		[Required]
		public string Title { get; set; }

        [Required]
        public string Artist { get; set; }

        [Required]
        public string Album { get; set; }

        [Required]
        public string Genre { get; set; }
    }
}


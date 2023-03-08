using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlaylistApp.Models
{
	public class Playlist
	{
		[Key]
		[Required]
		public int PlaylistId { get; set; }

		[Required]
		public int UserId { get; set; }
		public User User { get; set; }

		[Required]
		public List<Song> Songs { get; set; }
	}
}


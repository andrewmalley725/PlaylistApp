using System;
using System.ComponentModel.DataAnnotations;

namespace PlaylistApp.Models
{
	public class PlaylistSongs
	{

		[Key]
		[Required]
		public int id { get; set; }

		[Required]
		public int playListId { get; set; }
		public Playlist Playlist { get; set; }

		[Required]
        public int SongId { get; set; }
        public Song Song { get; set; }

    }
}


using DelugedClient.Common;
using System;

namespace DelugedClient.Model
{
	[Flags]
	public enum TorrentStatusKey : Int64
	{
		[StatusKeyName("active_time")]
		ActiveTime = 1,
		[StatusKeyName("all_time_download")]
		AllTimeDownload = 2,
		[StatusKeyName("compact")]
		Compact = 4,
		[StatusKeyName("distributed_copies")]
		DistributedCopies = 8,
		[StatusKeyName("download_payload_rate")]
		DownloadSpeed = 16,
		[StatusKeyName("upload_payload_rate")]
		UploadSpeed = 32,
		[StatusKeyName("file_priorities")]
		FilePriorities = 64,
		[StatusKeyName("files")]
		Files = 128,
		[StatusKeyName("hash")]
		Hash = 256,
		[StatusKeyName("is_auto_managed")]
		IsAutoManaged = 512,
		[StatusKeyName("is_finished")]
		IsFinished = 1024,
		[StatusKeyName("max_connections")]
		MaxConnections = 2048,
		[StatusKeyName("max_download_speed")]
		MaxDownloadSpeed = 4096,
		[StatusKeyName("max_upload_slots")]
		MaxUploadSlots = 8192,
		[StatusKeyName("max_upload_speed")]
		MaxUploadSpeed = 16384,
		[StatusKeyName("message")]
		Message = 32768,
		[StatusKeyName("move_on_completed_path")]
		MoveOnCompletedPath = 65536,
		[StatusKeyName("move_on_completed")]
		MoveOnCompleted = 131072,
		[StatusKeyName("next_announce")]
		NextAnnounce = 262144,
		[StatusKeyName("num_peers")]
		NumberOfPeers = 524288,
		[StatusKeyName("num_seeds")]
		NumberOfSeeds = 1048576,
		[StatusKeyName("paused")]
		Paused = 2097152,
		[StatusKeyName("prioritize_first_last")]
		PrioritizeFirstLast = 4194304,
		[StatusKeyName("progress")]
		Progress = 8388608,
		[StatusKeyName("remove_at_ratio")]
		RemoveAtRation = 16777216,
		[StatusKeyName("save_path")]
		SavePath = 33554432,
		[StatusKeyName("seeding_time")]
		SeedingTime = 67108864,
		[StatusKeyName("seed_rank")]
		SeedRank = 134217728,
		[StatusKeyName("state")]
		State = 268435456,
		[StatusKeyName("stop_at_ratio")]
		StopAtRatio = 536870912,
		[StatusKeyName("stop_ratio")]
		StopRatio = 1073741824,
		[StatusKeyName("time_added")]
		TimeAdded = 2147483648,
		[StatusKeyName("total_done")]
		TotalDone = 4294967296,
		[StatusKeyName("total_payload_download")]
		TotalDownloadSpeed = 8589934592,
		[StatusKeyName("total_payload_upload")]
		TotalUploadSpeed = 17179869184,
		[StatusKeyName("total_peers")]
		TotalPeers = 34359738368,
		[StatusKeyName("total_seeds")]
		TotalSeeds = 68719476736,
		[StatusKeyName("total_uploaded")]
		TotalUploaded = 137438953472,
		[StatusKeyName("total_wanted")]
		TotalWanted = 274877906944,
		[StatusKeyName("tracker")]
		Tracker = 549755813888,
		[StatusKeyName("trackers")]
		Trackers = 1099511627776,
		[StatusKeyName("comment")]
		Comment = 2199023255552,
		[StatusKeyName("eta")]
		ETA = 4398046511104,
		[StatusKeyName("file_progress")]
		FileProgress = 8796093022208,
		[StatusKeyName("is_seed")]
		IsSeed = 17592186044416,
		[StatusKeyName("name")]
		Name = 35184372088832,
		[StatusKeyName("num_files")]
		NumberOfFiles = 70368744177664,
		[StatusKeyName("num_pieces")]
		NumberOfPieces = 140737488355328,
		[StatusKeyName("queue")]
		QueuePosition = 281474976710656,
		[StatusKeyName("ratio")]
		Ratio = 562949953421312,
		[StatusKeyName("total_size")]
		TotalSize = 1125899906842624
	}
}

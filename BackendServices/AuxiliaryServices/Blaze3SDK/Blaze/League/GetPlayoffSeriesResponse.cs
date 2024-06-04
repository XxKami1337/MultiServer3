using Tdf;

namespace Blaze3SDK.Blaze.League
{
	[TdfStruct]
	public struct GetPlayoffSeriesResponse
	{

		[TdfMember("LGID")]
		public uint mLeagueId;

		[TdfMember("RNDS")]
		public List<PlayoffSeries> mPlayoffSeries;

	}
}

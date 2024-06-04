namespace Blaze3SDK.Blaze.GameManager
{
	public enum MatchmakingResult : int
	{
		SUCCESS_CREATED_GAME = 0,
		SUCCESS_JOINED_NEW_GAME = 1,
		SUCCESS_JOINED_EXISTING_GAME = 2,
		SESSION_TIMED_OUT = 3,
		SESSION_CANCELED = 4,
		SESSION_TERMINATED = 5,
		SESSION_ERROR_GAME_SETUP_FAILED = 6,
	}
}
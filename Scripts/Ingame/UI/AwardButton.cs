using GooglePlayGames;
using UnityEngine;

public class AwardButton : MonoBehaviour
{
    public void ShowLeaderBoardUI() => ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_ranking);
}
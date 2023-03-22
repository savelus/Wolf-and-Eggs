using TMPro;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{

    public class ScoreView : MonoBehaviour
    {
        public TMP_Text score;

        public void SetScore(int currentScore)
        {
            score.text = currentScore.ToString();
        }

    }
}
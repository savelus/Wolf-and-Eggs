using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGames.WolfAndEggs
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private ScoreView scoreView;
        public int currentScore { get; private set; }
        
        public void AddScore(int score)
        {
            currentScore += score;
            scoreView.SetScore(currentScore);
        }
        
    }
}
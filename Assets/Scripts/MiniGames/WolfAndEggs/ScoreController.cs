using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;

namespace MiniGames.WolfAndEggs
{
    public class ScoreController
    {
        private readonly ScoreView _scoreView;
        public int CurrentScore { get; private set; }

        public ScoreController(ScoreView scoreView)
        {
            _scoreView = scoreView;
        }
        public void AddScore(int score)
        {
            CurrentScore += score;
            _scoreView.SetScore(CurrentScore);
        }
        
    }
}
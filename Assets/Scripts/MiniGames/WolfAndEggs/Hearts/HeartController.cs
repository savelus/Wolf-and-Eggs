using System;

namespace MiniGames.WolfAndEggs.Hearts
{
    public class HeartController
    {
        private readonly HeartView _heartView;
        
        public HeartController(HeartView heartView)
        {
            _heartView = heartView;
        }
        
        public void RemoveLive()
        {
            foreach (var heart in _heartView.hearts)
            {
                if (heart.activeSelf)
                {
                    heart.SetActive(false);
                    break;
                }
            }

            if (!_heartView.hearts.Exists(x => x.activeSelf))
            {
                throw new Exception("Игра кончилась вместе с разрабом");
            }
        }

        public void RestoreLives()
        {
            foreach (var heart in _heartView.hearts)
            {
                heart.SetActive(true);
            }
        }
    }
}

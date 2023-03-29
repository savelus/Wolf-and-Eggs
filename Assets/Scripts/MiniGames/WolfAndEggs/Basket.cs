using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class Basket : MonoBehaviour
    {
        private GameController _gameController;

        [HideInInspector] public int BasketNumber;

        public void Initialize(GameController gameController, int number)
        {
            BasketNumber = number;
            _gameController = gameController;
        }
    }

    
}

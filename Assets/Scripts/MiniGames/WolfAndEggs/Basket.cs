using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MiniGames.WolfAndEggs;
using Unity.VisualScripting;
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

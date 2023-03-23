using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class HeartController : MonoBehaviour
    {
        public HeartView HeartView;

        public void RemoveLive()
        {
            foreach (var heart in HeartView.hearts)
            {
                if (heart.activeSelf)
                {
                    heart.SetActive(false);
                    break;
                }
            }

            if (!HeartView.hearts.Exists(x => x.activeSelf))
            {
                throw new Exception("Игра кончилась вместе с разрабом");
            }
        }

        public void RestoreLives()
        {
            foreach (var heart in HeartView.hearts)
            {
                heart.SetActive(true);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using MiniGames.WolfAndEggs;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class Basket : MonoBehaviour
    {
        private ScoreController _scoreController;


        public void Initialize(ScoreController scoreController)
        {
            _scoreController = scoreController;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Egg>())
            {
                Debug.Log(collision.gameObject.GetComponent<Rigidbody2D>().velocity);
                Debug.Log(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
                _scoreController.AddScore(collision.gameObject.GetComponent<Egg>().eggBonus);
                Destroy(collision.gameObject);
            }
        }

    }
}

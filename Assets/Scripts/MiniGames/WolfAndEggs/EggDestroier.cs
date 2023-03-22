using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class EggDestroier : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Egg>())
            {
                Destroy(collision.gameObject);
            }
        }
    }
}

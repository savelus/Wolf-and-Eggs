using Unity.VisualScripting;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class Egg : MonoBehaviour
    {
        public int eggBonus;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 2)
            {
                Debug.Log('h');
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                
            }
        }
        
    }
}
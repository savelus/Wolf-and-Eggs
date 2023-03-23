using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class Egg : MonoBehaviour
    {
        public int eggBonus;
        private GameController _gameController;
        [HideInInspector] public int RoostNumber;
        public void Initalizate(GameController gameController, int roostNumber)
        {
            _gameController = gameController;
            RoostNumber = roostNumber;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Basket basket = collision.gameObject.GetComponent<Basket>();
            if (basket == null) return;
            if (basket.BasketNumber == RoostNumber)
            {
                DOTween.Sequence()
                    .AppendCallback(() => ChangeEggColor(gameObject, Color.green))
                    .AppendCallback(AddBonus)
                    .AppendInterval(0.2f)
                    .AppendCallback(() => Destroy(gameObject));
            }
            else
            {
                DOTween.Sequence()
                    .AppendCallback(() => ChangeEggColor(gameObject, Color.red))
                    .AppendCallback(RemoveLive)
                    .AppendInterval(0.2f)
                    .AppendCallback(() => Destroy(gameObject));
            }
            /*
            if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) > 1)
            {
                Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
                //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.y);
                _gameController.HeartController.RemoveLive();
                DOTween.Sequence()
                    .AppendCallback(() => ChangeEggColor(gameObject, Color.red))
                    .AppendInterval(0.2f)
                    .AppendCallback(() => Destroy(gameObject));
            }
            
            else if (collision.gameObject.GetComponent<Basket>())
            {
                Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
                //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.y);
                
                _gameController.ScoreController.AddScore(eggBonus);
                DOTween.Sequence()
                    .AppendCallback(() => ChangeEggColor(gameObject, Color.green))
                    .AppendInterval(0.2f)
                    .AppendCallback(() => Destroy(gameObject));
                    
            }
            */
        }
        private void ChangeEggColor(GameObject egg, Color color)
        {
            egg.GetComponent<SpriteRenderer>().color = color;
        }

        private void AddBonus()
        {
            _gameController.ScoreController.AddScore(eggBonus);
        }

        private void RemoveLive()
        {
            _gameController.HeartController.RemoveLive();
        }
    }
}
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class Egg : MonoBehaviour
    {
        [HideInInspector] public bool isActive = true;
        [SerializeField] private Rigidbody2D Rigidbody;
        private Vector2 _velocity;
        private float _angularVelocity;
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
            if (collision.gameObject.GetComponent<EggDestroier>())
            {
                CreateSequenceAfterEggCollision(Color.red, 0.2f);
                _gameController.eggController.spawnedEggs.Remove(gameObject);
                RemoveLive();
            }
            Basket basket = collision.gameObject.GetComponent<Basket>();
            if (basket == null) return;
            if (basket.BasketNumber == RoostNumber)
            {
                CreateSequenceAfterEggCollision(Color.green, 0.2f);
                _gameController.eggController.spawnedEggs.Remove(gameObject);
            }
            else
            {
                CreateSequenceAfterEggCollision(Color.red, 0.2f);
                _gameController.eggController.spawnedEggs.Remove(gameObject);
                RemoveLive();
            }
        }

        private void CreateSequenceAfterEggCollision(Color color, float timeToDie)
        {
            DOTween.Sequence()
                .AppendCallback(() => ChangeEggColor(gameObject, color))
                .AppendCallback(DisableRigidbody)
                .AppendInterval(timeToDie)
                .AppendCallback(() => Destroy(gameObject));
        }
        private void ChangeEggColor(GameObject egg, Color color)
        {
            egg.GetComponent<SpriteRenderer>().color = color;
        }

        public void DisableRigidbody()
        {
            _velocity = Rigidbody.velocity;
            _angularVelocity = Rigidbody.angularVelocity;
            Rigidbody.Sleep();
        }
        
        public void EnableRigidbody()
        {
            Rigidbody.WakeUp();
            Rigidbody.velocity = _velocity;
            Rigidbody.angularVelocity = _angularVelocity;
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
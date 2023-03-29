using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs
{
    public class Egg : MonoBehaviour
    {
        [HideInInspector] public bool isActive = true;
        [SerializeField] private Rigidbody2D Rigidbody;
        private Vector2 _velocity;
        private float _angularVelocity;
        public int eggBonus;
        
        [HideInInspector] public int RoostNumber;
        
        [Inject] private GameController _gameController;
        [Inject] private ScoreController _scoreController;
        [Inject] private HeartController _heartController;
        [Inject] private EggController _eggController;
        public void Initalizate(int roostNumber)
        {
            RoostNumber = roostNumber;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<EggDestroier>())
            {
                CreateSequenceAfterEggCollision(Color.red, 0.2f);
                _eggController.spawnedEggs.Remove(gameObject);
                
                RemoveLive();
            }
            Basket basket = collision.gameObject.GetComponent<Basket>();
            if (basket == null) return;
            if (basket.BasketNumber == RoostNumber)
            {
                AddBonus();
                CreateSequenceAfterEggCollision(Color.green, 0.2f);
                _eggController.spawnedEggs.Remove(gameObject);
                
            }
            else
            {
                CreateSequenceAfterEggCollision(Color.red, 0.2f);
                _eggController.spawnedEggs.Remove(gameObject);
                
                RemoveLive();
            }
        }

        private void CreateSequenceAfterEggCollision(Color color, float timeToDie)
        {
            DOTween.Sequence()
                .AppendCallback(() => ChangeEggColor(gameObject, color))
                .AppendCallback(DisableRigidbody)
                .AppendInterval(timeToDie)
                .AppendCallback(Deactive);
            //.AppendCallback(RemoveFromScreen);
            //.AppendCallback(() => Destroy(gameObject));
        }
        private void ChangeEggColor(GameObject egg, Color color)
        {
            egg.GetComponent<SpriteRenderer>().color = color;
        }

        // private void RemoveFromScreen()
        // {
        //     _gameController.eggController.EggPool.Release(gameObject);
        // }
        public void DisableRigidbody()
        {
            _velocity = Rigidbody.velocity;
            _angularVelocity = Rigidbody.angularVelocity;
            Rigidbody.Sleep();
        }

        private void Deactive()
        {
            
            gameObject.transform.position = new Vector2(10, 10);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            _eggController.EggPool.Release(gameObject);
        }
        public void EnableRigidbody()
        {
            Rigidbody.WakeUp();
            Rigidbody.velocity = _velocity;
            Rigidbody.angularVelocity = _angularVelocity;
        }
        private void AddBonus()
        {
            _scoreController.AddScore(eggBonus);
        }

        private void RemoveLive()
        {
            _heartController.RemoveLive();
        }
    }
}
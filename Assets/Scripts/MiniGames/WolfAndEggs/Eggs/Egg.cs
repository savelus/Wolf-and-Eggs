﻿using DG.Tweening;
using MiniGames.WolfAndEggs.Hearts;
using MiniGames.WolfAndEggs.Score;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs.Eggs
{
    public class Egg : MonoBehaviour
    {
        [HideInInspector] public bool isActive = true;
        [SerializeField] private Rigidbody2D Rigidbody;
        private Vector2 _velocity;
        private float _angularVelocity;
        
        
        [HideInInspector] public int RoostNumber;
        
        [Inject] private ScoreController _scoreController;
        [Inject] private HeartController _heartController;
        [Inject] private EggController _eggController;

        [Inject] private EggSetting _eggSetting;
        [Inject] private EggSpawnTimeSettings _eggSpawnTimeSettings;
        public void Initalizate(int roostNumber)
        {
            RoostNumber = roostNumber;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<EggDestroier>())
            {
                CreateSequenceAfterEggCollision(_eggSetting.failedColor,  _eggSpawnTimeSettings.TimeToDelete);
                _eggController.SpawnedEggs.Remove(gameObject);
                
                RemoveLive();
            }
            var basket = collision.gameObject.GetComponent<Basket>();
            if (basket == null) return;
            if (basket.BasketNumber == RoostNumber)
            {
                AddBonus();
                CreateSequenceAfterEggCollision(_eggSetting.successColor, _eggSpawnTimeSettings.TimeToDelete);
                _eggController.SpawnedEggs.Remove(gameObject);
                
            }
            else
            {
                CreateSequenceAfterEggCollision(_eggSetting.failedColor,  _eggSpawnTimeSettings.TimeToDelete);
                _eggController.SpawnedEggs.Remove(gameObject);
                
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
            _scoreController.AddScore(_eggSetting.bonus);
        }

        private void RemoveLive()
        {
            _heartController.RemoveLive();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiniGames.WolfAndEggs;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace MiniGames.WolfAndEggs
{
    public class EggSpawner : MonoBehaviour
    {
        private RoostSetup _roostSetup;
        [SerializeField] private GameObject egg;
        private List<GameObject> _roosts = new List<GameObject>();
        private Random _rnd = new Random();

        [SerializeField] private float startMiddleTime = 2f;
        private float _currentMiddleTime;
        private float _allSpawnedTime = 0;

        private GameController _gameController;
        public void Initializate(List<GameObject> roosts, GameController gameController)
        {
            _currentMiddleTime = startMiddleTime;
            _gameController = gameController;
            foreach (var roost in roosts)
            {
                _roosts.Add(roost);
            }
            
        }

        public void SpawnEgg()
        {
            var numberRoost = _rnd.Next(_roosts.Count);
            Vector3 spawnEggPosition =
                _roosts[numberRoost].gameObject.transform.Find("Spawner").transform.position; //кажется гавно
            
            var spawnedEgg = Instantiate(egg, spawnEggPosition, quaternion.identity);
            spawnedEgg.GetComponent<Egg>().Initalizate(_gameController, numberRoost);
            Invoke(nameof(SpawnEgg), GetDeltaTime());
        }

        private float GetDeltaTime()
        {
            if (_currentMiddleTime > 0.4 && _allSpawnedTime / 100 > startMiddleTime - _currentMiddleTime)
            {
                _currentMiddleTime -= 0.1f;
            }

            var delta = _currentMiddleTime - 0.3f + (float)_rnd.NextDouble() * 0.6f;
            _allSpawnedTime += delta;
            //Debug.Log(currentMiddleTime + " || " + delta + " || " + _allSpawnedTime);
            
            return delta;
            
        }
    }
}

using System;
using System.Collections.Generic;
using Core.Pools.Base;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace MiniGames.WolfAndEggs.Eggs
{

    public class EggController : MonoBehaviour
    {
        enum GameState
        {
            NotStarted, Started, Paused, Resumed, Ended 
        }
        
        public BaseGamePool EggPool;
        private GameState _currentGameState = GameState.NotStarted; 
        // private RoostSetup _roostSetup;
        //[SerializeField] private GameObject egg;
        private List<GameObject> _roosts = new List<GameObject>();
        private Random _rnd = new Random();

        [SerializeField] private float startMiddleTime;
        private float _currentMiddleTime;
        private float _allSpawnedTime = 0;

        [Inject] private GameController _gameController;
        
        [HideInInspector] public List<GameObject> spawnedEggs;

        private bool _eggSpawnEnable;

        [Inject]private readonly EggSetting _eggSetting;
        public void Initializate(List<GameObject> roosts)
        {
            var eggParent = new GameObject();
            eggParent.name = nameof(eggParent);
            startMiddleTime = _eggSetting.StartDeltaTime;
            
            EggPool = new BaseGamePool(_eggSetting.EggPrefab);
            EggPool.SetParentContainer(eggParent.transform);
            EggPool.InitialFill();
            
            spawnedEggs = new List<GameObject>();
            _currentMiddleTime = startMiddleTime;
            foreach (var roost in roosts)
            {
                _roosts.Add(roost);
            }
            
        }

        public void SpawnEgg()
        {

            if (_currentGameState is GameState.Ended or GameState.Paused)
            {
                _eggSpawnEnable = false;
                return;
            }
            var numberRoost = _rnd.Next(_roosts.Count);
            Vector3 spawnEggPosition =
                _roosts[numberRoost].gameObject.transform.Find("Spawner").transform.position; //кажется гавно

            var spawnedEgg = EggPool.Take<Egg>();
            spawnedEgg.Initalizate(numberRoost);
            
            spawnedEgg.gameObject.transform.position = spawnEggPosition;
            spawnedEggs.Add(spawnedEgg.gameObject);
            _eggSpawnEnable = true;
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

        public void SwitchGameState()
        {
            switch (_currentGameState)
            {
                case GameState.NotStarted:
                {
                    _currentGameState = GameState.Started;
                    _gameController.buttonText.text = "Pause";  
                    SpawnEgg();
                    break;
                }
                case GameState.Started:
                {
                    _currentGameState = GameState.Paused;
                    _gameController.buttonText.text = "Resume"; 
                    
                    StopEggs();
                    break;
                }
                case GameState.Paused:
                    _currentGameState = GameState.Resumed;
                    _gameController.buttonText.text = "Pause";
                    RunEggs();
                    break;
                case GameState.Resumed:
                    _currentGameState = GameState.Paused;
                    _gameController.buttonText.text = "Resume"; 
                    StopEggs();
                    break;
                case GameState.Ended:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void StopEggs()
        {
            foreach (var egg in spawnedEggs)
            {
                egg.GetComponent<Egg>().DisableRigidbody();
            }
        }

        private void RunEggs()
        {
            foreach (var egg in spawnedEggs)
            {
                egg.GetComponent<Egg>().EnableRigidbody();
            }

            if (!_eggSpawnEnable)
            {
                SpawnEgg();
            }
        }
    }
}

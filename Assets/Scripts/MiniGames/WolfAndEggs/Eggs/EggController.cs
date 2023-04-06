using System;
using System.Collections.Generic;
using Core.Pools.Base;
using DG.Tweening;
using PlasticPipe.PlasticProtocol.Client.Proxies;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace MiniGames.WolfAndEggs.Eggs
{

    public class EggController
    {
        private enum GameState
        {
            NotStarted, Started, Paused, Resumed, Ended 
        }
        
        public BaseGamePool EggPool;
        private GameState _currentGameState = GameState.NotStarted; 
        // private roostSetup _roostSetup;
        //[SerializeField] private GameObject egg;
        private readonly List<GameObject> _roosts = new();
        private readonly Random _rnd = new();

        [SerializeField] private float _startMiddleTime;
        private float _currentMiddleTime;
        private float _allSpawnedTime;
        [Inject] private SceneSettings _sceneSettings;
        [HideInInspector] public List<GameObject> SpawnedEggs;

        private bool _eggSpawnEnable;

        [Inject]private readonly EggSetting _eggSetting;
        [Inject] private readonly EggSpawnTimeSettings _eggSpawnTimeSettings;
        public void Initialize(List<GameObject> roosts)
        {
            var eggParent = new GameObject();
            eggParent.name = nameof(eggParent);
            _startMiddleTime = _eggSpawnTimeSettings.StartMiddleTime;
            
            EggPool = new BaseGamePool(_eggSetting.eggPrefab);
            EggPool.SetParentContainer(eggParent.transform);
            EggPool.InitialFill();
            
            SpawnedEggs = new List<GameObject>();
            _currentMiddleTime = _startMiddleTime;
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
            SpawnedEggs.Add(spawnedEgg.gameObject);
            _eggSpawnEnable = true;
            DOTween.Sequence()
                .AppendInterval(GetDeltaTime())
                .AppendCallback(SpawnEgg);

        }

        private float GetDeltaTime()
        {
            if (_currentMiddleTime > _eggSpawnTimeSettings.minimalMiddleTime 
                && _allSpawnedTime / 100 > _startMiddleTime - _currentMiddleTime)
            {
                _currentMiddleTime -= _eggSpawnTimeSettings.deltaMiddleTime;
            }

            var delta = _currentMiddleTime - _eggSpawnTimeSettings.spreadTime 
                        + (float)_rnd.NextDouble() * _eggSpawnTimeSettings.spreadTime * 2;
            _allSpawnedTime += delta;
           
            return delta;
            
        }
        
        public void SwitchGameState()
        {
            switch (_currentGameState)
            {
                case GameState.NotStarted:
                {
                    _currentGameState = GameState.Started;
                    _sceneSettings.buttonText.text = "Pause";  
                    SpawnEgg();
                    break;
                }
                case GameState.Started:
                case GameState.Resumed:
                {
                    _currentGameState = GameState.Paused;
                    _sceneSettings.buttonText.text = "Resume"; 
                    
                    StopEggs();
                    break;
                }
                case GameState.Paused:
                    _currentGameState = GameState.Resumed;
                    _sceneSettings.buttonText.text = "Pause";
                    RunEggs();
                    break;
                case GameState.Ended:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void StopEggs()
        {
            foreach (var egg in SpawnedEggs)
            {
                egg.GetComponent<Egg>().DisableRigidbody();
            }
        }

        private void RunEggs()
        {
            foreach (var egg in SpawnedEggs)
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

using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGames.WolfAndEggs.Eggs
{
    [Serializable]
    public class EggSpawnTimeSettings
    {
        public float StartMiddleTime;
        
        public float TimeToDelete;
        
        public float minimalMiddleTime;

        [Range(0, 1f)] public float deltaMiddleTime;

        [Range(0, 1f)] public float spreadTime;
        
        

        
    }
}
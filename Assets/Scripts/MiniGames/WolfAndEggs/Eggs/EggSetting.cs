using System;
using UnityEngine;

namespace MiniGames.WolfAndEggs.Eggs
{
    [Serializable]
    public class EggSetting
    {
        public GameObject EggPrefab;

        public Color SuccessColor;

        public Color FailedColor;

        public float StartDeltaTime;

        public float TimeToDie;
    }
}
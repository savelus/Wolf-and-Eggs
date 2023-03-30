using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGames.WolfAndEggs.Eggs
{
    [Serializable]
    public class EggSetting
    {
        public GameObject eggPrefab;

        public Color successColor;

        public Color failedColor;

        public int bonus;
    }
}
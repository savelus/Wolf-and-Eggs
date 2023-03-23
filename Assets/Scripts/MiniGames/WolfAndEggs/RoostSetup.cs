using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class RoostSetup : MonoBehaviour
    {
        public GameObject Roost;
        public int CountRoostOnSide;
        private GameController _gameController;
        [HideInInspector] public List<GameObject> Roosts;
        
        

        public List<GameObject> Initializate(GameController gameController)
        {
            _gameController = gameController;
            Vector2 screenSize = Camera.main.ScreenToWorldPoint(
                new Vector2(Screen.width, Screen.height));

            float planedHeight = Roost.transform.localScale.y * CountRoostOnSide;
            
            if (planedHeight == 0 || planedHeight > screenSize.y)
                throw new Exception("что то не то с количеством насестов");
            float roostHeight = Roost.transform.localScale.y;
            float roostWidth = Roost.transform.localScale.x;
            float spawnPositionHeight = - planedHeight / 2;
            
            
            
            
            for (int i = 0; i < CountRoostOnSide; i++)
            {
                Vector3 spawnPosition = new Vector3(screenSize.x - roostWidth / 2, spawnPositionHeight, 0);
                var rightRoost = Instantiate(Roost, spawnPosition, Quaternion.identity);
                rightRoost.GetComponent<Roost>().Basket.GetComponent<Basket>().Initialize(_gameController, 2 * i);
                rightRoost.GetComponent<Roost>().Number = 2 * i;
                Roosts.Add(rightRoost);
                
                spawnPosition = new Vector3(- screenSize.x + roostWidth / 2, spawnPositionHeight, 0);
                var leftRoost = Instantiate(Roost, spawnPosition, Quaternion.identity);
                leftRoost.gameObject.transform.localScale = new Vector3(leftRoost.transform.localScale.x * -1,
                    leftRoost.transform.localScale.y, leftRoost.transform.localScale.z);
                Roosts.Add(leftRoost);
                leftRoost.GetComponent<Roost>().Number = 2 * i + 1;
                spawnPositionHeight += roostHeight + 0.1f;
                leftRoost.GetComponent<Roost>().Basket.GetComponent<Basket>().Initialize(_gameController, 2 * i + 1);
                
            }

            return Roosts;
        }

        public void ChangeBasket(GameObject activeRoost)
        {
            foreach (var roost in Roosts)
            {
                
                if (roost != activeRoost)
                {
                    roost.GetComponent<Roost>().DeactivateBasket();
                }
                else
                {
                    roost.GetComponent<Roost>().ActivateBasket();
                }
            }
        }
    }
}

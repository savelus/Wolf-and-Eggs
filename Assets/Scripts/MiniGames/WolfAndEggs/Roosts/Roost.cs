using UnityEngine;

namespace MiniGames.WolfAndEggs.Roosts
{
    public class Roost : MonoBehaviour
    {
        [HideInInspector] 
        public bool isVisibleBusket = false;
        public GameObject Basket;
        public GameObject Spawner;
        public GameObject TapZone;

        [HideInInspector] public int Number;
        //public Action<GameObject> OnChangeBasket;
        private void Awake()
        {
            Basket.SetActive(isVisibleBusket);
        }

        

        public void DeactivateBasket()
        {
            isVisibleBusket = false;
            Basket.SetActive(false);
        }
        
        public void ActivateBasket()
        {
            isVisibleBusket = true;
            Basket.SetActive(true);
        }

        
    }
    
    
}

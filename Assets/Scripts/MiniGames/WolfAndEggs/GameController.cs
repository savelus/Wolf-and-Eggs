using MiniGames.WolfAndEggs.Eggs;
using MiniGames.WolfAndEggs.Roosts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MiniGames.WolfAndEggs
{
    public class GameController : MonoBehaviour
    {
        [Inject] private RoostSetup _roostSetup;
        [Inject] private EggController _eggController;
    
        public Button SwitchStateButton;
        [SerializeField] public TMP_Text buttonText;
        void Start()
        {
            var roosts = _roostSetup.Initializate();
            _eggController.Initializate(roosts);
            SwitchStateButton.onClick.AddListener(_eggController.SwitchGameState);
            buttonText = SwitchStateButton.GetComponentInChildren<TMP_Text>(true);
            buttonText.text = "Play";
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouse = Input.mousePosition;
                //Debug.Log(mouse.x + " " + mouse.y);
            
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouse), Vector2.zero);
                if (hit)
                {
                    _roostSetup.ChangeBasket(hit.collider.gameObject);
                }
            }
        }
    }
}

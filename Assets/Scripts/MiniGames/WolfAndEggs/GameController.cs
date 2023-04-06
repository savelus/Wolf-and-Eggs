using MiniGames.WolfAndEggs.Eggs;
using MiniGames.WolfAndEggs.Roosts;
using TMPro;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs
{
    public class GameController : IInitializable,
                                  ITickable
    {
        [Inject] private RoostSetup _roostSetup;
        [Inject] private EggController _eggController;

        [Inject] private SceneSettings _sceneSettings;
        
        public void Initialize()
        {
            var roosts = _roostSetup.Initializate();
            _eggController.Initialize(roosts);
            _sceneSettings.switchStateButton.onClick.AddListener(_eggController.SwitchGameState);
            _sceneSettings.buttonText = _sceneSettings.switchStateButton.GetComponentInChildren<TMP_Text>(true);
            _sceneSettings.buttonText.text = "Play";
        }

        public void Tick()
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

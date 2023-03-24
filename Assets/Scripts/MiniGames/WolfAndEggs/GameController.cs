using System;
using System.Collections;
using System.Collections.Generic;
using MiniGames.WolfAndEggs;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public RoostSetup roostSetup;
    [FormerlySerializedAs("eggSpawner")] public EggController eggController;

    public ScoreController ScoreController;
    [HideInInspector] public int currentScore;

    public HeartController HeartController;

    public Button SwitchStateButton;
    public TMP_Text buttonText;
    void Start()
    {
        var roosts = roostSetup.Initializate(this);
        eggController.Initializate(roosts, this);
        SwitchStateButton.onClick.AddListener(eggController.SwitchGameState);
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
                roostSetup.ChangeBasket(hit.collider.gameObject);
            }
        }
    }
}

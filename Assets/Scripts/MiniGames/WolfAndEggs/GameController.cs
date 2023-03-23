using System;
using System.Collections;
using System.Collections.Generic;
using MiniGames.WolfAndEggs;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    public RoostSetup roostSetup;
    public EggSpawner eggSpawner;

    public ScoreController ScoreController;
    [HideInInspector] public int currentScore;

    public HeartController HeartController;
    void Start()
    {
        var roosts = roostSetup.Initializate(this);
        eggSpawner.Initializate(roosts, this);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager: MonoBehaviour
{
    public TextMeshPro timer;
    public TextMeshPro player1Lives;
    public TextMeshPro player2Lives;
    private DateTime initialTime;
    // Start is called before the first frame update
    void Start()
    {
        initialTime = System.DateTime.Now;
    }


    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if(timer != null) 
            timer.SetText((System.DateTime.Now - initialTime).ToString());//.Substring(3, 8));
                                                                      //print(textmeshPro.text);
    }

    public void UpdatePlayerLives(PlayerController player)
    {
        if (!(player1Lives && player2Lives)) return;

        if (player.stats.playerIndex == PlayerController.Players.ONE)
            player1Lives.text = player.Lives.ToString();
        else if (player.stats.playerIndex == PlayerController.Players.TWO)
            player2Lives.text = player.Lives.ToString();
        else
            throw new Exception("There are more players than ui can handle!");
    }
}

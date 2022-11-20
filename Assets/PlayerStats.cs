using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "", order = 1)]
public class PlayerStats : ScriptableObject
{
    public float speed;
    public float attack;
    public float defense;
    public Players playerIndex;
    // public float color (cand nu imi e lene o sa pun si dinastea cosmetice, particule aleaalea)
}

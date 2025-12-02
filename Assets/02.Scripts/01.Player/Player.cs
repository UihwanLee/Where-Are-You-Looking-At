using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStat Stat { get; private set; }
    public PlayerController Controller {  get; private set; }
    public PlayerCondition Condition {  get; private set; }


    private void Awake()
    {
        Stat = GetComponent<PlayerStat>();
        Controller = GetComponent<PlayerController>();
        Condition = GetComponent<PlayerCondition>();
    }
}

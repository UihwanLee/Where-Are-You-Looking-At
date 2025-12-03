using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private GameManager() { }

    #region 프로퍼티

    public Player Player { get { return player; } }

    #endregion

    public void StopPlayMode()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}

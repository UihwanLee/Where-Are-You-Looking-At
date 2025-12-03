using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClearWindowManager : MonoBehaviour
{
    [Header("Stat 정보")]
    [SerializeField] private PlayerStatUI playerStatUI;

    [Header("리스트 정보")]
    [SerializeField] private List<Attribute> playerAttributes = new List<Attribute>();
    [SerializeField] private List<Attribute> advisorAttributes = new List<Attribute>();

    public event Action<List<Attribute>> OnPlayerStatChange;    // 플레이어 Stat Change 이벤트
    public event Action<List<Attribute>> OnAdvisorStatChange;   // Advisor Stat Chnage 이벤트

    private void Reset()
    {
        playerStatUI = transform.FindChild<PlayerStatUI>("PlayerStatWindow");
    }

    private void Start()
    {
        // Player Stat Attribute를 가져와서 LocalIndex로 정렬하여 리스트화
        Player player = GameManager.Instance.Player;
        playerAttributes = GameManager.Instance.Player.Stat.AttributeDict.Values.OrderBy(attribute => attribute.LocalIndex).ToList();

        UpdateStatUI();

        playerStatUI.SetWindow(false);
    }

    private void UpdateStatUI()
    {
        // 모든 StatUI 갱신
        if (OnPlayerStatChange == null) { Debug.Log("등록된 이벤트가 없음!"); return; }
        OnPlayerStatChange.Invoke(playerAttributes);
    }

    public void OnOpenWindow()
    {
        playerAttributes[0].SetValue(200);
        OnPlayerStatChange.Invoke(playerAttributes);
        //playerStatUI.gameObject.SetActive(true);
    }
}

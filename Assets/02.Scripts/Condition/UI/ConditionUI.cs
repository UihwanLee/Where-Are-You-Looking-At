using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image expBar;
    [SerializeField] private Text goldText;

    //private Player player;

    private void Reset()
    {
        Initialize();
    }

    private void Initialize()
    {
        hpBar = transform.FindChild<Image>("HpBar");
        expBar = transform.FindChild<Image>("ExpBar");
        goldText = transform.FindChild<Text>("GoldText");
    }

    private void OnEnable()
    {
        // 이벤트 등록
    }

    private void OnDisable()
    {
        // 이벤트 종료
    }

    private void Start()
    {
        // Player Instance로 할당
    }

    public void UpdateUI()
    {
        // 플레이어 Condtion에 따른 UI 업데이트
    }
}

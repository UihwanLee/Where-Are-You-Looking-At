using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AdvisorStatUI : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject statTextListParent;
    [SerializeField] private Image advisorIcon;
    [SerializeField] private Text advisorName;
    [SerializeField] private Text advisorSlot;
    [SerializeField] private List<Text> statTextList = new List<Text>();
    [SerializeField] private ClearWindowManager clearWindowManager;

    private void Start()
    {
        clearWindowManager = ClearWindowManager.Instance;
    }

    private void Reset()
    {
        window = transform.GetChild(0).gameObject;
        clearWindowManager = transform.FindParent<ClearWindowManager>("ClearWindow");
        advisorIcon = transform.FindChild<Image>("Advisor_Icon");
        advisorName = transform.FindChild<Text>("Advisor_Name");
        advisorSlot = transform.FindChild<Text>("Advisor_Slot");
        statTextListParent = GameObject.Find("StatInfoValue");
        statTextList = statTextListParent.transform.GetComponentsInChildren<Text>().ToList();
    }

    private void OnEnable()
    {
        if (clearWindowManager != null)
        {
            Debug.Log("등록: AdvisorStat -> OnAdvisorStatChange 이벤트");
            clearWindowManager.OnAdvisorStatChange += UpdateStatUI;
        }
    }

    private void OnDisable()
    {
        if (clearWindowManager != null)
        {
            Debug.Log("해제: AdvisorStat -> OnAdvisorStatChange 이벤트");
            clearWindowManager.OnAdvisorStatChange -= UpdateStatUI;
        }
    }

    public void SetWindow(bool active)
    {
        window.SetActive(active);
    }

    public void UpdateStatUI(List<Attribute> statList)
    {
        if (statList.Count != statTextList.Count) return;

        for (int i = 0; i < statList.Count; i++)
        {
            statTextList[i].text = statList[i].Value.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject statTextListParent;
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
        statTextListParent = GameObject.Find("StatInfoValue");
        statTextList = statTextListParent.transform.GetComponentsInChildren<Text>().ToList();
    }

    private void OnEnable()
    {
        if (clearWindowManager != null)
        {
            Debug.Log("등록: PlayerStat -> OnPlayerStatChange 이벤트");
            clearWindowManager.OnPlayerStatChange += UpdateStatUI;
        }
    }

    private void OnDisable()
    {
        if (clearWindowManager != null)
        {
            Debug.Log("해제: PlayerStat -> OnPlayerStatChange 이벤트");
            clearWindowManager.OnPlayerStatChange -= UpdateStatUI;
        }
    }

    public void SetWindow(bool active)
    {
        window.SetActive(active);
    }

    public void UpdateStatUI(List<Attribute> statList)
    {
        if (statList.Count != statTextList.Count) return;

        for(int i=0; i<statList.Count; i++)
        {
            statTextList[i].text = statList[i].Value.ToString();
        }
    }
}

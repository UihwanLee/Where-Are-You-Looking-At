using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProductInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject statTextListParent;
    [SerializeField] private ClearWindowManager clearWindowManager;
    [SerializeField] private Image productIcon;
    [SerializeField] private Text productNameTxt;
    [SerializeField] private Text productTypeTxt;
    [SerializeField] private Text productDescTxt;

    private void Reset()
    {
        window = transform.GetChild(0).gameObject;
        clearWindowManager = transform.FindParent<ClearWindowManager>("ClearWindow");
        statTextListParent = GameObject.Find("StatInfoValue");
        productIcon = transform.FindChild<Image>("Product_Icon");
        productNameTxt = transform.FindChild<Text>("Product_Name");
        productTypeTxt = transform.FindChild<Text>("Product_Type");
        productDescTxt = transform.FindChild<Text>("Product_Desc");
    }

}

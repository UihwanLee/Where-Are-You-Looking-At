using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private PlayerStatSO data;

    [SerializeField] private Attribute maxHp;                        // 최대 체력
    [SerializeField] private Attribute speed;                        // 속도
    [SerializeField] private Attribute reproductionHp;               // 체력 재생
    [SerializeField] private Attribute recoveryHp;                   // 체력 회복
    [SerializeField] private Attribute defense;                      // 방어
    [SerializeField] private Attribute evasion;                      // 회피
    [SerializeField] private Attribute luck;                         // 행운
    [SerializeField] private Attribute absorptionItem;               // 아이템 흡수

    #region 파일 로드

    private const string FileName = "player_data.json";

    /// <summary>
    /// 데이터를 Json 파일로 저장.
    /// </summary>
    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);
        string filePath = Path.Combine(Application.persistentDataPath, FileName);

        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log($"데이터 저장 성공: {filePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"데이터 저장 실패: {e.Message}");
        }
    }

    /// <summary>
    /// Json 파일에서 데이터를 불러오기.
    /// </summary>
    public PlayerStat LoadData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FileName);

        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);

                PlayerStat loadedData = JsonUtility.FromJson<PlayerStat>(json);
                Debug.Log($"데이터 로드 성공: {filePath}");

                return loadedData;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"데이터 로드 실패: {e.Message}. ");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("저장된 파일이 없습니다.");
            // 저장된 파일이 없으면 초기값으로 시작
            return null;
        }
    }

    #endregion 

    private Dictionary<AttributeType, Attribute> attributeDict;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        maxHp = new Attribute(0, data.MaxHp);
        reproductionHp = new Attribute(1, data.ReproductionHp);
        recoveryHp = new Attribute(2, data.RecoveryHp);
        defense = new Attribute(3,data.Defense);
        evasion = new Attribute(4, data.Evasion);
        speed = new Attribute(5, data.Speed);
        luck = new Attribute(6, data.Luck);
        absorptionItem = new Attribute(7, data.AbsorptionItem);

        attributeDict = new Dictionary<AttributeType, Attribute>();
        attributeDict.Add(AttributeType.MaxExp, maxHp);
        attributeDict.Add(AttributeType.Speed, speed);
        attributeDict.Add(AttributeType.ReproductionHp, reproductionHp);
        attributeDict.Add(AttributeType.RecoveryHp, recoveryHp);
        attributeDict.Add(AttributeType.Defense, defense);
        attributeDict.Add(AttributeType.Evasion, evasion); 
        attributeDict.Add(AttributeType.Luck, luck);
        attributeDict.Add(AttributeType.AbsorptionItem, absorptionItem);    
    }

    public void Add(AttributeType type, float amount)
    {
        if (attributeDict.TryGetValue(type, out Attribute attribute))
        {
            attribute.AddValue(amount);
        }
    }

    public void Sub(AttributeType type, float amount)
    {
        if (attributeDict.TryGetValue(type, out Attribute attribute))
        {
            attribute.SubValue(amount);
        }
    }

    public void Set(AttributeType type, float amount)
    {
        if (attributeDict.TryGetValue(type, out Attribute attribute))
        {
            attribute.SetValue(amount);
        }
    }

    #region 프로퍼티

    public float MaxHp { get { return maxHp.Value; } }
    public float Speed { get { return speed.Value; } }
    public float ReproductionHp { get { return  reproductionHp.Value; } }
    public float RecoveryHp { get { return recoveryHp.Value; } }
    public float Defense { get { return defense.Value; } }
    public float Evasion { get { return evasion.Value; } }
    public float Luck { get { return luck.Value; }}
    public float AbsorptionItem {  get { return absorptionItem.Value; } }
    public Dictionary<AttributeType, Attribute> AttributeDict { get { return attributeDict; } }

    #endregion
}

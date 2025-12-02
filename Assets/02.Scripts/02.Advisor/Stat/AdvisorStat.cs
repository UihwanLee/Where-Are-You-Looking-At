using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AdvisorStat : MonoBehaviour
{
    [SerializeField] private AdvisorStatSO data;
    
    [SerializeField] private Attribute atk;                             // 공격력
    [SerializeField] private Attribute attackSpeed;                     // 공격 속도 
    [SerializeField] private Attribute attackRange;                     // 공격 범위
    [SerializeField] private Attribute critChance;                      // 치명타 확률
    [SerializeField] private Attribute critMultiplier;                  // 치명타 계수
    [SerializeField] private Attribute projectileSpeed;                 // 투사체 속도

    #region 파일 로드

    private const string FileName = "advisor_data.json";

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
        atk = new Attribute(0, data.Atk);
        attackSpeed = new Attribute(1, data.AttackSpeed);
        attackRange = new Attribute(2, data.AttackRange);
        critChance = new Attribute(3, data.CritChance);
        critMultiplier = new Attribute(4, data.CritMultiplier);
        projectileSpeed = new Attribute(5, data.ProjectileSpeed);

        attributeDict = new Dictionary<AttributeType, Attribute>();
        attributeDict.Add(AttributeType.Atk, atk);
        attributeDict.Add(AttributeType.AttackSpeed, attackSpeed);
        attributeDict.Add(AttributeType.AttackRange, attackRange);
        attributeDict.Add(AttributeType.CritChance, critChance);
        attributeDict.Add(AttributeType.CritMultiplier, critMultiplier);
        attributeDict.Add(AttributeType.ProjectileSpeed, projectileSpeed);
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

    public float Atk { get { return atk.Value; } }
    public float AttackSpeed { get { return attackSpeed.Value; } }
    public float AttackRange { get { return attackRange.Value; } }
    public float CritChance { get { return critChance.Value; } }
    public float CritMultiplier { get { return critMultiplier.Value; } }
    public float ProjectileSpeed { get {return projectileSpeed.Value; } }

    #endregion
}

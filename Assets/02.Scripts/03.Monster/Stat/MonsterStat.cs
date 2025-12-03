using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    [SerializeField] private MonsterStatSO data;

    [SerializeField] private Attribute maxHp;                           // 최대 체력
    [SerializeField] private Attribute speed;                           // 속도               
    [SerializeField] private Attribute atk;                             // 공격력
    [SerializeField] private Attribute spawnSpeed;                      // 스폰 속도

    #region 파일 로드

    private const string FileName = "monster_data.json";

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
        speed = new Attribute(1, data.Speed);
        atk = new Attribute(2, data.Atk);
        spawnSpeed = new Attribute(3, data.SpawnSpeed);

        attributeDict = new Dictionary<AttributeType, Attribute>();
        attributeDict.Add(AttributeType.MaxExp, maxHp);
        attributeDict.Add(AttributeType.Speed, speed);
        attributeDict.Add(AttributeType.Atk, atk);
        attributeDict.Add(AttributeType.SpawnSpeed, spawnSpeed);
    }

    public void Add(AttributeType type, float amount)
    {
        if(attributeDict.TryGetValue(type, out Attribute attribute))
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
    public float Atk { get { return atk.Value; } }
    public float SpawnSpeed { get { return spawnSpeed.Value; } }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FloatingTextPool : MonoBehaviour
{
    [Header("FloatingText 프리팹")]
    [SerializeField] private GameObject _prefab;
    [Header("FloatingText 초기 생성 개수")]
    [SerializeField] private int _maxCount;
    [Header("FloatingText SO 데이터")]
    [SerializeField] private FloatingTextSO _data;
    [Header("Canvas 부모")]
    [SerializeField] private Transform _canvasParent;

    private string _key;
    private PoolManager _poolManager;
    private FloatingTextPoolManager _manager;

    private void Start()
    {
        _poolManager = PoolManager.Instance;
        _manager = FloatingTextPoolManager.Instance;
        Initialize();
    }

    private void Initialize()
    {
        // Key 저장
        _key = _data.Name;

        // Damage 생성
        _poolManager.CreatePool(_key, _prefab, _maxCount, _canvasParent);

        // Pool 전달
        _manager.Add(this);
    }

    public GameObject SpawnText(string text, Transform target, Color? color = null)
    {
        // Text를 세팅하고 넘기기
        GameObject newText = _poolManager.GetObject(_key);

        if (newText != null)
        {
            if (newText.TryGetComponent<FloatingText>(out FloatingText floatingText))
            {
                // 초기화
                floatingText.Initialize();

                // 텍스트 설정
                floatingText.SetText(text);

                //// 색상 설정
                Color newColor = (Color)((color != null) ? color : _data.Color);
                floatingText.SetColor(newColor);

                // Duration 설정
                floatingText.SetDuration(_data.Duration);

                // 위치 설정
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position);
                floatingText.SetPosition(screenPosition);

                // Floating 코루틴은 몇 초 뒤에 자동으로 반환
                floatingText.StartFadeCoroutine(_data);
            }
        }

        return newText;
    }

    public void Release(GameObject obj)
    {
        _poolManager.ReleaseObject(_key, obj);
    }

    #region 프로퍼티

    public string Key { get { return _key; } }
    public FloatingTextSO Data { get { return _data; } }
    public TextType Type { get { return _data.Type; } }

    #endregion
}

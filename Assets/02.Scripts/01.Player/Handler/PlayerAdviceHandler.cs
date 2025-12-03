using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdviceHandler : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void ApplyAdvice(Advice advice)
    {
        switch(advice.Data.EN)
        {
            case "EN_001":
                Debug.Log(advice.Data.Value);
                player.Stat.Add(AttributeType.MaxHp, advice.Data.Value);
                break;
            case "EN_002":
                player.Stat.Add(AttributeType.Speed, advice.Data.Value);
                break;
            case "EN_003":
                player.Stat.Add(AttributeType.ReproductionHp, advice.Data.Value);
                break;
            default:
                break;
        }

        // Stat UI 갱신
        ClearWindowManager.Instance.OnPlayerStatChange?.Invoke();
    }
}

using UnityEngine;

public class RoomToRoom : MonoBehaviour
{
    [SerializeField] private Collider2D topEntranceCollider;
    [SerializeField] private Collider2D bottomEntranceCollider;
    public Anomaly anomaly;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!anomaly.isAnomaly && collision.IsTouching(topEntranceCollider) || anomaly.isAnomaly && collision.IsTouching(bottomEntranceCollider))//이상현상 없고 위로, 이상현상 있고 아래로 [정답처리]
            {
                //방번호 ++
                Debug.Log("정답");//체크완
            }
            else //[오답 처리]
            {
                //방번호 초기화
                Debug.Log("오답");//체크완
            }
            //몬스터 초기화
            anomaly.initAnomaly();//리셋 후 초기화
            collision.transform.position = new Vector3(25f, 1f);
        }
    }
}
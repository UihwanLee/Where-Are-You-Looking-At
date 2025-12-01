using UnityEngine;

public class RoomToRoom : MonoBehaviour
{
    [SerializeField] private Collider2D topEntranceCollider;
    [SerializeField] private Collider2D bottomEntranceCollider;
    bool anomaly; // 나중에 게임매니저나 맵관련에서 이상현상 체크 값 가져오기

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!anomaly && collision.IsTouching(topEntranceCollider) || anomaly && collision.IsTouching(bottomEntranceCollider))//이상현상 없고 위로, 이상현상 있고 아래로 [정답처리]
            {
                //방번호 ++
            }
            else //[오답 처리]
            {
                //방번호 초기화
            }
            //몬스터 초기화
            //이상현상 초기화
            collision.transform.position = new Vector3(25f, 1f);
        }

    }
}
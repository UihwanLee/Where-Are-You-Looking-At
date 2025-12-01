using UnityEngine;

public class RoomToRoom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //다음 방 번호 세팅
            //몬스터 초기화
            //이상현상 초기화
            collision.transform.position = new Vector3(25f, 1f);
        }

    }
}

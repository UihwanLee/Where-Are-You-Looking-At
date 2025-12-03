using UnityEngine;
using UnityEngine.UI;
public class RoomToRoom : MonoBehaviour
{
    [SerializeField] private Collider2D topEntranceCollider;
    [SerializeField] private Collider2D bottomEntranceCollider;
    
    public Anomaly anomaly;
    public int roomNumber;
    public Text roomNumbertxt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!anomaly.isAnomaly && collision.IsTouching(topEntranceCollider) || anomaly.isAnomaly && collision.IsTouching(bottomEntranceCollider))//�̻����� ���� ����, �̻����� �ְ� �Ʒ��� [����ó��]
            {
                roomNumber++;
                Debug.Log("����");//üũ��
            }
            else //[���� ó��]
            {
                roomNumber = 0;
                Debug.Log("����");//üũ��
            }
            //���� �ʱ�ȭ
            MonsterSpawner.Instance.ClearActiveMonster();
            anomaly.initAnomaly();//���� �� �ʱ�ȭ
            ClearWindowManager.Instance.SetClearWindow(true);
            Player player = GameManager.Instance.Player;
            player.transform.position = new Vector3(100f, 100f);
            roomNumbertxt.text = $"{roomNumber}";
        }
    }
}
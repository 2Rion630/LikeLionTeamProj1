using UnityEngine;

public class CHW_Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager gm = Object.FindFirstObjectByType<GameManager>();
            gm.isSceneCleared = true;
            /*if (gm != null)
            {
                gm.isSceneCleared = true;
                Debug.Log("Player�� �浹: �� Ŭ���� ó����.");
            }
            else
            {
                Debug.LogWarning("GameManager�� ã�� �� �����ϴ�.");
            }*/
        }
    }
}

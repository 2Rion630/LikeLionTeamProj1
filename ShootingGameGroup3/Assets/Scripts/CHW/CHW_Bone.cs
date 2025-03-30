using UnityEngine;

public class CHW_Bone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Bone ������Ʈ ����
        }
        else if (collision.CompareTag("Player"))
        {
            CHW_Player player = collision.GetComponent<CHW_Player>();
            if (player != null)
            {
                player.TakeDamage(1); // ü�� -1
            }

            Destroy(gameObject); // Bone ������Ʈ ����
        }


    }
}

using UnityEngine;

public class sdh_Bullet : MonoBehaviour
{
    float speed = 10f; // �Ѿ� �ӵ�
    public float dmg;
    private Vector3 direction; // �Ѿ��� ���ư� ����
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 wP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dirvec = wP - transform.position;
        direction = new Vector3(dirvec.x, dirvec.y, 0).normalized;


        float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);


        rb.AddForce(direction*speed, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("���� ����");
        }
        if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
            Debug.Log("���п� ����");
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class sdh_ShieldEnemy : MonoBehaviour
{
    public float HP = 10;
    Transform pt;   //�÷��̾� transform
    Rigidbody2D rb;
    float speed = 1f;
    float turnDelay = 0.1f;
    float nextCheckTime = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            pt = playerObject.transform;
        }
    }
    private void Update()
    {
        MoveTowardPlayer();


        if (Time.time >= nextCheckTime)
        {
            CheckFlipX();
            nextCheckTime = Time.time + turnDelay; // ���� üũ �ð� ����
        }

    }
    void MoveTowardPlayer()
    {
        rb.linearVelocity = (pt.position - transform.position).normalized * speed;
    }
    void CheckFlipX()
    {
        if (transform.position.x < pt.position.x) // ���� ����
        {
            if (Random.value > 0.5f) // 50% Ȯ���� �ൿ
            {
                Invoke("TurnLeft", 0.5f);
            }
        }
        else
        {
            if (Random.value > 0.5f) // 50% Ȯ���� �ൿ
            {
                Invoke("TurnRight", 0.5f);
            }
        }
    }
    
    void TurnLeft()
    {
        Vector3 v = transform.rotation.eulerAngles;
        v.y = 180;
        transform.rotation = Quaternion.Euler(v);
    }

    void TurnRight()
    {
        Vector3 v = transform.rotation.eulerAngles;
        v.y = 0;
        transform.rotation = Quaternion.Euler(v);
    }
}

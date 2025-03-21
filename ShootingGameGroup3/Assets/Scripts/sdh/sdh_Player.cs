using UnityEngine;

public class sdh_Player : MonoBehaviour
{
    float speed = 5f;
    public Transform gunT;
    private SpriteRenderer psr; 
    private SpriteRenderer gunsr;
    public Transform fp;


    void Start()
    {
        // SpriteRenderer ������Ʈ ��������
        psr = GetComponent<SpriteRenderer>();
        gunsr = gunT.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        FlipPlayerAndGun();
    }

    private void FlipPlayerAndGun()
    {
        // ���콺 ��ġ ���
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ���콺 ��ġ�� ���� Flip ����
        if (mousePosition.x < transform.position.x)
        {
            // ���콺�� ������ ��
            psr.flipX = true;
            gunsr.flipY = true; // �ѵ� ���� ����
            fp.localPosition = new Vector3(fp.localPosition.x, -Mathf.Abs(fp.localPosition.y), fp.localPosition.z);
 
        }
        else
        {
            // ���콺�� �������� ��
            psr.flipX = false;
            gunsr.flipY = false; // �� ���� ����
            fp.localPosition = new Vector3(fp.localPosition.x, Mathf.Abs(fp.localPosition.y), fp.localPosition.z);
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 vec = new Vector3(moveX, moveY, 0);

        if (vec.magnitude > 1) // �밢�� �̵� �� ����ȭ
        {
            vec.Normalize();
        }

        transform.Translate(vec * speed * Time.deltaTime);
    }


}

using System.Collections;
using UnityEngine;

public class sdh_Player : MonoBehaviour
{
    
    SpriteRenderer psr; 
    SpriteRenderer gunsr;
    Rigidbody2D rb;
    float speed = 3f;
    public Transform gunT;
    public Transform fp;
    bool isRolling = false;
    bool isDashCool = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        psr = GetComponent<SpriteRenderer>();
        gunsr = gunT.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isRolling)
        {
            Move();
            FlipPlayer();
        }
        FlipGun();
        Roll();
    }

    private void FlipPlayer()
    {
        // ���콺 ��ġ ���
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ���콺 ��ġ�� ���� Flip ����
        if (mousePosition.x < transform.position.x)
        {
            // ���콺�� ������ ��
            psr.flipX = true;
        }
        else
        {
            // ���콺�� �������� ��
            psr.flipX = false;
        }
    }

    void FlipGun()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ���콺 ��ġ�� ���� Flip ����
        if (mousePosition.x < transform.position.x)
        {
            gunsr.flipY = true; // �� ���� ����
            fp.localPosition = new Vector3(fp.localPosition.x, -Mathf.Abs(fp.localPosition.y), fp.localPosition.z);

        }
        else
        {
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

        rb.linearVelocity = vec * speed;
    }

    void Roll()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isRolling && !isDashCool)
        {
            StartCoroutine(StartRoll());
        }
    }

    IEnumerator StartRoll()
    {
        isRolling = true;
        isDashCool = true;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(moveX, moveY, 0).normalized;
        if (dir == Vector3.zero)
        {
            Vector3 wP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dirvec = wP - transform.position;
            dir = new Vector3(dirvec.x, dirvec.y, 0).normalized;
        }
        rb.AddForce(dir * 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        rb.linearVelocity = Vector2.zero;
        isRolling = false;
        yield return new WaitForSeconds(2.8f);
        isDashCool = false;
    }

    void GetHit()
    {
        //�ǰ�ó��
    }
}

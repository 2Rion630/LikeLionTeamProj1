using System.Collections;
using UnityEngine;

public class LHG_Monster2 : MonoBehaviour
{
    public int health = 10; // ������ ü��
    public float moveSpeed = 2f; // ������ �̵� �ӵ�
    private Transform player; // �÷��̾��� Transform
    public float chaseDistance = 10f; // �÷��̾ ������ �Ÿ�

    private Vector3 randomDirection; // ���� ����
    private float changeDirectionTime = 3f; // ���� ���� �ֱ�
    private float timer; // Ÿ�̸�

    private SpriteRenderer spriteRenderer; // ��������Ʈ ������
    private Color originalColor; // ���� ����

    private void Start()
    {
        // �÷��̾��� Transform�� ã�� ���� ������ ����
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomDirection();

        // ��������Ʈ �������� ���� ���� �ʱ�ȭ
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        // �� �����Ӹ��� Move �޼��� ȣ��
        Move();
    }

    private void Move()
    {
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // �÷��̾ ���� �Ÿ� �̳��� ���� ���
        if (distanceToPlayer < chaseDistance)
        {
            // �÷��̾� �������� �̵�
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // �÷��̾� ���⿡ ���� ��������Ʈ ȸ��
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false; // ������
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true; // ����
            }
        }
        else
        {
            // ���� �������� �̵�
            transform.position += randomDirection * moveSpeed * Time.deltaTime;

            // ���� ���� Ÿ�̸� ������Ʈ
            timer += Time.deltaTime;
            if (timer >= changeDirectionTime)
            {
                SetRandomDirection(); // ���� ���� ����
                timer = 0f; // Ÿ�̸� �ʱ�ȭ
            }
        }
    }

    private void SetRandomDirection()
    {
        // ������ x, y ���� �����Ͽ� ���� ���� ����
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        randomDirection = new Vector3(randomX, randomY, 0).normalized; // ����ȭ�Ͽ� ���� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Ѿ˰� �浹 �� ������ ó��
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1); // 1�� ������ �ޱ�
        }
    }

    public void TakeDamage(int damage)
    {
        // ü�� ����
        health -= damage;
        if (health <= 0)
        {
            
            Destroy(gameObject); // ���� ���� ������Ʈ ����
        }
        else
        {
            StartCoroutine(BecomeTransparent(2f)); // ���������� �ڷ�ƾ ����
        }
    }

    private IEnumerator BecomeTransparent(float duration)
    {
        // ���͸� �����ϰ� ����� �ڷ�ƾ
        Color transparentColor = originalColor;
        transparentColor.a = 0.1f; // ���� ����
        spriteRenderer.color = transparentColor; // ���� ����

        yield return new WaitForSeconds(duration); // ������ �ð� ���

        spriteRenderer.color = originalColor; // ���� �������� ����
    }

    
}

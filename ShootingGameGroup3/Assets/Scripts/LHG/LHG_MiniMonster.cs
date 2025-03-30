using UnityEngine;

public class LHG_MiniMonster : MonoBehaviour
{
    public int health = 3; // ���� ü��
    public float moveSpeed = 1f; // ���� �̵� �ӵ�
    private Transform player; // �÷��̾��� Transform
    public float chaseDistance = 7f; // ���� �Ÿ�

    private void Start()
    {
        // �±װ� "Player"�� ���� ������Ʈ�� ã�Ƽ� player ������ �Ҵ�
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            // �÷��̾� ���� ���
            Vector3 direction = (player.position - transform.position).normalized;

            // ���� ��ġ�� �÷��̾� �������� �̵�
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    // ���� ���ظ� ���� �� ȣ��Ǵ� �޼���
    public void TakeDamage(int damage)
    {
        health -= damage; // ü�� ����
        if (health <= 0)
        {
            // ü���� 0 ���ϰ� �Ǹ� �� ������Ʈ �ı�
            Destroy(gameObject);
        }
    }

    // �浹�� �߻����� �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� "Bullet" �±׸� ���� ���
        if (collision.CompareTag("Bullet"))
        {
            // ���ظ� ����
            TakeDamage(1);
        }
    }
}
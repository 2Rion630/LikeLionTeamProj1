using System.Collections;
using UnityEngine;

public class LHG_Monster : MonoBehaviour
{
    public int health = 3; // ������ ü��
    public GameObject MiniMonsterPrefab; // ������ �̴� ���� ������
    public int numberOfMiniMonster = 3; // ������ �̴� ������ ��

    public float moveSpeed = 1f; // ������ �̵� �ӵ�
    private Transform player; // �÷��̾��� Transform
    public float chaseDistance = 10f; // ���Ͱ� �÷��̾ �����ϴ� �Ÿ�

    private Vector3 randomDirection; // ���� ����
    private float changeDirectionTime = 2f; // ���� ���� �ֱ�
    private float timer; // Ÿ�̸�

    private SpriteRenderer spriteRenderer; // ��������Ʈ ������
    private Color originalColor; // ���� ����

    public AudioClip deathSound; // ���� ���� �� ����� ���� Ŭ��
    private AudioSource audioSource; // AudioSource ������Ʈ

    private void Start()
    {
        // �±װ� "Player"�� ���� ������Ʈ�� ã�Ƽ� player ������ �Ҵ�
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomDirection(); // �ʱ� ���� ���� ����

        // ��������Ʈ �������� ���� ���� ����
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // AudioSource ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
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
            transform.position += direction * moveSpeed * Time.deltaTime;

            // �÷��̾� ���⿡ ���� ���� ��������Ʈ ȸ��
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

            // Ÿ�̸� ����
            timer += Time.deltaTime;
            // �ֱⰡ ������ ���� ���� ����
            if (timer >= changeDirectionTime)
            {
                SetRandomDirection();
                timer = 0f; // Ÿ�̸� �ʱ�ȭ
            }
        }
    }

    private void SetRandomDirection()
    {
        // -1�� 1 ������ ���� ���� �����Ͽ� ���� ���� ����
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        randomDirection = new Vector3(randomX, randomY, 0).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� "Bullet" �±׸� ���� ���
        if (collision.CompareTag("Bullet"))
        {
            // ���ظ� ����
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ü�� ����
        if (health <= 0)
        {
            // ü���� 0 ���ϰ� �Ǹ� �̴� ���ͷ� �и�
            SplitIntoMiniMonster();
            PlayDeathSound(); // ���� �� ���� ���
            Destroy(gameObject); // ���� ������Ʈ �ı�
        }
        else
        {
            // ü���� ���������� ���������� �ڷ�ƾ ����
            StartCoroutine(BecomeTransparent(2f));
        }
    }

    private IEnumerator BecomeTransparent(float duration)
    {
        // ���͸� �����ϰ� ����� ���� �ڷ�ƾ
        Color transparentColor = originalColor;
        transparentColor.a = 0.1f; // ���� ����
        spriteRenderer.color = transparentColor;

        yield return new WaitForSeconds(duration); // ������ �ð� ���

        // ���� �������� ����
        spriteRenderer.color = originalColor;
    }

    private void SplitIntoMiniMonster()
    {
        // �̴� ���� ����
        for (int i = 0; i < numberOfMiniMonster; i++)
        {
            // ������ ��ġ�� �̴� ���� ����
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            Instantiate(MiniMonsterPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound); // ���� �� ���� ���
        }
    }
}
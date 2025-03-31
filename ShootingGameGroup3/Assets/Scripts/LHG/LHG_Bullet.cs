using System;
using UnityEngine;

public class LHG_Bullet : MonoBehaviour
{
    public float speed = 10f; // �߻�ü�� �̵� �ӵ�
    public int damage = 1; // �߻�ü�� ���ط�
    public int bounceCount = 2; // �ִ� �ٿ Ƚ��

    private Vector2 direction; // �߻�ü�� �̵� ����
    private int currentBounces = 0; // ���� �ٿ Ƚ��

    public GameObject Effect; // �߻�ü�� �浹���� �� ������ ����Ʈ
    public AudioClip hitSound; // �浹 �� ����� ���� Ŭ��
    public AudioClip shootSound; // �߻� �� ����� ���� Ŭ��
    private AudioSource audioSource; // AudioSource ������Ʈ

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������
    }

    // �߻�ü�� ������ �����ϴ� �޼���
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    // �߻� �޼���
    public void Shoot()
    {
        PlayShootSound(); // �߻� ���� ���
    }

    void Update()
    {
        // �߻�ü�� ���� �������� �̵���Ŵ
        transform.Translate(direction * speed * Time.deltaTime);
        // ī�޶��� ����Ʈ ��ǥ�� ������
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // �Ѿ��� ȭ�� ������ ������ �ı�
        if (transform.position.x < -screenBounds.x || transform.position.x > screenBounds.x ||
            transform.position.y < -screenBounds.y || transform.position.y > screenBounds.y)
        {
            Destroy(gameObject);
        }
    }

    // �浹�� �߻����� �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� "Monster" �±׸� ���� ���
        if (collision.CompareTag("Monster"))
        {
            LHG_Monster monster = collision.GetComponent<LHG_Monster>();
            if (monster != null)
            {
                // ���Ϳ��� ���ظ� ��
                monster.TakeDamage(damage);
                CreateEffect(); // ����Ʈ ����
                PlayHitSound(); // ���� ���
                Bounce(collision.transform.position); // �ٿ ó��
            }
        }
        // �浹�� ��ü�� "MiniMonster" �±׸� ���� ���
        else if (collision.CompareTag("MiniMonster"))
        {
            LHG_MiniMonster miniMonster = collision.GetComponent<LHG_MiniMonster>();
            if (miniMonster != null)
            {
                // �̴� ���Ϳ��� ���ظ� ��
                miniMonster.TakeDamage(damage);
                CreateEffect(); // ����Ʈ ����
                PlayHitSound(); // ���� ���
                Bounce(collision.transform.position); // �ٿ ó��
            }
        }
        // �浹�� ��ü�� "Monster2" �±׸� ���� ���
        else if (collision.CompareTag("Monster2"))
        {
            LHG_Monster2 monster2 = collision.GetComponent<LHG_Monster2>();
            if (monster2 != null)
            {
                // ����2���� ���ظ� ��
                monster2.TakeDamage(damage);
                CreateEffect(); // ����Ʈ ����
                PlayHitSound(); // ���� ���
                Bounce(collision.transform.position); // �ٿ ó��
            }
        }
        // �浹�� ��ü�� "Player" �±׸� ���� ��� (�÷��̾���� �浹 ó��)
        else if (collision.CompareTag("Player"))
        {
            // �÷��̾���� �浹 �� �ƹ��� �ൿ�� ���� ����
            // �Ǵ� �߻�ü�� ������ �� �ֽ��ϴ�.
            Destroy(gameObject); // �߻�ü ����
        }
    }

    // ����Ʈ�� �����ϴ� �޼���
    void CreateEffect()
    {
        GameObject go = Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(go, 0.5f); // 0.5�� �� ����Ʈ ������Ʈ ����
    }

    // ���带 ����ϴ� �޼���
    void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound); // ���� ���
        }
    }

    // �߻� ���带 ����ϴ� �޼���
    void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound); // �߻� ���� ���
        }
    }

    // �ٿ ó�� �޼���
    void Bounce(Vector2 hitPoint)
    {
        // �ִ� �ٿ Ƚ���� �������� ���� ���
        if (currentBounces < bounceCount)
        {
            currentBounces++; // �ٿ Ƚ�� ����
            Vector2 bounceDirection = (Vector2)transform.position - hitPoint; // �ݻ� ���� ���
            direction = bounceDirection.normalized; // ������ ����ȭ

            FindClosestMonster(); // ���� ����� ���� ã��
        }
        else
        {
            Destroy(gameObject); // �ٿ Ƚ���� �ʰ��ϸ� �߻�ü ����
        }
    }

    // ���� ����� ���͸� ã�� �޼���
    void FindClosestMonster()
    {
        // ���� ��ġ���� 10f �ݰ� ���� ��� �ݶ��̴��� ������
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, 10f);
        Transform closestMonsterTransform = null; // ���� ����� ������ ��ȯ
        float closestDistance = Mathf.Infinity; // ���� ����� �Ÿ� �ʱ�ȭ

        // ��� ���Ϳ� ���� �ݺ�
        foreach (Collider2D collider in monsters)
        {
            // ���� �Ǵ� �̴� ���� �±׸� ���� ���
            if (collider.CompareTag("Monster") || collider.CompareTag("MiniMonster"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position); // �Ÿ� ���
                if (distance < closestDistance) // ���� ����� ���� ������Ʈ
                {
                    closestDistance = distance;
                    closestMonsterTransform = collider.transform;
                }
            }
        }

        // ���� ����� ���Ͱ� �߰ߵ� ���
        if (closestMonsterTransform != null)
        {
            Vector2 targetDirection = (closestMonsterTransform.position - transform.position).normalized; // ��ǥ ���� ���
            direction = targetDirection; // ���� ������Ʈ
        }
    }
}
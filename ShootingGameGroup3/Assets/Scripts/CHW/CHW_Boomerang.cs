/*using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private Transform player;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 direction;
    private CHW_Player playerController;
    private bool returning = false;
    private SpriteRenderer spriteRenderer;

    [Header("Boomerang Settings")]
    public GameObject boomerangPrefab;
    public float speed = 7f;
    public float boomerangRange = 5f;
    public float rotationSpeed = 720f; //  ������ ȸ�� (����/��)
    public float returnSpeedMultiplier = 1.5f; //  �ǵ��ƿ� �� �ӵ� ����

    public void Initialize(Transform playerTransform, Vector3 throwDirection, CHW_Player controller)
    {
        player = playerTransform;
        startPosition = transform.position;
        direction = throwDirection.normalized;
        playerController = controller;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        targetPosition = startPosition + direction * boomerangRange;
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); //  ���������� ȸ��

        if (!returning)
        {
            transform.position += direction * speed * Time.deltaTime;

            // Ư�� �Ÿ� ���� �� �ǵ��ƿ���
            if (Vector3.Distance(startPosition, transform.position) >= boomerangRange)
            {
                returning = true;
            }
        }
        else
        {
            // �÷��̾�� ���ư���
            Vector3 returnDirection = (player.position - transform.position).normalized;
            transform.position += returnDirection * (speed * returnSpeedMultiplier) * Time.deltaTime;

            if (!spriteRenderer.enabled)
            {
                spriteRenderer.enabled = true;
            }

            // �÷��̾�� ������ ȸ��
            if (Vector3.Distance(transform.position, player.position) < 0.3f)
            {
                playerController.RetrieveBoomerang();
                Destroy(gameObject);
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!returning && collision.CompareTag("Wall"))
        {
            returning = true;
        }

        if (collision.CompareTag("Enemy"))
        {
            Skeleton enemy = collision.GetComponent<Skeleton>();
            if (enemy != null)
            {
                enemy.EnemyTakeDamage(1); // ü�� -1
            }

            Destroy(gameObject); // Bone ������Ʈ ����
        }
    }

    
}*/


using UnityEngine;

public class CHW_Boomerang : MonoBehaviour
{
    private Transform shooter;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 direction;
    private bool returning = false;
    private SpriteRenderer spriteRenderer;
    private CHW_BoomerangShooter boomerangShoother;

    public float speed = 7f;
    public float boomerangRange = 5f;
    public float rotationSpeed = 720f;
    public float returnSpeedMultiplier = 1.5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!returning)
        {
            transform.position += direction * speed * Time.deltaTime;
            if (Vector3.Distance(startPosition, transform.position) >= boomerangRange)
            {
                returning = true;
            }
        }
        else
        {
            Vector3 returnDirection = (shooter.position - transform.position).normalized;
            transform.position += returnDirection * (speed * returnSpeedMultiplier) * Time.deltaTime;
            if (Vector3.Distance(transform.position, shooter.position) < 0.3f)
            {
                boomerangShoother.RetrieveBoomerang();
                Destroy(gameObject);
            }
        }
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    public void Initialize(Transform shooterTransform, Vector3 throwDirection)
    {
        shooter = shooterTransform;
        boomerangShoother = shooterTransform.GetComponent<CHW_BoomerangShooter>();
        startPosition = transform.position;
        direction = throwDirection.normalized;
        //spriteRenderer.enabled = true;
        targetPosition = startPosition + direction * boomerangRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!returning && collision.CompareTag("Wall"))
        {
            returning = true;
        }
        
    }

    
}

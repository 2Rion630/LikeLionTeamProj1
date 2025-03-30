using UnityEngine;

public class CHW_BoomerangShooter : MonoBehaviour
{
    public Transform player;
    public GameObject boomerangPrefab;
    public Vector2 offset = new Vector2(0.5f, 0f);
    private bool facingRight = true;

    [Header("Boomerang Settings")]
    private bool canThrowBoomerang = true;
    private GameObject currentBoomerang;


    [Header("Boomerang Sound")]
    // �θ޶��� ȸ���� ������ �ݺ� ����� ���� Ŭ�� (Inspector���� �Ҵ�)
    public AudioClip chw_boomerang;
    private AudioSource audioSource;

    private void Start()
    {
        // AudioSource ������Ʈ�� ������ �߰�
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // ���� Ŭ�� �Ҵ� �� ���� ����
        audioSource.clip = chw_boomerang;
        audioSource.loop = true;
    }

    private void Update()
    {
        FollowPlayer();
        if (Input.GetMouseButtonDown(0) && canThrowBoomerang)
        {
            ThrowBoomerang();
        }
    }

    private void FollowPlayer()
    {
        if (player == null)
            return;

        Vector3 playerPosition = player.position;
        float direction = player.localScale.x;

        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            facingRight = !facingRight;
            offset.x = -offset.x; // �¿� ����
        }

        transform.position = new Vector3(
            playerPosition.x + offset.x,
            playerPosition.y + offset.y,
            0f
        );
    }

    public void ThrowBoomerang()
    {
        if (!canThrowBoomerang)
            return;
        canThrowBoomerang = false;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 throwDirection = (mousePosition - transform.position).normalized;
        GameObject boomerang = Instantiate(
            boomerangPrefab,
            transform.position,
            Quaternion.identity
        );

        CHW_Boomerang boomerangScript = boomerang.GetComponent<CHW_Boomerang>();
        if (boomerangScript != null)
        {
            boomerangScript.Initialize(transform, throwDirection);
        }
        else
        {
            Debug.LogError("Boomerang script not found on prefab!");
        }
        // �θ޶��� ȸ���Ǳ� ������ ���� ���� ���
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void RetrieveBoomerang()
    {
        canThrowBoomerang = true;
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

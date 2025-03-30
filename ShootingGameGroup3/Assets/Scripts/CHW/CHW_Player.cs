/*using System.Collections;
using UnityEngine;

public class CHW_Player : MonoBehaviour
{
    public float health = 3f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private bool isDashing = false;
    private bool canDash = true;

    [Header("Mouse Tracking")]
    public Camera mainCamera;

    [Header("Boomerang Settings")]
    public GameObject boomerangPrefab;
    //public Transform boomerangSpawnPoint;
    private bool canThrowBoomerang = true;
    private GameObject currentBoomerang;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        HandleInput();
        HandleRotation();
        HandleAnimations();
        if (Input.GetMouseButtonDown(0) && canThrowBoomerang)
        {
            ThrowBoomerang();
        }

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("�÷��̾� ü��: " + health);

        if (health <= 0)
        {
            Die();
        }


    }

    private void Die()
    {
        Debug.Log("�÷��̾ ����߽��ϴ�!");
        // ���� �÷��̾� ��� ó�� (��: ���� ���� ȭ�� ���� ��)
    }


    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
        }

        // �ӵ��� �������� �ִϸ��̼� ���� ����
        animator.SetBool("isMoving", rb.linearVelocity.magnitude > 0.1f);
    }

    private void HandleInput()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && moveInput != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }

    private void HandleRotation()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // ���콺 ��ġ�� �÷��̾�� �����̸� flip
        if (mousePosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); // �¿� ����
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // ���� ����
        }
    }

    private void Move()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        animator.SetTrigger("Dash"); // ��� �ִϸ��̼� �� ���� ����

        Vector2 dashDirection = moveInput;
        rb.linearVelocity = dashDirection * dashSpeed;

        isDashing = false;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void HandleAnimations()
    {
        if (isDashing)
        {
            animator.SetTrigger("Dash");
        }
        else if (moveInput != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }


    private void ThrowBoomerang()
    {
        if (!canThrowBoomerang) return;
        canThrowBoomerang = false;

        // ���콺 �������� ������ (��ġ�� �ƴ϶� ���⸸ ���)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 throwDirection = (mousePosition - transform.position).normalized;

        float offsetDistance = 0.5f; // �÷��̾� �տ��� ����
        Vector3 spawnPosition = transform.position + throwDirection * offsetDistance;

        currentBoomerang = Instantiate(boomerangPrefab, spawnPosition, Quaternion.identity);
        currentBoomerang.GetComponent<Boomerang>().Initialize(transform, throwDirection,this);
    }


    public void RetrieveBoomerang()
    {
        canThrowBoomerang = true;
    }
}


*/

using System.Collections;
using UnityEngine;

public class CHW_Player : MonoBehaviour
{
    public float health = 3f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private bool isDashing = false;
    private bool canDash = true;

    [Header("Mouse Tracking")]
    public Camera mainCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        HandleInput();
        HandleRotation();
        HandleAnimations();

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("�÷��̾� ü��: " + health);

        if (health <= 0)
        {
            Die();
        }


    }

    private void Die()
    {
        Debug.Log("�÷��̾ ����߽��ϴ�!");
        // ���� �÷��̾� ��� ó�� (��: ���� ���� ȭ�� ���� ��)
    }


    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
        }

        // �ӵ��� �������� �ִϸ��̼� ���� ����
        animator.SetBool("isMoving", rb.linearVelocity.magnitude > 0.1f);
    }

    private void HandleInput()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && moveInput != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }

    private void HandleRotation()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // ���콺 ��ġ�� �÷��̾�� �����̸� flip
        if (mousePosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); // �¿� ����
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // ���� ����
        }
    }

    private void Move()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        animator.SetTrigger("Dash"); // ��� �ִϸ��̼� �� ���� ����

        Vector2 dashDirection = moveInput;
        rb.linearVelocity = dashDirection * dashSpeed;

        isDashing = false;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void HandleAnimations()
    {
        if (isDashing)
        {
            animator.SetTrigger("Dash");
        }
        else if (moveInput != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }



}



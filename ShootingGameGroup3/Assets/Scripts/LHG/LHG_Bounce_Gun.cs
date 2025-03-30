using UnityEngine;

public class LHG_Bounce_Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // �߻��� �Ѿ� ������
    public Transform firePoint; // �Ѿ��� �߻�� ��ġ

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� �� �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ��ġ���� �Ѿ� ���� ���
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

            // �־��� �������� �Ѿ� �߻�
            Fire(direction);
        }
    }

    // �־��� �������� �Ѿ��� �߻��ϴ� �޼���
    public void Fire(Vector2 direction)
    {
        // �Ѿ� ������ �ν��Ͻ�ȭ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // �Ѿ��� LHG_Bullet ��ũ��Ʈ ��������
        LHG_Bullet bulletScript = bullet.GetComponent<LHG_Bullet>();

        // �Ѿ� ��ũ��Ʈ�� �����ϴ� ��� ���� ����
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction); // �Ѿ��� ���� ����
        }
    }

    // ���� �Ŵ������� ȣ���� �� �ִ� ���� �޼���
    public void FireAtDirection(Vector2 direction)
    {
        Fire(direction);
    }
}
using UnityEngine;

public class LHG_Weapon : MonoBehaviour
{
    public GameObject bulletPrefab; // �߻��� �Ѿ� ������
    public Transform firePoint; // �Ѿ��� �߻�� ��ġ

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
}

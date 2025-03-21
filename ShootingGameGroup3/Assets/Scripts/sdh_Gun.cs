using System.Collections;
using UnityEngine;

public class sdh_Gun : MonoBehaviour
{
    public GameObject bullet1; // �Ѿ� ������
    public GameObject bullet2; // �Ѿ� ������
    public GameObject bullet3; // �Ѿ� ������
    public Transform firePoint;    // �Ѿ� ���� ��ġ

    public Material FlashM;
    public Material DefaultM;
    float chargetime = 0;
    SpriteRenderer spr;
    Coroutine flashRoutine;
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    void Update()
    { 
        Vector3 mouseWP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateTowardsMouse(mouseWP);

       if(Input.GetMouseButtonDown(0))
        {
            flashRoutine = StartCoroutine(Flash());
        }

        if (Input.GetMouseButton(0))
        {
            chargetime += Time.deltaTime;
            if(chargetime > 3)
            {
                if(flashRoutine != null)
                {
                    StopCoroutine(flashRoutine);
                    spr.material = FlashM;
                }
            }

        }



        if (Input.GetMouseButtonUp(0))
        {
            if (chargetime >= 2)
            {
                UltraStrongShoot(mouseWP);
            }
            else if (chargetime > 1)
            {
                StrongShoot(mouseWP);
            }
            
            else
            {
                Shoot(mouseWP);
            }
            chargetime = 0;
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
                spr.material = DefaultM;
            }
        }
        

    }

    IEnumerator Flash() // 2�� ��¦�̸� �߰���
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            spr.material = DefaultM;
            yield return new WaitForSeconds(0.25f);
            spr.material = FlashM;
        }
    }

    private void RotateTowardsMouse(Vector3 wp)
    {

        // ���� ������Ʈ ��ġ���� ���콺 ��ġ���� ���� ���
        Vector3 direction = wp - transform.position;

        // Z�� ȸ���� ���� ���� ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ���� ������Ʈ�� ȸ�� ����
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    void Shoot(Vector3 wP)
    {
        Instantiate(bullet1, firePoint.position, Quaternion.identity);
    }
    void UltraStrongShoot(Vector3 wP)
    {
        Instantiate(bullet3, firePoint.position, Quaternion.identity);
    }
    void StrongShoot(Vector3 wP)
    {
        Instantiate(bullet2, firePoint.position, Quaternion.identity);
    }
}

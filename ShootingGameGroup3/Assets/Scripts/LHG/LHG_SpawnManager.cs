using UnityEngine;

public class LHG_SpawnManager : MonoBehaviour
{
    public GameObject monsterPrefab1; // ������ ù ��° ���� ������
    public GameObject monsterPrefab2; // ������ �� ��° ���� ������
    public float spawnInterval = 2f; // ���� ����
    private float timer;
    private int monsterCount = 0; // ���� ���� ��
    public int maxMonsters = 5; // �ִ� ���� ��

    private void Update()
    {
        // Ÿ�̸� ������Ʈ
        timer += Time.deltaTime;

        // ���� ������ ������ ���� ���� ���� �ִ� ������ ������ ���� ����
        if (timer >= spawnInterval && monsterCount < maxMonsters)
        {
            SpawnMonster();
            timer = 0f; // Ÿ�̸� ����
        }
    }

    void SpawnMonster()
    {
        // �������� ���� ������ ����
        GameObject selectedMonsterPrefab = Random.Range(0, 2) == 0 ? monsterPrefab1 : monsterPrefab2;

        // ���õ� ���� �������� ����
        Instantiate(selectedMonsterPrefab, transform.position, Quaternion.identity);
        monsterCount++; // ���� �� ����
    }
}


using UnityEngine;
using UnityEngine.Pool;

public class MonsterPooling : MonoBehaviour
{
    //���� �������� ���� ���� ������Ʈ ����
    [SerializeField] private GameObject _MonsterPrefab;
    //����Ƽ ������Ʈ Ǯ ����
    private ObjectPool<GameObject> _MonsterPool;
    

    private void Start()
    {
        
        _MonsterPool = new ObjectPool<GameObject>(CreatMonster, GetMonster, OnMonsterRelease, OnDestroyMonster);
    }
    [ContextMenu("Creat Monster")]
    //���� ����
    private GameObject CreatMonster()
    {
        //�������� �ν��Ͻ��� ��ȯ�Ͽ� ��ȯ
        return Instantiate(_MonsterPrefab);
    }
    //Ǯ���� ���͸� ���� �� ȣ��
    private void GetMonster(GameObject monster)
    {
        monster.SetActive(true);
    }
    //��ü�� Ǯ�� ���� ��
    private void OnMonsterRelease(GameObject monster)
    {
        monster.SetActive(false);
    }
    //Ǯ�� ũ�⸦ �ʰ��Ͽ��� �� ���͸� ����
    private void OnDestroyMonster(GameObject monster)
    {
        Destroy(monster);
    }

}

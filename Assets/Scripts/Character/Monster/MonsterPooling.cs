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
        //ObjectPool ������
        //�ʱ� 10�� ����
        //�ִ� 30������ Ǯ�� ����
        _MonsterPool = new ObjectPool<GameObject>(CreatMonster, null, OnMonsterRelease, OnDestroyMonster,true, defaultCapacity: 10, maxSize: 30);
        //10�� ����
        for (int i = 0; i < 10; i++)
        {
            var monster = CreatMonster(); //����Ǯ���� ������
            _MonsterPool.Release(monster); //�ٷ� ����
        }
    }
    //���� ����
    private GameObject CreatMonster()
    {
        //�������� �ν��Ͻ��� ��ȯ�Ͽ� ��ȯ
        return Instantiate(_MonsterPrefab);
    }
    ////Ǯ���� ���͸� ���� �� ȣ��
    //private void GetMonster()
    //{
        
    //}

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
    [ContextMenu("Creat Monster")]
    public GameObject SpawnMonster()
    {
        GameObject monster = _MonsterPool.Get();
        monster.SetActive(true);
        return monster;
    }




    /* ������Ʈ Ǯ��: ������Ʈ Ǯ���� ������ ���� �κп��� ������ �ִ�.
     *  1)�� �޸� ȿ����: Instantiate�� ȣ��������� ���� �޸𸮸� �Ҵ��ϴµ�, �̶� ������Ʈ Ǯ���� ���� �ʿ��� �޸𸮸� �̸� �Ҵ������μ� ����Ǵ� �޸� ������ ���� �� �ִ�. 
     *  2)���� ���� ����: Instantiate�� �ܼ� ���簡 �ƴ϶� ���� ������Ʈ�� Transform, SpriteRenderer���� ��� ������Ʈ�� �����Ͽ� ������ ���ο� ��ü�� ����� �ȴ�. �̷��� ���� ���� �۾��� ���� �۾����� ���� ���ҽ��� �ʿ�� ������ ������Ʈ Ǯ���� ���ؼ� �̷��� �۾��� �ʿ��� ��ŭ �̸��صξ� ���� ���� �߿� ��� ����� ���� �� �ִ�.
     *  3)Destroy�� �������� ���� �� ��ü�� �����ϰ� �Ǵµ�, �̶� Destroy�� ������Ʈ�� ���̰� �Ǹ�(�������� ť�� ���̰� �Ǹ�) �������� Ƣ�� �ȴ�. ������Ʈ Ǯ���� ������Ʈ�� ������ �� ������Ʈ�� ��Ȱ��ȭ �ϰ� �����ν� ����� �����Ѵ�.
     *  4)�ʱ�ȭ ���� ����: ������Ʈ Ǯ���� ������� �ʰ� ������Ʈ�� �ı��� ��� �� ������Ʈ�� �����ϴ� AI, HP ���� ������ �Բ� �ı��ϰ� ������ ������Ʈ Ǯ���� ����� ��� ��ü ���θ� �ʱ�ȭ�ϴ� �͸����� ������ �� �� �־� ����� ���� �� �ִ�.
     *  5)Garbage Collection ����� ����: Garbage Collection�� ���̻� ������� �ʴ� ��ü�� �ڵ����� �����Ͽ� �޸� �������� �����ϴ� �ý������� ��������� �۵��ϱ� ������ �׿��� �����ϰ� �Ǹ� �Ͻ������� ���ӿ� �δ��� �� �� �ִ�. ������ ��ü�� �������� �ʰ� �ʱ�ȭ �� �ٽ� ����ϰ� �Ǹ� ������ ���ϸ� ���� �� �ִ�.
     * �����: ����Ƽ ObjectPool
     * ������Ȳ �� �ذ����: GC(Garbage Collector)�� ������ ����, �������� ������ ���͵��� ������ �ʱ�ȭ ���� �ݺ����� ���� ��-->UnityEngine.Pool.ObjectPool<T> API�� �н� �� ����
     * �����
     *  1)ObjectPool<T>�� �ܼ��� ���� ������ �ƴ�, GC �߻� ���� �� ����ȭ ������ �ʿ伺�� �����ϰ� �̸� ������ 
     *  2)���� ����, �񵿱�� ó�� �� � ���� ���ظ� Ȯ���Ͽ� ����ȭ�� �ʿ伺�� �н���
     * ������ �κ�: C#�����ڿ� ���� ����, ���� ���翡 ���� ����
     * ���� ������ ��: ���� �ڵ忡���� �ܼ��� ������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ�� �ϰ� �ִ�, ���Ŀ��� �̸� �����Ͽ� Ȱ��ȭ �� �Բ� ������ �ڵ带 GetMonster()�� �߰��Ͽ� �ڵ带 �� ȿ�������� ���� ��ȹ�̴�. ���� 
     * 
     */
}

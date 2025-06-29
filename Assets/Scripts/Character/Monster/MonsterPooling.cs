using UnityEngine;
using UnityEngine.Pool;

public class MonsterPooling : MonoBehaviour
{
    //몬스터 프리팹을 받을 게임 오브젝트 선언
    [SerializeField] private GameObject _MonsterPrefab;
    //유니티 오브젝트 풀 선언
    private ObjectPool<GameObject> _MonsterPool;
    

    private void Start()
    {
        
        _MonsterPool = new ObjectPool<GameObject>(CreatMonster, GetMonster, OnMonsterRelease, OnDestroyMonster);
    }
    [ContextMenu("Creat Monster")]
    //몬스터 생성
    private GameObject CreatMonster()
    {
        //프리팹을 인스턴스로 변환하여 반환
        return Instantiate(_MonsterPrefab);
    }
    //풀에서 몬스터를 꺼낼 때 호출
    private void GetMonster(GameObject monster)
    {
        monster.SetActive(true);
    }
    //객체를 풀에 넣을 때
    private void OnMonsterRelease(GameObject monster)
    {
        monster.SetActive(false);
    }
    //풀의 크기를 초과하였을 때 몬스터를 삭제
    private void OnDestroyMonster(GameObject monster)
    {
        Destroy(monster);
    }

}

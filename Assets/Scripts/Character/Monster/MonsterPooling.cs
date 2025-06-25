using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterPooling : MonoBehaviour
{
    [SerializeField] private GameObject _MonsterPrefab;
    private ObjectPool<GameObject> _MonsterPool;

    private void Awake()
    {
        _MonsterPool = new ObjectPool<GameObject>(CreatMonster);
    }
    private GameObject CreatMonster()
    {
        return Instantiate(_MonsterPrefab);

    }
    private void GetMonster()
    {

    }
}

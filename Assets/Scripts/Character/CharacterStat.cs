using UnityEngine;
using UnityEngine.Events;

public class CharacterStat : MonoBehaviour
{
    public UnityEvent<int> GetHit;
    
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    private bool isAlive = true;

    private int Health
    {
        get { return _health; }
        set { _health = _health - value; }
    }

    private void Awake()
    {
        // 코드로 이벤트 연결
        GetHit.AddListener(HealthCahange);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable enemy = collision.gameObject.GetComponent<Damageable>();

        GetHit.Invoke(enemy.damage);
    }
    private void OnDestroy()
    {
        // 구독 해제
        GetHit.RemoveListener(HealthCahange);
    }
    
    private void HealthCahange(int damage)
    {
        Health = damage;
    }
    
}

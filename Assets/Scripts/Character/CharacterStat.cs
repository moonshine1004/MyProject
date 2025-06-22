using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterStat : MonoBehaviour
{
    public UnityEvent<int> GetHit;
    
    [SerializeField] private int _maxHealth=100;
    [SerializeField] private int _health;
    private bool isAlive = true;

    public Slider healthSlider;

    private int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private void Start()
    {
        _health = _maxHealth;
        healthSlider.maxValue = _maxHealth; //healthSlider �ʱ�ȭ
    }
    private void Awake()
    {
        // �ڵ�� �̺�Ʈ ����
        GetHit.AddListener(TakeDamage);
        
    }
    private void Update()
    {
        healthSlider.value = _health; //healthSlider��ȭ
    }
    private void OnDestroy()
    {
        // ���� ����
        GetHit.RemoveListener(TakeDamage);
    }
    
    //�������� ���� �� �߻��ϴ� ��Ȳ ��� �־��
    public void TakeDamage(int damage)
    {
        Health -= damage; //Health ������Ƽ�� _health-damage
        healthSlider.value=_health; //healthSlider��ȭ
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�浹�ϴ� ������Ʈ�� Damageabla ������Ʈ�� ������
        Damageable hit = collision.gameObject.GetComponent<Damageable>();
        //GetHit �̺�Ʈ�� �������� �ŰԺ����� �Ͽ� �̺�Ʈ
        GetHit.Invoke(hit.damage);
    }
}

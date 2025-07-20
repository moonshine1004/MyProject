using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ProjectileLauncher : MonoBehaviour
{
    //투사체 발사에 관한 스크립트입니다.

    [SerializeField] private UsingCardList _usingCardList;
    [SerializeField] private PlayerController _playerController;
    
    [Header("풀링 시스템")]
    [SerializeField] GameObject _projectile;
    private ObjectPool<GameObject> _pool;

    [Header("투사체 설정")]
    [Range(0f, 7f)] public float lifetime;
    public float speed = 5.0f;

    private Camera _camera;

    [SerializeField] public Sprite[] _sprites = new Sprite[12];

    private Dictionary<int, Sprite> _dictionary;

    private void Start()
    {
        _camera = Camera.main;
        _dictionary = new Dictionary<int, Sprite>();

        //ObjectPool 생성자
        //초기 10개 생성
        //최대 30개까지 풀에 저장
        _pool = new ObjectPool<GameObject>(CreatProjectile, null, OnProjectileRelease, OnDestroyProjectile, true, defaultCapacity: 10, maxSize: 30);
        //10개 생성
        for (int i = 0; i < 10; i++)
        {
            var projectile = CreatProjectile(); //프로젝타일 풀을 만들어서 꺼내옴
            _pool.Release(projectile); //바로 넣음
        }
        //딕셔너리 12칸 생성
        for (int i = 1; i <= 12; i++)
        {
            _dictionary[i] = null; // Add → 대입으로 변경
        }

        for (int i = 1; i <= _sprites.Length; i++)
        {
            if (_sprites[i] != null)
            {
                _dictionary[i] = _sprites[i]; // 덮어쓰기
            }
        }
    }
    //스킬 입력 이벤트가 입력되면 발사를 실행하는 스크립트
    public void OnSkillInput(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            Shoot();
        }
    }
    //투사체 발사 스크립트
    private void Shoot()
    {
        //오브젝트 풀에서 투사체를 꺼내옴
        GameObject projectile = _pool.Get();
        RenderProjectileSprite(projectile, _usingCardList.hand[_playerController.NowSkillIndex].CardID);



        //커서 방향 발사를 위한 커서의 위치(스크린 좌표를 뭘드 좌표로 변환)
        Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;
        //투사체 발사가 시작되는 지점을 객체의 위치로 지정
        Vector3 startPos = transform.position;
        //커서 위치와 객체 위치를 노말라이제이션하여 방향 설정
        Vector3 direction = (mouseWorldPos - startPos).normalized;
        
        //투사체 시작 위치 초기화
        projectile.transform.position = startPos;
        //투사체에서 Damageable 컴포넌트를 가져와 데미지 설정
        projectile.GetComponent<Damageable>().damage = _usingCardList.GetDamage(_playerController.NowSkillIndex);
        //쿼터니언을 통해 투사체 스프라이트 방향 지정
        projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        projectile.SetActive(true);
        //방향과 속도를 곱해서 투사체 발사(lionearVelocity값을 한 번 지정하면 유지됨으로 update 필요없음)
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _pool.Release(gameObject);
    }
    private void RenderProjectileSprite(GameObject sprite, int i)
    {
        SpriteRenderer renderer = sprite.GetComponent<SpriteRenderer>();
        renderer.sprite = _dictionary[i];
    }
    


    private GameObject CreatProjectile()
    {
        return Instantiate(_projectile);
    }
    private void OnProjectileRelease(GameObject projectile)
    {
        projectile.SetActive(false);
    }
    private void OnDestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
    }
}

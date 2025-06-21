using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //컴포넌트
    private Rigidbody2D _rb;
    private Animator _animator;
    //변수
    [SerializeField] private float moveSpeed; //이동 속도
    //위치값
    private Vector3 screenPos; //마우스 클릭된 스크린 위치 값
    private Vector3 worldPos; //스크린 위치값을 월드 위치로 변환
    private Vector3 targetPos =Vector3.zero; //목표 위치
    //이동 상태
    bool isMoving = false;

    //개념 정리용 가짜 메서드
    private void ForStudy()
    {
        //인풋 액션을 매개변수로 받는 메서드는 public으로 선언해야 Input System으로부터 이벤트를 받아왔을 때 메서드를 실행할 수 있음?
        //애니메이터를 실행할 때 애니메이터의 파라메터
    }
    private void Start()
    {
        //컴포넌트 겟
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        worldPos = transform.position;
    }
    private void Update()
    {
        if (isMoving)
        {
            //현재 위치와 목표 위치를 노말라이제이션하여 방향 계산
            Vector2 currentPos = _rb.position;
            Vector2 direction = ((Vector2)targetPos - currentPos).normalized;
            //목표 위치까지의 거리 계산(거리에 따라 이동 여부 변환)
            float distance = Vector2.Distance(currentPos, targetPos);
            if (distance < 0.1f) //도착하면
            {
                _rb.linearVelocity = Vector2.zero; //속도 0
                isMoving = false; //이동 해제
                _animator.SetBool(PlayerAnimatorCore.isMoving, false); //애니메이터 파라메터 변환
            }
            else //도착하지 않았으면
            {
                OnSetDirection();
                _rb.linearVelocity = direction * moveSpeed; //방향과 속도 곱함
                _animator.SetBool(PlayerAnimatorCore.isMoving, true); //애니메이터 파라메터 변환
            }
        }
    }
    //클릭 기반 이동 메서드
    public void OnClickMove(InputAction.CallbackContext callback)
    {
        //이벤트를 받아오면 실행되는 함수
        //이벤트는 인풋 액션 시스템의 플레이어 컨트롤러의 우클릭에 설정
        screenPos = Mouse.current.position.ReadValue(); //마우스가 클릭 된 화면(로컬) 위치 정보를 저장
        worldPos = Camera.main.ScreenToWorldPoint(screenPos); //화면 위치 정보를 월드 위치로 변환
        worldPos.z = 0;
        targetPos = worldPos; //타겟 위치를 월드 위치로 변환
        isMoving = true; //이동 해제
    }
    //방향 전환 메서드
    private void OnSetDirection()
    {
        //캐릭터 방향 변수 --> 더 효율적인 방법 질문
        Vector3 scale = transform.localScale;
        if (transform.localScale.x > 0 && targetPos.x < 0)
        {
            scale.x *= -1;
        }
        else if (transform.localScale.x < 0 && targetPos.x > 0)
        {
            scale.x *= -1;
        }
        transform.localScale = scale;
    }
    //스킬 발동 메서드들-->하나로 관리할 수 있도록 수정 요망
    //공격 트리거를 온, 각 공격에 해당하는 키를 온
    public void OnQSkill(InputAction.CallbackContext callback)
    {
        _animator.SetTrigger(PlayerAnimatorCore.AttackInput);
        _animator.SetTrigger(PlayerAnimatorCore.InputQ);
    }
    public void OnWSkill(InputAction.CallbackContext callback)
    {
        _animator.SetTrigger(PlayerAnimatorCore.AttackInput);
        _animator.SetTrigger(PlayerAnimatorCore.InputW);
    }
    public void OnESkill(InputAction.CallbackContext callback)
    {
        _animator.SetTrigger(PlayerAnimatorCore.AttackInput);
        _animator.SetTrigger(PlayerAnimatorCore.InputE);
    }
    public void OnRSkill(InputAction.CallbackContext callback)
    {
        _animator.SetTrigger(PlayerAnimatorCore.AttackInput);
        _animator.SetTrigger(PlayerAnimatorCore.InputR);
    }
    public void OnTSkill(InputAction.CallbackContext callback)
    {
        _animator.SetTrigger(PlayerAnimatorCore.AttackInput);
        _animator.SetTrigger(PlayerAnimatorCore.InputT);
    }


}

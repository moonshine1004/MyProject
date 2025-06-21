using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //������Ʈ
    private Rigidbody2D _rb;
    private Animator _animator;
    //����
    [SerializeField] private float moveSpeed; //�̵� �ӵ�
    //��ġ��
    private Vector3 screenPos; //���콺 Ŭ���� ��ũ�� ��ġ ��
    private Vector3 worldPos; //��ũ�� ��ġ���� ���� ��ġ�� ��ȯ
    private Vector3 targetPos =Vector3.zero; //��ǥ ��ġ
    //�̵� ����
    bool isMoving = false;

    //���� ������ ��¥ �޼���
    private void ForStudy()
    {
        //��ǲ �׼��� �Ű������� �޴� �޼���� public���� �����ؾ� Input System���κ��� �̺�Ʈ�� �޾ƿ��� �� �޼��带 ������ �� ����?
        //�ִϸ����͸� ������ �� �ִϸ������� �Ķ����
    }
    private void Start()
    {
        //������Ʈ ��
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        worldPos = transform.position;
    }
    private void Update()
    {
        if (isMoving)
        {
            //���� ��ġ�� ��ǥ ��ġ�� �븻�������̼��Ͽ� ���� ���
            Vector2 currentPos = _rb.position;
            Vector2 direction = ((Vector2)targetPos - currentPos).normalized;
            //��ǥ ��ġ������ �Ÿ� ���(�Ÿ��� ���� �̵� ���� ��ȯ)
            float distance = Vector2.Distance(currentPos, targetPos);
            if (distance < 0.1f) //�����ϸ�
            {
                _rb.linearVelocity = Vector2.zero; //�ӵ� 0
                isMoving = false; //�̵� ����
                _animator.SetBool(PlayerAnimatorCore.isMoving, false); //�ִϸ����� �Ķ���� ��ȯ
            }
            else //�������� �ʾ�����
            {
                OnSetDirection();
                _rb.linearVelocity = direction * moveSpeed; //����� �ӵ� ����
                _animator.SetBool(PlayerAnimatorCore.isMoving, true); //�ִϸ����� �Ķ���� ��ȯ
            }
        }
    }
    //Ŭ�� ��� �̵� �޼���
    public void OnClickMove(InputAction.CallbackContext callback)
    {
        //�̺�Ʈ�� �޾ƿ��� ����Ǵ� �Լ�
        //�̺�Ʈ�� ��ǲ �׼� �ý����� �÷��̾� ��Ʈ�ѷ��� ��Ŭ���� ����
        screenPos = Mouse.current.position.ReadValue(); //���콺�� Ŭ�� �� ȭ��(����) ��ġ ������ ����
        worldPos = Camera.main.ScreenToWorldPoint(screenPos); //ȭ�� ��ġ ������ ���� ��ġ�� ��ȯ
        worldPos.z = 0;
        targetPos = worldPos; //Ÿ�� ��ġ�� ���� ��ġ�� ��ȯ
        isMoving = true; //�̵� ����
    }
    //���� ��ȯ �޼���
    private void OnSetDirection()
    {
        //ĳ���� ���� ���� --> �� ȿ������ ��� ����
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
    //��ų �ߵ� �޼����-->�ϳ��� ������ �� �ֵ��� ���� ���
    //���� Ʈ���Ÿ� ��, �� ���ݿ� �ش��ϴ� Ű�� ��
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

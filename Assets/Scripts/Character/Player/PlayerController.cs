using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //������Ʈ
    private Rigidbody2D _rb;
    private Animator _animator;
    //����
    [SerializeField] private float _moveSpeed; //�̵� �ӵ�
    //��ġ��
    private Vector3 screenPos; //���콺 Ŭ���� ��ũ�� ��ġ ��
    private Vector3 worldPos; //��ũ�� ��ġ���� ���� ��ġ�� ��ȯ
    private Vector3 targetPos =Vector3.zero; //��ǥ ��ġ
    //�̵� ����
    bool isMoving = false;
    //��ų �ε���
    public int NowSkillIndex=0;
    UsingCardList _UsingCardList;

    
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

                _rb.linearVelocity = direction * _moveSpeed; //����� �ӵ� ����
                _animator.SetBool(PlayerAnimatorCore.isMoving, true); //�ִϸ����� �Ķ���� ��ȯ
            }
        }
        OnSetDirection();
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
    public void OnSkillInput(InputAction.CallbackContext callback)
    {
        _animator.SetBool(PlayerAnimatorCore.OnSkillInput, true);
        var name = callback.control.name; //��ǲ �׼��� ��Ʈ�� �̸��� ������
        _UsingCardList.UseCard(NowSkillIndex); //���� ��ų �ε����� �ش��ϴ� ī�� ���
        //_animator.SetInteger(PlayerAnimatorCore.SkillIndex, _UsingCardList.hand[NowSkillIndex].CardID);

    }
    public enum SkillType
    {
        Q=0, W=1, E=2, R=3, T=4
    }

}

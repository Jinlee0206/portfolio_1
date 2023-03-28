using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public enum eState
    {
        GROUNDED,
        STANDING,
        CROUCH,
        SPRINT,
        JUMPING,
        COMBOATK1,
        COMBOATK2,
        COMBOATK3,
        COMBOATK4,
        SKILL1,
        SKILL2,
        JUMPATK,
        ROLL,
        DOWN,
    }

    [HideInInspector]
    public pStateMachine m_playerSM;

    public Dictionary<eState, pState> m_pStates = new Dictionary<eState, pState>();

    [SerializeField]
    public Transform cameraTransform;
    [SerializeField]
    public Rigidbody pRigidbody;
    [SerializeField]
    public CharacterController characterController;
    [SerializeField]
    public Animator pAnim;
    [HideInInspector]
    public Transform playerTr;
    [HideInInspector]
    public Transform dragonTr;
    [HideInInspector]
    public PlayerWeapon weapon;
    
    public Vector3 moveDirection = Vector3.zero;


    public float pWalkSpeed = 3.0f;
    public float pCrouchSpeed = 1.5f;
    public float pRunSpeed = 5.0f;


    [HideInInspector]
    public float animExitTime = 0.95f;
    [HideInInspector]
    public string curAnimName;
    [HideInInspector]
    public bool animName => pAnim.GetCurrentAnimatorStateInfo(0).IsName(curAnimName);
    [HideInInspector]
    public float animTime => pAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;


    #region Properties

    private int horizonalMoveParam = Animator.StringToHash("H_Speed");
    private int verticalMoveParam = Animator.StringToHash("V_Speed");

    public int hashCrouchParam => Animator.StringToHash("Crouch");
    public int hashSprintParam => Animator.StringToHash("Sprint");
    public int hashComboAtk1 => Animator.StringToHash("ComboAtk1");
    public int hashComboAtk2 => Animator.StringToHash("ComboAtk2");
    public int hashComboAtk3 => Animator.StringToHash("ComboAtk3");
    public int hashComboAtk4 => Animator.StringToHash("ComboAtk4");
    public int hashSkill1 => Animator.StringToHash("Skill1");
    public int hashSkill2 => Animator.StringToHash("Skill2");
    public int hashJumpAtk => Animator.StringToHash("JumpAtk");
    public int hashRollBack => Animator.StringToHash("RollBack");
    public int hashDown => Animator.StringToHash("Down");
    #endregion

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pAnim = GetComponentInChildren<Animator>();
        pRigidbody = GetComponent<Rigidbody>();
        playerTr = GetComponent<Transform>();
        dragonTr = GameObject.FindWithTag("DRAGON").GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        m_playerSM = new pStateMachine();

        m_pStates.Add(eState.GROUNDED, new PlayerState_GROUNDED(this, m_playerSM));
        m_pStates.Add(eState.STANDING, new PlayerState_STANDING(this, m_playerSM));
        m_pStates.Add(eState.CROUCH, new PlayerState_CROUCH(this, m_playerSM));
        m_pStates.Add(eState.SPRINT, new PlayerState_SPRINT(this, m_playerSM));
        //m_pStates.Add(eState.JUMPING, new PlayerState_JUMPING(this, m_playerSM));
        m_pStates.Add(eState.COMBOATK1, new PlayerState_COMBOATK1(this, m_playerSM));
        m_pStates.Add(eState.COMBOATK2, new PlayerState_COMBOATK2(this, m_playerSM));
        m_pStates.Add(eState.COMBOATK3, new PlayerState_COMBOATK3(this, m_playerSM));
        m_pStates.Add(eState.COMBOATK4, new PlayerState_COMBOATK4(this, m_playerSM));
        m_pStates.Add(eState.SKILL1, new PlayerState_SKILL1(this, m_playerSM));
        m_pStates.Add(eState.SKILL2, new PlayerState_SKILL2(this, m_playerSM));
        m_pStates.Add(eState.JUMPATK, new PlayerState_JUMPATK(this, m_playerSM));
        m_pStates.Add(eState.ROLL, new PlayerState_ROLL(this, m_playerSM));
        m_pStates.Add(eState.DOWN, new PlayerState_DOWN(this, m_playerSM));

        m_playerSM.Initialize(m_pStates[eState.STANDING]);
    }

    private void Update()
    {
        m_playerSM.CurrentState.LogicUpdate();
    }

    private void LateUpdate()
    {
        m_playerSM.CurrentState.PhysicsUpdate();
    }

    public void SetAnimationBool(int param, bool value)
    {
        pAnim.SetBool(param, value);
    }

    public void TriggerAnimation(int param)
    {
        pAnim.SetTrigger(param);
    }

    public void CheckAnimationState(string command)
    {
        if (animName && animTime >= animExitTime)
        {
            m_playerSM.ChangeState(m_pStates[eState.STANDING]);
        }
    }
    
    public void Move(float x, float z)
    {
        moveDirection = new Vector3(x, 0, z);

        moveDirection.Normalize();

        MoveTo(cameraTransform.rotation * new Vector3(x, 0, z));
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        pAnim.SetFloat(horizonalMoveParam, x);
        pAnim.SetFloat(verticalMoveParam, z);

        characterController.SimpleMove(moveDirection);
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
    }


    public void ResetMoveParams()
    {
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        pAnim.SetFloat(horizonalMoveParam, 0f);
        pAnim.SetFloat(verticalMoveParam, 0f);
    }

    public void ComboAtkCheck(eState state, string _animName)
    {
        curAnimName = _animName;

        if(animName && animTime > 0.6f && Input.GetMouseButtonDown(0))
        {
            m_playerSM.ChangeState(m_pStates[state]);
        }

        else if (animName && animTime > animExitTime)
        {
            m_playerSM.ChangeState(m_pStates[eState.STANDING]);
        }
    }

    public void ColliderActive(PlayerCtrl player, string _animName)
    {
        curAnimName = _animName;

        if (animName && animTime > 0.2f && animTime <= 0.45f)
        {
            weapon.meleeArea.enabled = true;
        }

        else if (animName && animTime > 0.5f)
        {
            weapon.meleeArea.enabled = false;
        }

    }


}

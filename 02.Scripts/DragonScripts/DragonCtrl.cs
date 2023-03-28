using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DragonCtrl : MonoBehaviour
{
    public enum eState
    {
        IDLE,
        AI,
        TRACE,
        ATTACK_LCLAW,
        ATTACK_RFCLAW,
        ATTACK_BITE,
        ATTACK_BRESS,
        FLY,
        ATTACK_SPITFIRE,
        LAND,
        STEPBACK,
        DIE
    }

    [HideInInspector]
    public StateMachine m_dragonSM;

    public Dictionary<eState, State> m_states = new Dictionary<eState, State>();

    [HideInInspector]
    public Transform dragonTr;
    [HideInInspector]
    public Transform playerTr;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public float animExitTime = 0.95f;
    [HideInInspector]
    public string curAnimName;
    [HideInInspector]
    public bool animName => anim.GetCurrentAnimatorStateInfo(0).IsName(curAnimName);
    [HideInInspector]
    public float animTime => anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

    #region GetHit
    Rigidbody dragonRB;
    BoxCollider dragonBC;
    Material dragonMat;
    Light light;

    public int maxHealth;
    public int curHealth;
    #endregion

    private Vector3 startPosition { get; set; }
    private Vector3 startRotation { get; set; }


    private float attackRadius = 2.0f;
    private float dragonSpeed = 4.0f;
    [HideInInspector]
    public float attackDist;
    [HideInInspector]
    public float traceDist = 20.0f;
    [HideInInspector]
    public float fieldOfView = 50f;
    [HideInInspector]
    public float viewDistance = 10f;

    public Transform attackRoot;
    public Transform pulsePos;
    public Transform bressPos;
    public Transform stepBackPos;

    [HideInInspector]
    public DragonMovePattern pattern;
    
    public int patternIdx;

    [HideInInspector]
    public bool isPatterning = false;
    [HideInInspector]
    public bool isReady = false;
    [HideInInspector]
    public bool isMeleeState = false;
    [HideInInspector]
    public bool isFlying = false;

    public float distance => Vector3.Distance(playerTr.position, dragonTr.position);

    public int hashTrace => Animator.StringToHash("isTrace");
    public int hashDragonAtkA => Animator.StringToHash("DragonAtk_Jbite");
    public int hashDragonAtkB => Animator.StringToHash("DragonAtk_Lclaw");
    public int hashDragonAtkC => Animator.StringToHash("DragonAtk_RFclaw");
    public int hashSniffleAround => Animator.StringToHash("SniffleAround");
    public int hashBress => Animator.StringToHash("DragonAtk_Bress");
    public int hashisFlyB => Animator.StringToHash("isFlyB");
    public int hashDragonAtkSpitFire => Animator.StringToHash("DragonAtk_SpitFire");
    public int hashisLand => Animator.StringToHash("isLand");
    public int hashStepBack => Animator.StringToHash("StepBack");
    public int hashDie => Animator.StringToHash("Die");




    private void Awake()
    {
        startPosition = this.transform.position;
        startRotation = this.transform.eulerAngles;

        pattern = this.transform.GetComponent<DragonMovePattern>();
        dragonTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        dragonRB = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        dragonBC = GetComponentInChildren<BoxCollider>();
        dragonMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        light = GameObject.FindWithTag("LIGHT").GetComponent<Light>();

        attackDist = Vector3.Distance(transform.position, new Vector3(attackRoot.position.x, transform.position.y, attackRoot.position.z)) + attackRadius;
        attackDist += agent.radius;

        agent.stoppingDistance = attackDist;
        agent.speed = dragonSpeed;

        patternIdx = 0;
    }


    private void Start()
    {
        m_dragonSM = new StateMachine();

        m_states.Add(eState.IDLE, new DragonState_IDLE(this, m_dragonSM));
        m_states.Add(eState.AI, new DragonState_AI(this, m_dragonSM));
        m_states.Add(eState.TRACE, new DragonState_TRACE(this, m_dragonSM));
        m_states.Add(eState.ATTACK_LCLAW, new DragonState_ATTACK_LCLAW(this, m_dragonSM));
        m_states.Add(eState.ATTACK_RFCLAW, new DragonState_ATTACK_RFCLAW(this, m_dragonSM));
        m_states.Add(eState.ATTACK_BITE, new DragonState_ATTACK_BITE(this, m_dragonSM));
        m_states.Add(eState.FLY, new DragonState_FLY(this, m_dragonSM));
        m_states.Add(eState.ATTACK_SPITFIRE, new DragonState_ATTACK_SPITFIRE(this, m_dragonSM));
        m_states.Add(eState.LAND, new DragonState_LAND(this, m_dragonSM));
        m_states.Add(eState.ATTACK_BRESS, new DragonState_ATTACK_BRESS(this, m_dragonSM));
        m_states.Add(eState.STEPBACK, new DragonState_STEPBACK(this, m_dragonSM));
        m_states.Add(eState.DIE, new DragonState_DIE(this, m_dragonSM));

        m_dragonSM.Initialize(m_states[eState.AI]);

        
    }

    private void Update()
    {
        m_dragonSM.CurrentState.LogicUpdate();
        //LightOff();
    }

    private void LateUpdate()
    {
        m_dragonSM.CurrentState.PhysicsUpdate();
        FreezeVelocity();
    }

    // 충돌 시 물리적인 충돌이 NavMesh에 영향이 없게
    private void FreezeVelocity()
    {
        dragonRB.velocity = Vector3.zero;
        dragonRB.angularVelocity = Vector3.zero;
    }

    public void SetAnimationBool(int param, bool value)
    {
        anim.SetBool(param, value);
    }

    public void TriggerAnimation(int param)
    {
        anim.SetTrigger(param);
    }

    public void CheckAnimationState(string command)
    {
        if (animName && animTime >= animExitTime)
        {
            if (!isFlying)
                m_dragonSM.ChangeState(m_states[eState.AI]);
            else
                m_dragonSM.ChangeState(m_states[eState.FLY]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MELEE")
        {
            ShakeCamera.Instance.OnShakeCamera(0.1f, 0.5f);
            PlayerWeapon weapon = other.GetComponent<PlayerWeapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;

            StartCoroutine(OnDamage(reactVec));
            Debug.Log("Melee : " + curHealth);
        }
    }

    IEnumerator OnDamage(Vector3 reactVect)
    {
        yield return new WaitForSeconds(1f);

        if (curHealth > 0)
        {
            dragonMat.color = Color.white;
        }
        else
        {
            dragonMat.color = Color.grey;
            if (m_dragonSM.CurrentState != m_states[DragonCtrl.eState.DIE])
                m_dragonSM.ChangeState(m_states[eState.DIE]);
        }
    }

    public void LightOff()
    {
        light.gameObject.SetActive(false);
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        if (attackRoot != null)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            Gizmos.DrawSphere(attackRoot.position, attackRadius);
        }

        var leftRayRotation = Quaternion.AngleAxis(-fieldOfView * 0.5f, Vector3.up);
        var leftRayDirection = leftRayRotation * transform.forward;
        Handles.color = new Color(1f, 1f, 1f, 0.2f);
        Handles.DrawSolidArc(attackRoot.position, Vector3.up, leftRayDirection, fieldOfView, viewDistance);
    }

#endif

}

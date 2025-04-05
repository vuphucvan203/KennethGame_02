using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum StateAction
{
    Attack,
    Move,
}

public enum AttackType
{
    Melee,
    SmallAcid,
    BigAcid,
    Stun,
    Fire,
    BloodTalon,
    TwinTalons
}

public abstract class EnemyAI : KennMonoBehaviour
{
    [SerializeField] protected Enemy enemy;
    [SerializeField] protected EnemyStateTrigger stateTrigger;
    [SerializeField] protected StateAction action;
    [SerializeField] protected AttackType attack;
    public AttackType AttackType => attack;
    [SerializeField] protected float timer, cooldown;
    protected Vector3 directionTarget;
    protected Transform target;
    protected bool detected = false;
    public bool startCooldown { get; set; }
    public float patrolRadius = 10f;
    public int speed = 2;
    public int rotationSpeed = 5;


    protected override void Start()
    {
        base.Start();
        action = StateAction.Move;
        directionTarget = GetRandomPoint();
    }

    private void Update()
    {
        MakeDecision();
        MoveExcute();
        AttackExcute();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        enemy = GetComponent<Enemy>();
        stateTrigger = GetComponent<EnemyStateTrigger>();
    }

    protected virtual void MakeDecision()
    {
        StartCooldown(cooldown);
    }

    protected void MoveExcute()
    {
        if(detected)
        {
            FollowTarget();
        }
        else
        {
            MoveToNewDirection(directionTarget);
            if (Vector3.Distance(transform.position, directionTarget) < 0.1f)
            {
                directionTarget = GetRandomPoint();
                MoveToNewDirection(directionTarget);
            }
        } 
            
        if (action == StateAction.Move) stateTrigger.ActiveMove();
    }

    protected void AttackExcute()
    {
        if (action == StateAction.Attack && detected)
        {
            enemy.currentAttack = attack;
            stateTrigger.ActiveAttack();
        }
        else action = StateAction.Move;
    }

    public void StartCooldown(float cooldown)
    {
        if (startCooldown)
        {
            action = StateAction.Move;
            timer += Time.deltaTime;
            if (timer > cooldown)
            {
                action = StateAction.Attack;
                startCooldown = false;
                timer = 0;
            }
        }
    }

    protected void MoveToNewDirection(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        Vector3 direction = transform.position - targetPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    protected void FollowTarget()
    {
        if (!detected) return;
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 3f)
        {
            action = StateAction.Attack;
            return;
        }
        MoveToNewDirection(target.position);
    }

    protected Vector3 GetRandomPoint()
    {
        Vector3 randomPoint = transform.position + new Vector3(
        Random.Range(-patrolRadius, patrolRadius),
        Random.Range(-patrolRadius, patrolRadius), 0);
        return randomPoint;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BodyCollider>(out BodyCollider body))
        {
            if (body.colliderDefind != ColliderType.SoliderBody) return;
            detected = true;
            target = body.transform.parent;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<BodyCollider>(out BodyCollider body))
        {
            if (body.colliderDefind != ColliderType.SoliderBody) return;
            detected = false;
        }
    }

}
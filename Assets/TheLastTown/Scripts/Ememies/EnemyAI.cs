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
    public Enemy Enemy => enemy;
    [SerializeField] protected EnemyStateTrigger stateTrigger;
    [SerializeField] protected StateAction action;
    [SerializeField] protected AttackType attack;
    public AttackType AttackType => attack;
    [SerializeField] protected CheckFront checkFront;
    protected float timer, cooldown; 
    public CheckFront CheckFront => checkFront;
    protected Vector3 directionTarget;
    protected bool detected = false;
    public bool startCooldown { get; set; }
    public LayerMask layerMask;
    public float patrolRadius = 10f;
    public int rotationSpeed = 5;
    public float attackLimit = 3f;


    protected override void Start()
    {
        base.Start();
        action = StateAction.Move;
        directionTarget = GetRandomPoint();
    }

    private void Update()
    {
        if (Enemy.isDeath) return;
        MakeDecision();
        MoveExcute();
        AttackExcute();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        enemy = GetComponent<Enemy>();
        stateTrigger = GetComponent<EnemyStateTrigger>();
        checkFront = GetComponentInChildren<CheckFront>();
    }

    protected virtual void MakeDecision()
    {
        StartCooldown(cooldown);
    }

    protected void MoveExcute()
    {
        Collider2D areaDetection = Physics2D.OverlapCircle(transform.position, patrolRadius, layerMask);
        if (checkFront && checkFront.HasObstacle)
        {
            directionTarget = GetRandomPoint();
            MoveToNewDirection(directionTarget);
        } 
            
        if (areaDetection != null)
        {
            detected = true;
            FollowTarget(areaDetection.transform.position);
        }
        else 
        {
            detected = false;
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
                HandleAttackStrategy();
                startCooldown = false;
                timer = 0;
            }
        }
    }

    protected virtual void HandleAttackStrategy()
    {
        action = StateAction.Attack;
    }    

    protected void MoveToNewDirection(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, enemy.SpeedStats.value * Time.deltaTime);
        Vector3 direction = transform.position - targetPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    protected void FollowTarget(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        if (distance < attackLimit)
        {
            action = StateAction.Attack;
        }
        else MoveToNewDirection(target);
    }

    protected Vector3 GetRandomPoint()
    {
        Vector3 randomPoint = transform.position + new Vector3(
        Random.Range(-patrolRadius, patrolRadius),
        Random.Range(-patrolRadius, patrolRadius), 0);
        return randomPoint;
    }

}
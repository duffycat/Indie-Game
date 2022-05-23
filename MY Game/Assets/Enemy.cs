using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : FiniteStateMachine, IInteractable
{
    public Bounds bounds;
    public float viewRadius;
    public Transform player;
    public EnemyIdleState idleState;
    public EnemyWanderState wanderState;
    public EnemyChaseState chaseState;
    public EnemyStunState stunState;
    public GameOnState gameOnState;

    public NavMeshAgent Agent { get; private set; }

    protected override void Awake()
    {
        idleState = new EnemyIdleState(this, idleState);
        wanderState = new EnemyWanderState(this, wanderState);
        chaseState = new EnemyChaseState(this, chaseState);
        stunState = new EnemyStunState(this, stunState);
        gameOnState = new GameOnState(this, gameOnState);
        entryState = wanderState;
        if (TryGetComponent(out NavMeshAgent agent) == true)
        {
            Agent = agent;
        }

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameManger.Instance.Cheese.GotCheeseEvent += GameOn;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

    public void GameOn()
    {
       SetState(gameOnState);
    }

    public void Activate()
    {
        SetState(stunState);
    }
}

public abstract class EnemyBehaviourState : IState
{
    protected Enemy Instance { get; private set; }


    public EnemyBehaviourState(Enemy instance)
    {
        Instance = instance;

    }

    public abstract void OnStateEnter();

    public abstract void OnStateExit();

    public abstract void OnStateUpdate();

    public virtual void DrawStateGizmos() { }
}
[System.Serializable]
public class EnemyIdleState : EnemyBehaviourState
{
    [SerializeField]
    private Vector2 idleTimeRange = new Vector2(3, 10);
    private float timer = -1;
    private float idleTime = 0;

    public EnemyIdleState(Enemy instance, EnemyIdleState idle) : base(instance) 
    {
        idleTimeRange = idle.idleTimeRange;
    }

    public override void OnStateEnter() 
    { 
        Instance.Agent.isStopped = true;
        idleTime = Random.Range(idleTimeRange.x, idleTimeRange.y);
        timer = 0;
 //       Debug.Log("idle waiting for " + idleTime + " seconds");
    }

    public override void OnStateExit() 
    { 
        timer = -1;
        idleTime = 0;
        //Debug.Log("Exiting idle state");
    }

    public override void OnStateUpdate() 
    {
        if (Vector3.Distance(Instance.transform.position, Instance.player.position) <= Instance.viewRadius)
        {
            if (Instance.CurrentState.GetType() != typeof(EnemyChaseState))
            {
               Instance.SetState(Instance.chaseState);
            }
        }

        if (timer >= 0) 
        {
            timer += Time.deltaTime;
            if(timer >= idleTime) 
            {
 //              Debug.Log("Exiting Idle State after " + idleTime + " seconds");
               Instance.SetState(Instance.wanderState);
            }
        }
    }
}

[System.Serializable]
public class EnemyWanderState : EnemyBehaviourState
{
    private Vector3 targetPosition;
    [SerializeField]
    private float wanderSpeed = 3.5f;

    public EnemyWanderState(Enemy instance, EnemyWanderState wanderState) : base(instance) 
    {
        wanderSpeed = wanderState.wanderSpeed;
    }

    public override void OnStateEnter()
    {
        Instance.Agent.speed = wanderSpeed;
        Instance.Agent.isStopped = false;
        Vector3 randomPosInBounds = new Vector3(
            Random.Range(-Instance.bounds.extents.x, Instance.bounds.extents.x),
            Instance.transform.position.y,
            Random.Range(-Instance.bounds.extents.z, Instance.bounds.extents.z));
        while (Physics.CheckSphere(randomPosInBounds, 0.5f) == true)
        {
            Debug.Log("Before: " + randomPosInBounds);
            randomPosInBounds = new Vector3(
            Random.Range(-Instance.bounds.extents.x, Instance.bounds.extents.x),
            Instance.transform.position.y,
            Random.Range(-Instance.bounds.extents.z, Instance.bounds.extents.z));
            Debug.Log("After: " + randomPosInBounds);
        }
        targetPosition = randomPosInBounds;
        Instance.Agent.SetDestination(targetPosition);
        Debug.Log("target pos " + targetPosition);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        Vector3 t = targetPosition;
        t.y = 0;
        if(Vector3.Distance(Instance.transform.position, targetPosition) <= Instance.Agent.stoppingDistance) 
        {
            Instance.SetState(Instance.idleState);
        }
        else if (Vector3.Distance(Instance.transform.position, Instance.player.position) < Instance.viewRadius)
        {
            Instance.SetState(Instance.chaseState);
        }

    }

    public override void DrawStateGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(targetPosition, 0.5f);
    }
}

[System.Serializable]
public class EnemyChaseState : EnemyBehaviourState
{
    [SerializeField]
    private float chaseSpeed = 5f;

    public EnemyChaseState(Enemy instance, EnemyChaseState chaseState) : base(instance) 
    {
        chaseSpeed = chaseState.chaseSpeed;
    }

    public override void OnStateEnter()
    {
        Instance.Agent.isStopped = false;
        Instance.Agent.speed = chaseSpeed;
        Debug.Log("chase on");
    }

    public override void OnStateExit()
    {
        Debug.Log("chase off");
    }

    public override void OnStateUpdate()
    {
        Instance.Agent.SetDestination(Instance.player.position);

        if(Vector3.Distance(Instance.transform.position, Instance.player.position) > Instance.viewRadius) 
        {
            Instance.SetState(Instance.wanderState);
        }
        Vector3 here = Instance.transform.position - Instance.player.position;
        if (here.x < 1 && here.z < 1 && here.x > -1 && here.z > -1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
    }

}

[System.Serializable]
public class EnemyStunState : EnemyBehaviourState
{
    [SerializeField]
    private float stunTime = 1.5f;

    private float timer = -1;
    private float stunTimer = 0;
    public EnemyStunState(Enemy instance, EnemyStunState stunState) : base(instance)
    {
        stunTime = stunState.stunTime;
    }

    public override void OnStateEnter()
    {
        Debug.Log("dead");


        Instance.Agent.isStopped = true;
        stunTimer = stunTime;
        timer = 0;
    }

    public override void OnStateExit()
    {
        timer = -1;
        stunTimer = 0;
    }

    public override void OnStateUpdate()
    {
        if (timer >= 0)
        {
            timer += Time.deltaTime;
            if (timer >= stunTimer)
            {
                Debug.Log("Exiting Idle State after " + stunTimer + " seconds");
                Instance.SetState(Instance.wanderState);
            }
        }
    }

    public override void DrawStateGizmos()
    {
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(targetPosition, 0.5f);
    }

}


[System.Serializable]
public class GameOnState : EnemyBehaviourState
{
    [SerializeField]
    private float chaseGOSpeed = 5f;

    public GameOnState(Enemy instance, GameOnState gameOnState) : base(instance)
    {
        chaseGOSpeed = gameOnState.chaseGOSpeed;
    }

    public override void OnStateEnter()
    {
        Instance.Agent.isStopped = false;
        Instance.Agent.speed = chaseGOSpeed;
        Debug.Log("Game on");
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        Instance.Agent.SetDestination(Instance.player.position);
        Vector3 here = Instance.transform.position - Instance.player.position;
        if (here.x < 1 && here.z < 1 && here.x > -1 && here.z > -1) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
    }
}
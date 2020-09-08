using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : Agent
{
    #region VARIABLES
    public enum StateEnemy
    {
        NONE,
        PATROL,
        SEEK,
        FLEE
    };
    public enum AttackType
    {
        NONE,
        MELEE_LIGHT,
        MELEE_HEAVY,
        CHARGE,
        AREA
    };

    /////////ENEMY
    NavMeshAgent agent;
    StateEnemy currentState;
    public float lifeThreshold = 30f;
    float maxLife;
    float currentPercentLife;
    AttackType currentAttack;
    public float distanceToRun;

    /////////PATROL ELEMENTS
    public Transform[] patrolPoints;
    int currentPoint;
    public float minDistanceToPoint = 0.5f;

    /////////PLAYER
    Player player;
    Transform playerTransform;
    public float maxDistanceToPlayer = 20f;
    #endregion

    #region START
    void Start()
    {
        ///Set Agent
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        maxLife = life;

        ////Set Patrol
        currentState = StateEnemy.PATROL;
        UpdatePatrolPoint();

        ////Set Player
        GameObject auxPlayer = GameObject.FindGameObjectWithTag("Player");
        if (auxPlayer != null)
        {
            player = auxPlayer.GetComponent<Player>();
            playerTransform = auxPlayer.transform;
        }        
    }
    #endregion
    
    #region FIXED UPDATE
    //Para cada frame
    void FixedUpdate()
    {
        distanceToRun = maxDistanceToPlayer * 0.5f;
        CheckChangeState();

        switch (currentState)
        {
            case StateEnemy.NONE:
                {
                    break;
                } 
            case StateEnemy.PATROL:
                {
                    if ( Vector3.Distance( this.transform.position , patrolPoints[currentPoint].position ) <= minDistanceToPoint)
                    {
                        UpdatePatrolPoint();
                    }
                    break;
                }
            case StateEnemy.SEEK:
                {
                    agent.SetDestination(playerTransform.position);
                    break;
                }
            case StateEnemy.FLEE:
                {
                    if (Vector3.Distance(this.transform.position, patrolPoints[currentPoint].position) <= distanceToRun)
                    {
                        //Sacamos la dirección al player
                        Vector3 dirToplayer = this.transform.position - playerTransform.position;
                        Vector3 newPosFlee = this.transform.position + dirToplayer;

                        agent.SetDestination(newPosFlee);
                    }
                    break;
                }                
            default:
                {
                    break;
                }                
        }
    }
    #endregion

    #region  CHECK CHANGE STATE
    void CheckChangeState()
    {
        switch (currentState)
        {
            case StateEnemy.NONE:
                {
                    break;
                }
            case StateEnemy.PATROL:
                {
                    if (Vector3.Distance(this.transform.position, playerTransform.position) <= maxDistanceToPlayer)
                    {

                        //Current % of Life
                        currentPercentLife = (life / maxLife) * 100;

                        if (currentPercentLife >= lifeThreshold)
                        {
                            Debug.Log("EL ESTADO ACTUAL ES SEEK");
                            currentState = StateEnemy.SEEK;
                            agent.speed = speed * 2;
                        }
                        else
                        {
                            Debug.Log("EL ESTADO ACTUAL ES FLEE");
                            currentState = StateEnemy.FLEE;
                            agent.speed = speed * 2;
                        }
                        
                       
                    }
                    break;
                }
            case StateEnemy.SEEK:
                {
                    //Distance to Player
                    if (Vector3.Distance(this.transform.position, playerTransform.position) >= maxDistanceToPlayer)
                    {
                        Debug.Log("EL ESTADO ACTUAL ES PATROL");
                        currentState = StateEnemy.PATROL;
                        agent.speed = speed;
                        UpdatePatrolPoint();
                    }

                    //Current % of Life
                    currentPercentLife = (life / maxLife) * 100;

                    if (currentPercentLife <= lifeThreshold)
                    {
                        Debug.Log("EL ESTADO ACTUAL ES FLEE");
                        currentState = StateEnemy.FLEE;
                    }

                    break;
                }
            case StateEnemy.FLEE:
                {
                    if (Vector3.Distance(this.transform.position, playerTransform.position) >= maxDistanceToPlayer)
                    {
                        Debug.Log("EL ESTADO ACTUAL ES PATROL");
                        currentState = StateEnemy.PATROL;
                        agent.speed = speed;
                        UpdatePatrolPoint();
                    }

                    //Current % of Life
                    currentPercentLife = (life / maxLife) * 100;

                    if (currentPercentLife >= lifeThreshold)
                    {
                        Debug.Log("EL ESTADO ACTUAL ES SEEK");
                        currentState = StateEnemy.SEEK;
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    #endregion

    #region UPDATE PATROL POINT
    void UpdatePatrolPoint()
    {
        //buscar un siguiente punto
        currentPoint = Random.Range(0, patrolPoints.Length - 1);

        agent.SetDestination(patrolPoints[currentPoint].position);
    }
    #endregion    

    #region ON DRAW GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, maxDistanceToPlayer);
        Gizmos.DrawWireSphere(this.transform.position, distanceToRun);
    }
    #endregion
}

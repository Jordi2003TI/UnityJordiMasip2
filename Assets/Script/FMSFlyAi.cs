using System;
using UnityEngine;
//[RequireComponent typeof(Rigidbody2D)]

public class FMSFlyAi : MonoBehaviour
{
    enum FlyStates
    {
        Move,
        attack
    }

    public Transform Player;

    private Rigidbody2D _rigidbody;
    public float speed = 3.0f;
    [SerializeField]
    Vector2 direction = new Vector2(1, 0.25f);

    [SerializeField] LayerMask Ground;
    public Transform UpPoint;
    public Transform LateralPoint;
    public Transform DownPoint;
    bool upHit, lateralHit, downHit;

    public Transform Jugador;

    float attackDistance = 10.0f;
    float timerToShoot;
    public float radiusDetectWalls = 0.25f;

    public Bullet bala;
    float _elapseTime = 0f;
    float _ratioShoot = 2.0f;


    FSM<FlyStates> brain;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _rigidbody = rb;
        InitFSM();
    }

    // Update is called once per frame
    void Update()
    {
        brain.Update();
    }
    public void InitFSM()
    {
        brain = new FSM<FlyStates>(FlyStates.Move);
        brain.SetOnEnter(FlyStates.Move, ()=> timerToShoot = 0f);

        brain.SetOnEnter(FlyStates.attack, ()=> {});
        brain.SetOnStay(FlyStates.Move, MoveUpdate);
        brain.SetOnStay(FlyStates.attack, AttackUpdate);
    }
    public void AttackUpdate()
    {
        Vector2 direction = Jugador.position - transform.position;
        direction.Normalize();
        _rigidbody.linearVelocity = direction * speed;
        _elapseTime += Time.deltaTime;

        if(_elapseTime > _ratioShoot)
        {
            _elapseTime = 0f;
            Bullet currentBullet = Instantiate(bala, transform.position, Quaternion.identity);
            currentBullet.dir = direction;
        }
        
    }

    public void MoveUpdate()
    {
        _rigidbody.linearVelocity = direction * speed;
        if (DetectCollison())
        {
            ChangeDirection();
            if (IsPlayerCloseByDistance())
            {
                brain.ChangeState(FlyStates.attack);
            }
        }
    }

    bool DetectCollison()
    {
        upHit = Physics2D.OverlapCircle(UpPoint.transform.position, radiusDetectWalls,Ground);
        downHit = Physics2D.OverlapCircle(DownPoint.transform.position, radiusDetectWalls,Ground);
        lateralHit = Physics2D.OverlapCircle(LateralPoint.transform.position, radiusDetectWalls,Ground);
        return upHit || downHit || lateralHit;
    }

    void ChangeDirection()
    {
            if (lateralHit)
        {
            transform.Rotate(0, 180, 0);
            direction.x = -direction.x;
        }

        if (upHit && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        if (downHit && direction.y < 0)
        {
            direction.y = -direction.y;
        }

    }

    bool IsPlayerCloseByDistance()
    {
        if(Vector2.Distance(transform.position, Jugador.position) <= attackDistance)
        {
            return true;
        }
            return false;
    }

}

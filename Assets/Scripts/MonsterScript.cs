using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MonsterScript : MonoBehaviour
{
    private Animator animator;
    private float MinimumHitPeriod = 1f;
    private float HitCounter = 0;
    public float CurrentHP = 100;
    private bool IsHit = false;

    public float MoveSpeed = 2.0f;
    public GameObject FollowTarget;
    private Rigidbody rigidBody;
    public CollisionListScript PlayerSensor;
    public CollisionListScript AttackSensor;
    [SerializeField] FloatingHealthBar healthBar;

    public void AttackPlayer()
    {
        if (AttackSensor.CollisionObjects.Count > 0)
        {
            AttackSensor.CollisionObjects[0].transform.GetChild(0).GetChild(0).SendMessage("Hit", 10);
        }
    }
    
    void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rigidBody = this.GetComponent<Rigidbody>();
        // healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdataHealthBar(CurrentHP);
    }
    void Update()
    {
        // Debug.Log("cnt is :" + PlayerSensor.CollisionObjects.Count);
        if (PlayerSensor.CollisionObjects.Count > 0)
        {
            FollowTarget = PlayerSensor.CollisionObjects[0].gameObject;
        }
        else
        {
            FollowTarget = null;
            animator.SetBool("Run", false);
        }
        if(IsHit)
        {
            FollowTarget = GameObject.FindGameObjectWithTag("Player");
        }
        // Debug.Log("the followtarget is :" + FollowTarget);

        if (CurrentHP > 0 && HitCounter > 0) // hit or just be hit, stopping running
        {
            HitCounter -= Time.deltaTime;
        }
        else
        {
            if (CurrentHP > 0)
            {
                if (FollowTarget != null)
                {
                    Vector3 lookAt = FollowTarget.gameObject.transform.position;
                    lookAt.y = this.gameObject.transform.position.y;
                    this.transform.LookAt(lookAt);
                    animator.SetBool("Run", true);


                    if (AttackSensor.CollisionObjects.Count > 0)
                    {
                        animator.SetBool("Attack", true);
                        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                    else
                    {
                        animator.SetBool("Attack", false);
                        rigidBody.velocity = this.transform.forward * MoveSpeed;
                    }
                }
            }
            else
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    public void Hit(float value)
    {
        if (HitCounter <= 0)
        {
            IsHit = true;
            // FollowTarget = GameObject.FindGameObjectWithTag("Player");
            HitCounter = MinimumHitPeriod;
            CurrentHP -= value;
            healthBar.UpdataHealthBar(CurrentHP);

            animator.SetFloat("HP", CurrentHP);
            animator.SetTrigger("Hit");

            if (CurrentHP <= 0) { BuryTheBody(); }
        }
    }
    void BuryTheBody()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Collider>().enabled = false;
        this.transform.DOMoveY(-0.8f, 1f).SetRelative(true).SetDelay(1).OnComplete(() =>
        {
            this.transform.DOMoveY(-0.8f, 1f).SetRelative(true).SetDelay(3).OnComplete(() =>
            {
                GameObject.Destroy(this.gameObject);
            });
        });
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private SpriteRenderer sprite;
    private Animator animator;

#pragma warning disable 0649
    [Header("Player Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float attackTime;
    [SerializeField] private float comboTime;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackMovement;
    [SerializeField] private float attackKnockback;
    [SerializeField] private float dodgeTime;
    [SerializeField] private float dodgeIFrameTime;
    //[SerializeField] private float dodgeDistance;
    [SerializeField] private float dodgeSpeed;
    [SerializeField] private AnimationCurve dodgeCurve;
    [SerializeField] private float dodgeCooldown;

    [Header("Player References")]
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private GameObject sword;
    [SerializeField] private Collider2D hurtbox;
    [SerializeField] private PlayerData playerData;
#pragma warning restore 0649

    private Coroutine routine;
    private bool noAction = false;
    private int attackState = 0;
    private Coroutine attackTimer;

    private Vector2 lastDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerData.position = transform.position;
        playerData.layer = gameObject.layer;
    }

    public void Move(float horizontal_axis, float vertical_axis)
    {
        if (routine == null)
        {
            rb.velocity = new Vector2(horizontal_axis * moveSpeed, vertical_axis * moveSpeed);
            if (horizontal_axis != 0) sprite.flipX = horizontal_axis > 0;
            if (horizontal_axis != 0 || vertical_axis != 0)
            {
                lastDirection = new Vector2(horizontal_axis, vertical_axis);
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }

    public void Attack(Vector2 direction)
    {
        if(routine == null)
        {
            noAction = true;
            routine = StartCoroutine(Strike(direction));
        }
    }

    private IEnumerator Strike(Vector2 direction)
    {
        if (direction == Vector2.zero) direction = lastDirection;
        rb.velocity = direction * attackMovement;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Hitbox temp = Instantiate(hitbox, transform.position, Quaternion.Euler(0, 0, angle), transform);
        temp.transform.localPosition = direction.normalized * 1;
        temp.gameObject.layer = gameObject.layer;

        GameObject o = Instantiate(sword, transform.position, Quaternion.Euler(0, 0, angle), transform);
        o.transform.localPosition = direction.normalized * 1;

        LayerMask mask = LayerMask.GetMask("EnemyA", "WorldA");
        if (gameObject.layer == 14) mask = LayerMask.GetMask("EnemyB", "WorldB");
        temp.Initialize(damage, attackKnockback, mask);

        yield return new WaitForSeconds(attackTime);
        Destroy(temp.gameObject);
        Destroy(o);

        routine = null;


        yield return new WaitForSeconds(attackCooldown);
        attackState++;
        if (attackState > 3)
        {
            attackState = 0;
        }

        noAction = false;

        attackTimer = StartCoroutine(ComboTimer());
    }

    private IEnumerator ComboTimer()
    {
        yield return new WaitForSeconds(comboTime);
        attackState = 0;
    }

    public void DodgeRoll(Vector2 direction)
    {
        if(routine == null && !noAction)
        {
            noAction = true;
            routine = StartCoroutine(dodge(direction));
        }
    }

    private IEnumerator dodge(Vector2 direction)
    {
        yield return new WaitForFixedUpdate();
        hurtbox.enabled = false;
        GameManager.instance.ToggleWorlds();
        ToggleLayer();
        rb.velocity = Vector2.zero;
        if (direction.x > 0) animator.SetTrigger("dodgeR");
        else animator.SetTrigger("dodge");

        if (direction == Vector2.zero) direction = lastDirection;

        //Vector2 target = (Vector2)transform.position + direction.normalized * dodgeDistance;
        //Vector2 origPos = transform.position;
        float timer = 0;

        while(timer < dodgeTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;

            rb.velocity = direction.normalized * dodgeSpeed * dodgeCurve.Evaluate(timer / dodgeTime);
            //transform.position = Vector2.Lerp(origPos, target, dodgeCurve.Evaluate(timer / dodgeTime));
            if (timer > dodgeIFrameTime) hurtbox.enabled = true;
        }

        rb.velocity = Vector2.zero;

        routine = null;

        yield return new WaitForSeconds(dodgeCooldown);
        noAction = false;
    }

    private void ToggleLayer()
    {
        if (gameObject.layer == 9)
        {
            gameObject.layer = 10;
            hurtbox.gameObject.layer = 10;
        }
        else
        {
            gameObject.layer = 9;
            hurtbox.gameObject.layer = 9;
        }
    }

    private IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.white;
    }

    public override void OnDeath()
    {
        LoadManager.instance.LoadScene("GameOver");
    }

    public override void OnHit(int amount)
    {
        StartCoroutine(FlashRed());
    }

    public override void OnHeal(int amount)
    {
        throw new System.NotImplementedException();
    }

    public override void OnKnockback(float amount, Vector2 direction)
    {
        rb.velocity = direction.normalized * amount;
    }
}

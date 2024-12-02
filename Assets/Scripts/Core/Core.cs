using System.Collections;
using UnityEngine;

public enum StatType
{
    damage,
    critChance,
    critPower,
    health,
    armor

}

public class Core : MonoBehaviour
{
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public CoreFX fx { get; private set; }

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    [Header("Attack info")]
    public Transform attackCheck;
    public float attackCheckRadius;


    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;


    [Header("Stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;
    public Stat maxHealth;
    public Stat armor;


    public int currentHeatlh;
    public System.Action onHealthChanged;
    public bool isDead;
    public bool isInvincible { get; private set; }

    [Header("Time slow on hit")]
    public float slowDownDuration = 0.5f;
    public float slowDownScale = 0.5f;
    private bool isSlowedDown = false;
    private float slowDownTimer;


    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    public System.Action onFlipped;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        fx = GetComponent<CoreFX>();

        animator = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody2D>();

        critPower.SetDefaultValue(150);
        currentHeatlh = GetMaxHealthValue();
        fx = GetComponent<CoreFX>();

    }

    protected virtual void Update()
    {
        if (isSlowedDown)
        {
            slowDownTimer -= Time.unscaledDeltaTime;

            if (slowDownTimer <= 0)
            {
                Time.timeScale = 1;
                isSlowedDown = false;
            }
        }

    }


    public virtual void DoDamage(Core _targetStats)
    {
        int totalDamage = damage.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);
    }



    private int CheckTargetArmor(Core _targetStats, int totalDamage)
    {
        totalDamage -= _targetStats.armor.GetValue();

        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool CanCrit()
    {
        int totalCritChance = critChance.GetValue();

        int randomValue = Random.Range(0, 100);

        if (randomValue <= totalCritChance)
            return true;

        return false;

    }

    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = critPower.GetValue() * .01f;
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible)
            return;

        DecreaseHealthBy(_damage);

        GetComponent<Core>().DamageEffect();
        fx.StartCoroutine("FlashFX");

        if (!isSlowedDown)
        {
            Time.timeScale = slowDownScale;
            slowDownTimer = slowDownDuration;
            isSlowedDown = true;
        }

        if (currentHeatlh <= 0 && !isDead)
            Die();
    }

    public virtual void DecreaseHealthBy(int _damage)
    {
        currentHeatlh -= _damage;

        if (onHealthChanged != null)
            onHealthChanged();
    }

    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHeatlh += _amount;

        if (currentHeatlh > GetMaxHealthValue())
            currentHeatlh = GetMaxHealthValue();

        if (onHealthChanged != null)
            onHealthChanged();
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }

    public virtual void DamageEffect()
    {
        StartCoroutine(HitKnockback(new Vector2(0, 0), 0));
    }

    public virtual IEnumerator HitKnockback(Vector2 direction, float duration)
    {
        isKnocked = true;

        rb.velocity = new Vector2(direction.x * -facingDir, direction.y);

        yield return new WaitForSeconds(duration);

        isKnocked = false;
    }

    public void SetZeroVelocity()
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(0, 0);
    }

    public void SetVelocity(float _xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, yVelocity);
        FlipController(_xVelocity);
    }

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    public void KillCharacter()
    {
        if (!isDead)
            Die();
    }

    public Stat GetStat(StatType buffType)
    {
        if (buffType == StatType.damage)
            return damage;
        else if (buffType == StatType.critChance)
            return critChance;
        else if (buffType == StatType.critPower)
            return critPower;
        else if (buffType == StatType.health)
            return maxHealth;
        else if (buffType == StatType.armor)
            return armor;

        return null;
    }

    public void MakeInvincible(bool _invincible)
    {
        isInvincible = _invincible;
    }

}

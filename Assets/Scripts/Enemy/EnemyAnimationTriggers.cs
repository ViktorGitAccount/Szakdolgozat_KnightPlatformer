using UnityEngine;

public class EnemyAnimationTriggers : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {

                Player target = hit.GetComponent<Player>();
                enemy.DoDamage(target);
            }
        }
    }

    private void RangeAttackTrigger()
    {
        enemy.AnimationRangedAttackTrigger();
    }

    private void OpenCounterWindow() => enemy.OpencounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}

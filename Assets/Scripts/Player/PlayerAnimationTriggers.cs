using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                Enemy target = hit.GetComponent<Enemy>();
                player.DoDamage(target);

                

                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

            }

            if (hit.GetComponent<HiddenWall>() != null)
            {
                HiddenWall wall = hit.GetComponent<HiddenWall>();
                wall.DestroyWall();
            }
        }
    }
}

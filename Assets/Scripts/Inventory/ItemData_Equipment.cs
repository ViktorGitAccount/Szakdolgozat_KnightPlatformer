using UnityEngine;

public enum EquipmentType
{
    Weapon,
    ChestPlate,
    Helmet,
    Boots,
    Gloves
}


[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Defensive stats")]
    public int maxHealth;
    public int armor;



    private int descriprtionLength;


    public void AddModifiers()
    {
        Player playerStats = PlayerManager.instance.player.GetComponent<Player>();

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.maxHealth.AddModifier(maxHealth);
        playerStats.armor.AddModifier(armor);


    }

    public void RemoveModifiers()
    {
        Player playerStats = PlayerManager.instance.player.GetComponent<Player>();

        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);

        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.armor.RemoveModifier(armor);

    }

    public override string GetDescription()
    {
        sb.Length = 0;
        descriprtionLength = 0;

        AddItemDescription(damage, "Damage");
        AddItemDescription(critChance, "Crit Chance");
        AddItemDescription(critPower, "Crit Power");

        AddItemDescription(maxHealth, "Health");
        AddItemDescription(armor, "Armor");



        if (descriprtionLength < 5)
        {
            for (int i = 0; i < 5 - descriprtionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }



        return sb.ToString();
    }

    private void AddItemDescription(int _value, string _name)
    {
        if (_value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (_value > 0)
                sb.Append("+ " + _value + " " + _name);

            descriprtionLength++;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockButton;
    public bool parryUnlocked { get; private set; }

    [Header("Parry restore health")]
    [SerializeField] private UI_SkillTreeSlot parryRestoreUnlockedButton;
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthAmount;
    public bool parryRestoreUnlocked { get; private set; }

    [Header("Parry Arrow")]
    [SerializeField] private UI_SkillTreeSlot parryArrowUnlockButton;
    public bool parryArrowUnlocked { get; private set; }

    public override void UseSkill()
    {
        base.UseSkill();

        if (parryRestoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.GetMaxHealthValue() * restoreHealthAmount);
            player.IncreaseHealthBy(restoreAmount);
        }
    }

    protected override void Start()
    {
        base.Start();

        parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        parryRestoreUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryArrowUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryArrow);
    }

    protected override void CheckUnlock()
    {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryArrow();
    }

    private void UnlockParry()
    {
        if (parryUnlockButton.unlocked)
            parryUnlocked = true;
    }

    private void UnlockParryRestore()
    {
        if (parryRestoreUnlockedButton.unlocked)
            parryRestoreUnlocked = true;
    }

    private void UnlockParryArrow()
    {
        if (parryArrowUnlockButton.unlocked)
            parryArrowUnlocked = true;
    }
}

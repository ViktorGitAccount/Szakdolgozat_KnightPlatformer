using System;
using UnityEngine;
using UnityEngine.UI;

public class Heal_Skill : Skill
{
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthAmount;

    [Header("Healing I")]
    [SerializeField] private UI_SkillTreeSlot basicHealingButton;
    public bool basicHealingUnlocked { get; private set; }

    [Header("Healing II")]
    [SerializeField] private UI_SkillTreeSlot advancedHealingButton;
    public bool advancedHealingUnlocked { get; private set; }

    [Header("Healing III")]
    [SerializeField] private UI_SkillTreeSlot professionalHealingButton;
    public bool professionalHealingUnlocked { get; private set; }

    public override void UseSkill()
    {

        base.UseSkill();

        if (basicHealingUnlocked|| advancedHealingUnlocked|| professionalHealingUnlocked)
        {
            float restoreFactor = restoreHealthAmount;

            if (advancedHealingUnlocked) restoreFactor += 0.1f;
            if (professionalHealingUnlocked) restoreFactor += 0.2f;

            int restoreAmount = Mathf.RoundToInt(player.GetMaxHealthValue() * restoreFactor);
            player.IncreaseHealthBy(restoreAmount);
        }
    }

    protected override void CheckUnlock()
    {
        base.CheckUnlock();

        UnlockBasicHealing();
        UnlockAdvancedHealing();
        UnlockProfessionalHealing();
    }

    protected override void Start()
    {
        base.Start();

        basicHealingButton.GetComponent<Button>().onClick.AddListener(UnlockBasicHealing);
        advancedHealingButton.GetComponent<Button>().onClick.AddListener(UnlockAdvancedHealing);
        professionalHealingButton.GetComponent<Button>().onClick.AddListener(UnlockProfessionalHealing);
    }

    private void UnlockBasicHealing()
    {
        if (basicHealingButton.unlocked)
            basicHealingUnlocked = true;
    }
    private void UnlockAdvancedHealing()
    {
        if (advancedHealingButton.unlocked)
            advancedHealingUnlocked = true;
    }

    private void UnlockProfessionalHealing()
    {
        if (professionalHealingButton.unlocked)
            professionalHealingUnlocked = true;
    }
}

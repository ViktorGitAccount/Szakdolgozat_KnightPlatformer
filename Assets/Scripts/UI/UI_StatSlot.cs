using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UI ui;

    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;

    [TextArea]
    [SerializeField] private string statDescription;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;

        if(statNameText != null)
            statNameText.text = statName;
    }

    private void Start()
    {
        UpdateStatValueUI();

        ui = GetComponentInParent<UI>();
    }

    public void UpdateStatValueUI()
    {
        Player playerStats = PlayerManager.instance.player.GetComponent<Player>();

        if (playerStats != null)
        {
            statValueText.text = playerStats.GetStat(statType).GetValue().ToString(); 

            if(statType == StatType.health)
                statValueText.text = playerStats.GetMaxHealthValue().ToString();

            if (statType == StatType.damage)
                statValueText.text = playerStats.damage.GetValue().ToString();

            if(statType == StatType.critPower)
                statValueText.text = playerStats.critPower.GetValue().ToString();

            if(statType == StatType.critChance)
                statValueText.text = playerStats.critChance.GetValue().ToString();  



        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statToolTip.ShowStatToolTip(statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statToolTip.HideStatToolTip();
    }
}

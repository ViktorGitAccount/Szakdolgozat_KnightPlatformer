using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ingame : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Player player;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image healImage;

    private SkillManager skills;


    [Header("Souls info")]
    [SerializeField] private TextMeshProUGUI currency;
    [SerializeField] private float soulsAmount;
    [SerializeField] private float increaseRate = 100;


    private void Start()
    {
        if (player != null)
            player.onHealthChanged += UpdateHealthUI;

        skills = SkillManager.instance;
    }

    private void Update()
    {
        UpdateCurrencyUI();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            SetCooldownOf(dashImage);

        if (Input.GetKeyDown(KeyCode.F))
            SetCooldownOf(healImage);


        CheckCooldownOf(dashImage, skills.dash.cooldown);
        CheckCooldownOf(healImage, skills.heal.cooldown);


    }

    private void UpdateCurrencyUI()
    {
        if (soulsAmount < PlayerManager.instance.CurrentCurrencyAmount())
            soulsAmount += Time.deltaTime * increaseRate;
        else
            soulsAmount = PlayerManager.instance.CurrentCurrencyAmount();

        currency.text = ((int)soulsAmount).ToString();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = player.GetMaxHealthValue();
        slider.value = player.currentHeatlh;
    }

    public void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }

    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }
}

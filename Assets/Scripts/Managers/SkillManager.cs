using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Dash_Skill dash { get; private set; }
    public Parry_Skill parry { get; private set; }
    public Heal_Skill heal { get; private set; }


    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        dash = GetComponent<Dash_Skill>();
        parry = GetComponent<Parry_Skill>();
        heal = GetComponent<Heal_Skill>();

    }
}
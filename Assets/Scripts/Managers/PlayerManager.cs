using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int currency;


    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public bool HaveEnoughMoney(int price)
    {
        if (price > currency)
            return false;

        currency -= price;
        return true;
    }

    public int CurrentCurrencyAmount()
    {
        return currency;
    }

    public void LoadData(GameData _data)
    {
        this.currency = _data.currency;

    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = this.currency;


    }

}

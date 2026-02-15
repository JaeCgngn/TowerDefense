using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int startMoney = 400;
    public int Money { get; private set; } // Changed to a property with a private setter

    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Money = startMoney;
        OnMoneyChanged?.Invoke(Money);
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        OnMoneyChanged?.Invoke(Money);
    }

    public bool SpendMoney(int amount)
    {
        if (Money < amount)
            return false;

        Money -= amount;
        OnMoneyChanged?.Invoke(Money);
        return true;
    }
}

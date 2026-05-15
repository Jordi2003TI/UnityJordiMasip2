using UnityEngine;
using System;

[DefaultExecutionOrder(-100)]
public class CoinManager : MonoBehaviour
{
    private static CoinManager _instance;

    [SerializeField] private float _amount;

    public float Amount { get { return _instance._amount; } }
    public static CoinManager Instance { get { return _instance; } }
    public static Action OnAddPoints;

    public static Action OnBonusLife;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddAmount(float amount)
    {
        if (_instance != null)
        {
            Instance.AddAmountInternal(amount);
        }
    }

    private void AddAmountInternal(float amount)
    {
        float before = _amount;
        _amount += amount;
        OnAddPoints?.Invoke();

         if (Mathf.FloorToInt(_amount / 10) > Mathf.FloorToInt(before / 10))
        {
            Debug.Log("¡Bonus life disparado! Monedas: " + _amount); // ← temporal
            OnBonusLife?.Invoke();
        }
    }
}
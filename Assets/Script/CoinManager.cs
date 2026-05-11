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
        _amount += amount;
        OnAddPoints?.Invoke(); // ← AQUÍ es donde debe estar
    }
}
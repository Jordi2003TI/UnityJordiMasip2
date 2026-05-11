using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]

public class CoinText : MonoBehaviour
{
    TextMeshProUGUI _text;

    private void Start(){
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable(){
        CoinManager.OnAddPoints += ChangeText;
    }

    private void OnDisable(){
        CoinManager.OnAddPoints -= ChangeText;
    }

    private void ChangeText(){
        _text.text="Coins: " +  CoinManager.Instance.Amount;
    }
}

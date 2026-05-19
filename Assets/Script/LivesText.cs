using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LivesText : MonoBehaviour
{
    TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>(); // Awake siempre corre antes que Start
    }

    private void OnEnable()
    {
        PlayerHealth.OnLivesChanged += ChangeText;
    }

    private void OnDisable()
    {
        PlayerHealth.OnLivesChanged -= ChangeText;
    }

    private void ChangeText()
    {
        if (PlayerHealth.Instance != null)
            _text.text = "Lives: " + PlayerHealth.Instance.GetCurrentLives();
    }
}
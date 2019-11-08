using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float updateSpeed;
    Quaternion rotation;
    
    public void Awake()
    {
        GetComponentInParent<CharacterStats>().onHealthPercentChange += handleHealthChanged;
    }

    void Start()
    {
        rotation = transform.rotation;
    }

    private void handleHealthChanged(float newPercent)
    {
        // healthBar.fillAmount = newPercent;
        StartCoroutine(changeHealthBar(newPercent));
    }

    private IEnumerator changeHealthBar(float newPercent)
    {
        float preChangePercent = healthBar.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeed) {
            elapsed += Time.deltaTime;
            healthBar.fillAmount = Mathf.Lerp(preChangePercent, newPercent, elapsed / updateSpeed);
            yield return null;
        }

        healthBar.fillAmount = newPercent;
    }

    void LateUpdate()
    {
        transform.rotation = rotation;
    }
}

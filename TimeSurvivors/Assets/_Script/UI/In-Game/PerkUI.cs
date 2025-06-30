using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkUI : MonoBehaviour
{
    public GameObject panel;
    public Button[] perkButtons;
    public TextMeshProUGUI[] perkTexts;

    string[] perkTypes = new string[] { "MoveSpeed", "FireRate", "Damage", "CritChance", "Heal", "MaxHealth" };

    public void ShowPerkChoices()
    {
        panel.SetActive(true);

        List<string> availablePerks = new List<string>(perkTypes);
        for (int i = 0; i < perkButtons.Length; i++)
        {
            int randomIndex = Random.Range(0, availablePerks.Count);
            string perk = availablePerks[randomIndex];
            availablePerks.RemoveAt(randomIndex);

            float value = GetRandomValue(perk);
            perkTexts[i].text = $"{perk} +{value}";

            int capturedIndex = i; 
            perkButtons[i].onClick.RemoveAllListeners();
            perkButtons[i].onClick.AddListener(() =>
            {
                ApplyPerk(perk, value);
                panel.SetActive(false);
            });
        }
    }

    float GetRandomValue(string perk)
    {
        float value = 1f;

        switch (perk)
        {
            case "MoveSpeed":
                value = Random.Range(0.25f, 1.5f);
                break;
            case "FireRate":
                value = Random.Range(0.15f, 0.4f);
                break;
            case "Damage":
                value = Random.Range(1f, 20f);
                break;
            case "CritChance":
                value = Random.Range(5f, 10f);
                break;
            case "Heal":
                value = Random.Range(10f, 50f);
                break;
            case "MaxHealth":
                value = Random.Range(10f, 50f);
                break;
        }

        return RoundToQuarter(value);
    }


    void ApplyPerk(string perk, float value)
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();

        switch (perk)
        {
            case "MoveSpeed": stats.moveSpeed += value; break;
            case "FireRate": stats.fireRate -= value; break;
            case "Damage": stats.damage += value; break;
            case "CritChance": stats.critChance += value; break;
            case "Regen": stats.health += value; break;
            case "MaxHealth": stats.maxHealth += value; break;
        }
        Time.timeScale = 1f;
    }

    float RoundToQuarter(float value)
    {
        return Mathf.Round(value * 4f) / 4f;
    }

}

/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkUI : MonoBehaviour
{
    public GameObject panel;
    public Button[] perkButtons;
    public Text[] perkTexts;

    string[] perkTypes = new string[] { "MoveSpeed", "FireRate", "Damage", "CritChance", "Regen", "MaxHealth" };

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

            int capturedIndex = i; // for lambda capture
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
        switch (perk)
        {
            case "MoveSpeed": return Random.Range(0.5f, 1.5f);
            case "FireRate": return Random.Range(5f, 20f); // % faster
            case "Damage": return Random.Range(1f, 3f);
            case "CritChance": return Random.Range(2f, 5f); // %
            case "Regen": return Random.Range(0.1f, 0.5f);
            case "MaxHealth": return Random.Range(5f, 15f);
        }
        return 1f;
    }

    void ApplyPerk(string perk, float value)
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();

        switch (perk)
        {
            case "MoveSpeed": stats.moveSpeed += value; break;
            case "FireRate": stats.fireRateMultiplier *= 1f + (value / 100f); break;
            case "Damage": stats.damage += value; break;
            case "CritChance": stats.critChance += value; break;
            case "Regen": stats.regen += value; break;
            case "MaxHealth": stats.maxHealth += value; stats.currentHealth += value; break;
        }
    }
}
*/
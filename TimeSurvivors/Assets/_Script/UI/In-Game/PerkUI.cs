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
        switch (perk)
        {
            case "MoveSpeed":
                return RoundToQuarter(Random.Range(0.25f, 1.5f));

            case "FireRate":
                return SnapToSet(Random.Range(0.15f, 0.45f), new float[] { 0.15f, 0.30f, 0.45f });

            case "Damage":
                return Mathf.Round(Random.Range(1f, 20f));

            case "CritChance":
                return Mathf.Round(Random.Range(5f, 10f));

            case "Heal":
            case "MaxHealth":
                return SnapToSet(Random.Range(10f, 50f), new float[] { 5f, 10f, 20f, 30f, 40f, 50f });

            default:
                return 1f;
        }
    }



    void ApplyPerk(string perk, float value)
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();

        switch (perk)
        {
            case "MoveSpeed": stats.moveSpeed += value; break;
            case "FireRate": stats.fireRate -= value; break;
            case "Damage": stats.damage += (int)value; break;
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

    float SnapToSet(float value, float[] allowed)
    {
        float closest = allowed[0];
        float smallestDiff = Mathf.Abs(value - closest);

        foreach (float option in allowed)
        {
            float diff = Mathf.Abs(value - option);
            if (diff < smallestDiff)
            {
                smallestDiff = diff;
                closest = option;
            }
        }

        return closest;
    }


}

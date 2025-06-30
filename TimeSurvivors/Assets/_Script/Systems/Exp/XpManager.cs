using UnityEngine;

public class XpManager : MonoBehaviour
{
    public static XpManager Instance;

    public int currentXP = 0;
    public int level = 1;
    public int xpToNextLevel = 5;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        xpToNextLevel += Mathf.RoundToInt(xpToNextLevel * 0.25f);
        Time.timeScale = 0f;
        FindObjectOfType<PerkUI>().ShowPerkChoices();
    }
}

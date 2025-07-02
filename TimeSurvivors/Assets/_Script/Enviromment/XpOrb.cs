using UnityEngine;

public class XpOrb : MonoBehaviour
{
    public int xpAmount = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            XpManager.Instance.AddXP(xpAmount);
            Destroy(gameObject);
        }
    }
}

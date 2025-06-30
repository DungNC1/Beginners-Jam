using UnityEngine;

public class XpOrb : MonoBehaviour
{
    public int xpAmount = 1;
    private float lifeTime = 10f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            XpManager.Instance.AddXP(xpAmount);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class BricksManager : MonoBehaviour
{
    public static BricksManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Sprite[] Sprite;
}

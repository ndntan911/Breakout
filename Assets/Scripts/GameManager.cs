using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance { get; private set; }

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

    #endregion

    public bool IsGameStarted { get; set; }

    private void Start()
    {
        Screen.SetResolution(540, 960, false);
    }
}

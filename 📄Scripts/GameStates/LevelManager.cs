// LevelManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int CurrentLevelNumber { get; private set; } = 1; // one-based

    // event for level cleared
    public delegate void LevelClearedHandler();
    public event LevelClearedHandler OnLevelCleared;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        ParseCurrentSceneIndex();
        
    }

    private void Start()
    {
        // set UI level text if UIManager present
        GameManager.Instance.uiManager?.SetLevelText(CurrentLevelNumber);
    }

    public void ParseCurrentSceneIndex()
    {
        // If the active scene name matches LevelN pattern (Level1, Level2)
        string name = SceneManager.GetActiveScene().name;
        
        if (name.StartsWith("Level"))
        {
            string tail = name.Substring("Level".Length);
            
            if (int.TryParse(tail, out int n)) {
                CurrentLevelNumber = n;
                
            }
        }
    }

    private void Update()
    {
        // check enemy count only while in gameplay
        if (GameManager.Instance.StateMachine.Current is GameplayState)
        {
           
            // simple method: find any object tagged "Enemy"
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log("Enemy count === "+ enemies.Length);
            if (enemies.Length == 0)
            {
                // dispatch once
                OnLevelCleared?.Invoke();
                // Prevent repeated invocation; move to upgrade state via GameManager
                //GameManager.Instance.StateMachine.ChangeState(new UpgradeState(GameManager.Instance.StateMachine));
                GameManager.Instance.StateMachine.ChangeState(
                    new UpgradeState(GameManager.Instance.StateMachine)
                );
            }
        }
    }

    /* public void LoadNextLevel()
    {
        int next = CurrentLevelNumber + 1;
        string nextName = $"Level{next}";
        // simple check: if scene exists in build settings you can load it
        SceneManager.LoadScene(nextName);
        CurrentLevelNumber = next;
        GameManager.Instance.uiManager?.SetLevelText(CurrentLevelNumber);
    } */
    public void LoadNextLevel()
    {
        Debug.Log("Current level is ==="+ CurrentLevelNumber.ToString());
        int next = CurrentLevelNumber + 1;
        string nextName = "Level" + next;
        Debug.Log("Next level name === "+ nextName);

        if (Application.CanStreamedLevelBeLoaded(nextName))
        {
            Debug.Log("CanStreamedLevelBeLoaded");
            SceneManager.LoadScene(nextName);
            CurrentLevelNumber = next;
        }
        else
        {
            Debug.Log("No more levels! Returning to Main Menu.");
            SceneManager.LoadScene("Level0");
        }
    }


    public void ReloadCurrentLevel()
    {
        string name = $"Level{CurrentLevelNumber}";
        SceneManager.LoadScene(name);
    }

    public void LoadLevelDirect(int levelNumber)
    {
        string name = $"Level{levelNumber}";
        SceneManager.LoadScene(name);
        CurrentLevelNumber = levelNumber;
    }

    public bool IsGameplayScene()
    {
        string s = SceneManager.GetActiveScene().name;
        return s.StartsWith("Level") && s != "Level0";
    }

}

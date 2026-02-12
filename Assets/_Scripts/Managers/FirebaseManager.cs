using UnityEngine;
using Firebase; 

public class FirebaseManager : MonoBehaviour
{
  
    public static FirebaseManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);


            InitializeFirebase();
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void InitializeFirebase()
    {
        Debug.Log("Ініціалізація Firebase...");
        
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            
            var dependencyStatus = task.Result;
            
            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                Debug.Log("<color=green>Firebase успішно ініціалізовано!</color>");
                
            }
            else
            {
                Debug.LogError($"<color=red>Не вдалося ініціалізувати Firebase: {dependencyStatus}</color>");
            }
        });
    }
}
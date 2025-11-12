using UnityEngine;

public class InputManager : MonoBehaviour
{
    // --- Singleton ---
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        
    }
    // --- Кінець Singleton ---

    [Header("Керування")]
    [Tooltip("Ігнорувати ввід, менший за це значення")]
    public float inputDeadzone = 0.05f; // "Мертва зона" для ігнорування дрейфу

    // Приватні змінні, що зберігають стан
    private float motorInput;
    private float turnInput;
    
    // Головна логіка (слухає клавіатуру)
    void Update()
    {
        // 1. Ввід повороту (A/D) з "мертвою зоною"
        float rawTurnInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(rawTurnInput) < inputDeadzone)
        {
            turnInput = 0f;
        }
        else
        {
            turnInput = rawTurnInput;
        }

        // 2. Ввід мотора (W/S) з "мертвою зоною"
        float rawMotorInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(rawMotorInput) < inputDeadzone)
        {
            motorInput = 0f;
        }
        else
        {
            motorInput = rawMotorInput;
        }
    }

    // --- Публічні "Get" методи (для машини) ---
    public float GetMotorInput()
    {
        return motorInput;
    }

    public float GetTurnInput()
    {
        return turnInput;
    }

    // --- Публічні "Set" методи (для майбутнього UI-ричажка) ---
    public void SetMotorInput(float value)
    {
        motorInput = value;
    }

    public void SetTurnInput(float value)
    {
        turnInput = value;
    }
}
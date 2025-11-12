using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using Firebase;
using Firebase.Auth; 
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    [Header("Firebase")]
    private FirebaseAuth auth;

    [Header("Панелі")]
    public GameObject initialPanel;
    public GameObject loginPanel;
    public GameObject registerPanel;

    [Header("Поля вводу (Спільні)")]
    // Ми можемо "ділити" одні й ті самі поля між панелями, 
    // якщо вони однакові, або підключити різні. 
    // Для простоти, зробимо їх окремими.
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;
    public TextMeshProUGUI loginStatus;

    public TMP_InputField registerEmail;
    public TMP_InputField registerPassword;
    public TextMeshProUGUI registerStatus;

    [Header("Кнопки")]
    // Кнопки на InitialPanel
    public Button showLoginButton;
    public Button showRegisterButton;
    // public Button googleLoginButton; // (Якщо додали)

    // Кнопки на LoginPanel
    public Button loginSubmitButton;
    public Button loginBackButton;

    // Кнопки на RegisterPanel
    public Button registerSubmitButton;
    public Button registerBackButton;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        // --- Прив'язка кнопок до функцій ---
        
        // Головні кнопки
        showLoginButton.onClick.AddListener(ShowLoginPanel);
        showRegisterButton.onClick.AddListener(ShowRegisterPanel);
        // googleLoginButton.onClick.AddListener(OnGoogleLogin);

        // Кнопки панелі Входу
        loginSubmitButton.onClick.AddListener(OnLoginSubmit);
        loginBackButton.onClick.AddListener(ShowInitialPanel);

        // Кнопки панелі Реєстрації
        registerSubmitButton.onClick.AddListener(OnRegisterSubmit);
        registerBackButton.onClick.AddListener(ShowInitialPanel);

        // Починаємо з початкової панелі
        ShowInitialPanel();
    }

    // --- Керування Панелями ---
    private void ShowInitialPanel()
    {
        initialPanel.SetActive(true);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
    }

    private void ShowLoginPanel()
    {
        initialPanel.SetActive(false);
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    private void ShowRegisterPanel()
    {
        initialPanel.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    // --- Логіка Firebase ---

    private async void OnRegisterSubmit()
    {
        string email = registerEmail.text;
        string password = registerPassword.text;
        registerStatus.text = "Реєстрація...";

        try
        {
            await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            registerStatus.text = "Успішна реєстрація: " + auth.CurrentUser.Email;
            
            // TODO: Зберегти початкові дані юзера в Firestore
            
            SceneManager.LoadScene("MainMenuScene"); 
        }
        catch (FirebaseException ex)
        {
            registerStatus.text = "Помилка: " + ex.Message;
        }
    }

    private async void OnLoginSubmit()
    {
        string email = loginEmail.text;
        string password = loginPassword.text;
        loginStatus.text = "Вхід...";

        try
        {
            await auth.SignInWithEmailAndPasswordAsync(email, password);
            loginStatus.text = "Успішний вхід: " + auth.CurrentUser.Email;
            
            SceneManager.LoadScene("MainMenuScene"); 
        }
        catch (FirebaseException ex)
        {
            loginStatus.text = "Помилка: " + ex.Message;
        }
    }
    
    // (Тут буде логіка OnGoogleLogin, якщо потрібна)
}
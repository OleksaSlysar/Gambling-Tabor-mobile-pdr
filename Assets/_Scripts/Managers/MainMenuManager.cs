using UnityEngine;
using UnityEngine.SceneManagement; // Потрібно для переходу між сценами
using Firebase.Auth; // Потрібно для виходу з акаунта (опціонально)

public class MainMenuManager : MonoBehaviour
{
    [Header("Налаштування Сцен")]
    public string gameSceneName = "SceneRakovo"; // Назва вашої ігрової сцени
    public string authSceneName = "AuthScene"; // Назва сцени входу

    // --- Методи для Кнопок ---

    /**
     * Викликається кнопкою "Грати в Практику".
     * Завантажує головну ігрову сцену.
     */
    public void OnPlayPracticeClicked()
    {
        Debug.Log("Завантаження ігрової сцени: " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
    }

    /**
     * Викликається кнопкою "Акаунт".
     * (Зараз це заглушка, пізніше тут можна відкрити панель)
     */
    public void OnAccountClicked()
    {
        Debug.Log("Кнопка 'Акаунт' натиснута. (Поки що нічого не робить)");
        // TODO: (наприклад, accountPanel.SetActive(true);)
    }

    /**
     * Викликається кнопкою "Вихід".
     * Виходить з гри ТА виходить з акаунта Firebase.
     */
    public void OnExitClicked()
    {
        Debug.Log("Вихід з гри...");

        // 1. (Опціонально) Вийти з Firebase, щоб при наступному запуску
        // гравець знову побачив екран входу.
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        if (auth != null && auth.CurrentUser != null)
        {
            auth.SignOut();
            Debug.Log("Вихід з акаунта Firebase.");
            // Після виходу, перекидаємо на сцену входу
            SceneManager.LoadScene(authSceneName); 
        }
        else
        {
            // Якщо Firebase не налаштовано або юзер вже вийшов
            Application.Quit(); // Закриває додаток (не працює в редакторі)
        }
    }
}
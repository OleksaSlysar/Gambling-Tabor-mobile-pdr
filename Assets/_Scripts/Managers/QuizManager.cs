using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
using Firebase.Firestore;
using Firebase.Storage;
using System.Collections.Generic;
using System.Linq; 

[System.Serializable]
public class PDRQuestion
{
    public string DocumentID; 
    public string questionText;
    public string imageRef; 
    public List<string> answers;
    public int correctAnswerIndex;
    public string videoRef_onSuccess; 
    public string nextQuestionID; 
}

public class QuizManager : MonoBehaviour
{
    [Header("UI Елементи")]
    public Image questionImage;
    public TextMeshProUGUI questionText;
    public List<Button> answerButtons;
    public TextMeshProUGUI statusText;

    private FirebaseFirestore db;
    private FirebaseStorage storage;

    private Dictionary<string, PDRQuestion> questionBank = new Dictionary<string, PDRQuestion>();
    private PDRQuestion currentQuestion; 

    async void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        storage = FirebaseStorage.DefaultInstance;

        answerButtons[0].onClick.AddListener(() => OnAnswerClicked(0));
        answerButtons[1].onClick.AddListener(() => OnAnswerClicked(1));
        answerButtons[2].onClick.AddListener(() => OnAnswerClicked(2));
        answerButtons[3].onClick.AddListener(() => OnAnswerClicked(3));

        await LoadAllQuestions();
        if (questionBank.Count > 0)
        {
            ShowQuestion("q_001"); 
        }
    }

    public async Task LoadAllQuestions()
    {
        statusText.text = "Завантаження питань...";
        
        QuerySnapshot snapshot = await db.Collection("pdr_questions").GetSnapshotAsync();
        
        questionBank.Clear(); 

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            Dictionary<string, object> data = document.ToDictionary();
            PDRQuestion q = new PDRQuestion
            {
                DocumentID = document.Id,
                questionText = data["questionText"].ToString(),
                imageRef = data.ContainsKey("imageRef") ? data["imageRef"].ToString() : null,
                answers = (data["answers"] as IEnumerable<object>).Select(a => a.ToString()).ToList(),
                correctAnswerIndex = System.Convert.ToInt32(data["correctAnswerIndex"]),
                videoRef_onSuccess = data.ContainsKey("videoRef_onSuccess") ? data["videoRef_onSuccess"].ToString() : null,
                nextQuestionID = data.ContainsKey("nextQuestionID") ? data["nextQuestionID"].ToString() : null
            };
            questionBank.Add(q.DocumentID, q); 
        }
        statusText.text = "";
    }

    void ShowQuestion(string questionID)
    {
        if (!questionBank.ContainsKey(questionID))
        {
            Debug.LogError("Помилка: Не знайдено питання з ID: " + questionID);
            ShowResults();
            return;
        }

        currentQuestion = questionBank[questionID]; 

        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Count; i++)
        {
            if (i < currentQuestion.answers.Count)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnAnswerClicked(int answerIndex)
    {
        answerButtons.ForEach(b => b.interactable = false); 

        if (answerIndex == currentQuestion.correctAnswerIndex)
        {
            statusText.text = "ПРАВИЛЬНО!";
            
            LoadNextQuestion(); 
        }
        else
        {
            statusText.text = "Помилка. Правильна відповідь: " + currentQuestion.answers[currentQuestion.correctAnswerIndex];
            
            answerButtons.ForEach(b => b.interactable = true); 
        }
    }

    void LoadNextQuestion()
    {
  
        if (!string.IsNullOrEmpty(currentQuestion.nextQuestionID))
        {
        
            ShowQuestion(currentQuestion.nextQuestionID);
        }
        else
        {
            
            ShowResults();
        }
        
        answerButtons.ForEach(b => b.interactable = true); 
    }

    void ShowResults()
    {
        questionText.text = "Тест Завершено!";
        statusText.text = "Ви відповіли на всі питання.";
        questionImage.gameObject.SetActive(false);
        answerButtons.ForEach(b => b.gameObject.SetActive(false));
    }
}
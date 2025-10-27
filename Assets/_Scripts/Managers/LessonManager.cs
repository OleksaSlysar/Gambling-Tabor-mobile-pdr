// LessonManager.cs
using UnityEngine;
using System.Collections.Generic;

public class LessonManager : MonoBehaviour
{
    public List<LessonData> allLessons;
    private LessonData currentLesson;
    private int currentObjectiveIndex;

    public void StartLesson(int lessonID)
    {
        currentLesson = allLessons[lessonID];
        currentObjectiveIndex = 0;
        StartObjective(currentObjectiveIndex);
    }

    void StartObjective(int index)
    {
        // ... Логіка для показу цілі гравцю (наприклад, "Доїдьте до точки X")
        // ... Активація тригерів на карті
    }

    public void ObjectiveCompleted()
    {
        currentObjectiveIndex++;
        if (currentObjectiveIndex < currentLesson.objectives.Count)
        {
            StartObjective(currentObjectiveIndex);
        }
        else
        {
            Debug.Log("Урок Завершено!");
            // ... Зберегти прогрес в БД
        }
    }
}
// LessonData.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Lesson", menuName = "Driving School/Lesson")]
public class LessonData : ScriptableObject
{
    public string lessonName;
    [TextArea] public string description;
    public List<ObjectiveData> objectives;
}

[System.Serializable]
public class ObjectiveData
{
    public string objectiveDescription;
    public ObjectiveType type;
    public Transform targetLocation; // Для завдань "Доїхати до..."
    // ... інші параметри ...
}

public enum ObjectiveType { GoToLocation, StopAtLine, Park, FollowRoute }
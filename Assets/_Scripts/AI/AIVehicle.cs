// _Scripts/AI/AIVehicle.cs
using UnityEngine;
using UnityEngine.AI; // Обов'язково для NavMeshAgent

[RequireComponent(typeof(NavMeshAgent))]
public class AIVehicle : Venicle // Наслідуємо наш базовий клас Vehicle
{
    private NavMeshAgent agent;
    
    [Header("AI Налаштування")]
    public Transform target; // Куди ми хочемо їхати

    // Ми будемо керувати агентом вручну,
    // тому нам потрібні ці змінні
    private float steeringInput;
    private float motorInput;
    private float brakeInput;

    public override void Start()
    {
        base.Start(); // Викликаємо Start() з Vehicle.cs (налаштує Rigidbody)
        agent = GetComponent<NavMeshAgent>();

        // Оскільки ми вимкнули UpdatePosition, нам треба
        // вручну синхронізувати агента з нашою машиною
        agent.Warp(transform.position); 
    }

    void Update()
    {
        // 1. Встановлюємо ціль для агента
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        // 2. Синхронізуємо позицію "мозку" (агента) з позицією "тіла" (машини)
        // Це потрібно, якщо машина відхилилась від курсу (напр., через занос)
        if (Vector3.Distance(transform.position, agent.nextPosition) > agent.height)
        {
            agent.Warp(transform.position);
        }

        // 3. Розраховуємо, куди "дивиться" мозок
        if (agent.hasPath)
        {
            // Беремо вектор бажаного руху від NavMesh
            Vector3 desiredVelocity = agent.desiredVelocity;

            // Перетворюємо цей глобальний вектор у локальний для машини
            // (тобто "наскільки вліво/вправо" і "наскільки вперед/назад")
            Vector3 localDesiredVelocity = transform.InverseTransformDirection(desiredVelocity.normalized);

            // localDesiredVelocity.x - це наш стіринг (від -1 до 1)
            steeringInput = Mathf.Clamp(localDesiredVelocity.x, -1f, 1f);

            // localDesiredVelocity.z - це наш газ (від -1 до 1, але нам треба 0-1)
            motorInput = Mathf.Clamp01(localDesiredVelocity.z);

            // Просте гальмування: якщо ми близько до цілі, гальмуємо
            if (agent.remainingDistance < agent.stoppingDistance && agent.desiredVelocity.magnitude < 0.5f)
            {
                motorInput = 0;
                brakeInput = 1;
            }
            else
            {
                brakeInput = 0;
            }
        }
        else
        {
            // Якщо шляху немає, стоїмо
            motorInput = 0;
            steeringInput = 0;
            brakeInput = 1;
        }
    }

    // 4. Передаємо розраховані значення у базовий клас Vehicle
    protected override float GetMotorInput()
    {
        return motorInput;
    }

    protected override float GetSteeringInput()
    {
        return steeringInput;
    }

    protected override float GetBrakeInput()
    {
        return brakeInput;
    }
}
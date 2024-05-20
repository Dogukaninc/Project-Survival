using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemySO enemySO;
    public EnemySO EnemySO => enemySO;
    
    
    [Header(" Enemy Properties ")] 
    private float movementSpeed;
    private float damagePower;
    
    [Space(10)]
    private Animator animator;

    private NavMeshAgent _navMeshAgent;
    private Health _health;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _health = GetComponent<Health>();
        
        movementSpeed = EnemySO.speed;
        damagePower = EnemySO.power;
    }
    
    void Update()
    {
        
    }

    public void TakeDamage(int damagePoint)
    {
        _health.currentHealth -= damagePoint;
        //TODO: Damage popup burda çalışacak
    }
}
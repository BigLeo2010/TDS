using TMPro;
using UnityEngine;

public class UnitLogic : MonoBehaviour
{
    [Header("Settings")]
    public float radius = 10f;
    public LayerMask targetMask;
    public float fireRate = 2f;
    public float damage = 1f;
    public int level = 0;

    public UnitBeh beh;

    [HideInInspector] public bool canDo = true;

    private float nextFireTime;
    private MovLogic currentTarget;

    private void Start()
    {
        InitializeUpgrade(level);
    }

    void Update()
    {
        if (!canDo) return;

        FindBestTarget();

        if (currentTarget != null && Time.time >= nextFireTime)
        {
            RotateToTarget(); 
            Shoot();
        }
    }

    void FindBestTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, radius, targetMask);

        float minDistanceToEnd = float.MaxValue;
        currentTarget = null;

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].TryGetComponent<MovLogic>(out var mov))
            {
                if (mov.distanceToEnd < minDistanceToEnd)
                {
                    minDistanceToEnd = mov.distanceToEnd;
                    currentTarget = mov;
                }
            }
        }
    }

    void RotateToTarget()
    {
        Vector3 direction = (currentTarget.transform.position - transform.position);
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void Shoot()
    {
        currentTarget.health -= damage;
        nextFireTime = Time.time + fireRate;
    }

    public void Upgrade(TextMeshProUGUI upgText)
    {
        if (level != 4)
        {
            if (FinanceManager.money >= beh.UpgradeCost[level])
            {
                FinanceManager.money -= beh.UpgradeCost[level];
                level++;
                InitializeUpgrade(level);
            }
        }
    }

    void InitializeUpgrade(int i)
    {
        radius = beh.RadiusUpgrade[i];
        fireRate = beh.FireUpgrade[i];
        damage = beh.DamageUpgrade[i];
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(transform.position, radius);
    }
}

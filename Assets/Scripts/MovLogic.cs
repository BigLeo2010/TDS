using UnityEngine;

public class MovLogic : MonoBehaviour
{
    [SerializeField] Data data;
    public ZombieBeh beh;

    public float health;

    [HideInInspector] public float distanceToEnd;

    private int currentPointIndex = 0;

    private void Start()
    {
        health = beh.health;
    }

    void Update()
    {
        if (data.points.Length == 0 || currentPointIndex >= data.points.Length) return;

        Vector3 targetPos = new Vector3(data.points[currentPointIndex].position.x, transform.position.y, data.points[currentPointIndex].position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, beh.speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            currentPointIndex++;
            if (currentPointIndex >= data.points.Length)
            {
                GameObject.FindWithTag("GameController").GetComponent<LevelHealth>().curHealth -= health;
                Destroy(gameObject);
                return;
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }

        // --- ÐÀÑ×ÅÒ ÄÈÑÒÀÍÖÈÈ ---

        float currentSegmentLength = 1f;
        if (currentPointIndex > 0)
        {
            currentSegmentLength = Vector3.Distance(data.points[currentPointIndex].position, data.points[currentPointIndex - 1].position);
        }

        float distToNextPoint = Vector3.Distance(transform.position, targetPos);

        float distancePassedOnSegment = currentSegmentLength - distToNextPoint;

        float progressPercent = Mathf.Clamp(distancePassedOnSegment / currentSegmentLength, 0, 1) * 100f;

        int remainingSegments = data.points.Length - currentPointIndex - 1;
        distanceToEnd = (remainingSegments * 100f) + (100f - progressPercent);
    }

}

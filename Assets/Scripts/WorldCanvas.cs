using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldCanvas : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxDistance = 1000f;
    [SerializeField] private LayerMask zombieLayer; 

    [Header("UI Elements")]
    [SerializeField] private GameObject enemyBarRoot;
    [SerializeField] private TextMeshProUGUI enemyHealthText;
    [SerializeField] private Image healthBarImage;

    private Camera _mainCam;
    private Transform _camTransform;

    void Start()
    {
        _mainCam = Camera.main;
        _camTransform = _mainCam.transform;

        if (enemyBarRoot) enemyBarRoot.SetActive(false);
    }

    void Update()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, zombieLayer))
        {
            if (hit.transform.TryGetComponent<MovLogic>(out var enemy))
            {
                UpdateEnemyUI(enemy, hit.transform.position);
            }
        }
        else if (enemyBarRoot.activeSelf)
        {
            enemyBarRoot.SetActive(false);
        }
    }

    private void UpdateEnemyUI(MovLogic enemy, Vector3 targetPos)
    {
        if (!enemyBarRoot.activeSelf) enemyBarRoot.SetActive(true);

        enemyBarRoot.transform.position = new Vector3(targetPos.x, enemy.transform.position.y + 2f, targetPos.z);
        enemyBarRoot.transform.rotation = _camTransform.rotation;

        float current = enemy.health;
        float max = enemy.beh.health;

        enemyHealthText.text = $"{current}/{max}";
        healthBarImage.fillAmount = current / max;
    }
}

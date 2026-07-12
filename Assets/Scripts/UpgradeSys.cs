using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeSys : MonoBehaviour
{
    private Camera _mainCam;
    private Transform cam;

    [Header("Parameters")]
    [SerializeField] GameObject UpgradePanel;
    [SerializeField] GameObject trackerCube;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI fireRateText;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI radiusText;

    [Header("Buttons")]
    [SerializeField] Button upgradeBtn;
    [SerializeField] Button sellBtn;
    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] TextMeshProUGUI sellText;

    void Start()
    {
        _mainCam = Camera.main;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        UpgradePanel.SetActive(false);
        trackerCube.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                if (hit.transform.TryGetComponent<UnitLogic>(out var unit))
                {
                    ShowPanel(unit, hit.transform.position);
                }
                else
                {
                    UpgradePanel.SetActive(false);
                    trackerCube.SetActive(false);
                }
            }
            else
            {
                UpgradePanel.SetActive(false);
                trackerCube.SetActive(false);
            }
        }

        RotatePanel(UpgradePanel.transform, cam);
        Vector3 angl = trackerCube.transform.position;
        angl.x = UpgradePanel.transform.position.x;
        angl.z = UpgradePanel.transform.position.z;
        trackerCube.transform.position = angl;
    }

    void ShowPanel(UnitLogic unit, Vector3 pos)
    {
        UpgradePanel.SetActive(true);
        trackerCube.SetActive(true);

        UpgradePanel.transform.position = new Vector3(pos.x, 5f, pos.z);

        levelText.text = $"Уровень: {unit.level + 1}";
        fireRateText.text = $"Скор.Стрел.: {unit.fireRate}";
        damageText.text = $"Урон: {unit.damage}";
        radiusText.text = $"Радиус: {unit.radius}";

        upgradeBtn.onClick.RemoveAllListeners();
        sellBtn.onClick.RemoveAllListeners();

        if (unit.level < unit.beh.UpgradeCost.Length-1)
        {
            upgradeText.text = $"Улучшить: {unit.beh.UpgradeCost[unit.level]}";
            upgradeBtn.interactable = true;
            upgradeBtn.onClick.AddListener(() => {
                unit.Upgrade(upgradeText);
                ShowPanel(unit, pos);
            });
        }
        else
        {
            upgradeText.text = "МАКСИМУМ";
            upgradeBtn.interactable = false;
        }
    }

    void RotatePanel(Transform panel, Transform camera)
    {
        float dist = Vector3.Distance(camera.position, panel.position);
        float height = camera.position.y - panel.position.y;
        float angleX = Mathf.Asin(height / dist) / Mathf.Deg2Rad;

        float k1 = camera.position.x - panel.position.x;
        float k2 = camera.position.z - panel.position.z;
        float angleY = Mathf.Atan(k1 / k2) / Mathf.Deg2Rad;

        Vector3 angle = panel.eulerAngles;
        angle.x = angleX;
        angle.y = angleY;
        panel.eulerAngles = angle;
    }
}

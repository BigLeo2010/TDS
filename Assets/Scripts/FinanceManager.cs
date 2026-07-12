using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinanceManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    public static float money = 500f;

    [SerializeField] GameObject[] unit;
    [SerializeField] float[] cost;
    [SerializeField] private LayerMask dragLayer;
    private bool canDrag = false;

    private Camera _mainCam;
    GameObject obj;

    void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        moneyText.text = money.ToString() + "$";

        if (canDrag && obj != null)
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, dragLayer))
            {
                Vector3 targetPos = new Vector3(hit.point.x, 0.7f, hit.point.z);
                obj.transform.position = Vector3.Lerp(obj.transform.position, targetPos, Time.deltaTime * 25f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                StopDragging();
            }
        }
    }

    public void BuyUnit(int index)
    {
        if (money >= cost[index])
        {
            money -= cost[index];
            obj = Instantiate(unit[index]);

            obj.layer = LayerMask.NameToLayer("Ignore Raycast");

            obj.GetComponent<UnitLogic>().canDo = false;
            canDrag = true;
        }
    }

    private void StopDragging()
    {
        obj.layer = LayerMask.NameToLayer("Default");
        obj.GetComponent<UnitLogic>().canDo = true;
        obj = null;
        canDrag = false;
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Inventory/Data")]
public class Data : ScriptableObject
{
    public Transform[] points;
    public UnitBeh[] unitBehs;
    public GameObject[] unitPrefs;
}

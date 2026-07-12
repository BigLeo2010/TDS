using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Inventory/Unit")]
public class UnitBeh : ScriptableObject
{
    public float cost;
    public float[] UpgradeCost;
    public float[] SellCost;
    public float[] DamageUpgrade;
    public float[] RadiusUpgrade;
    public float[] FireUpgrade;
}

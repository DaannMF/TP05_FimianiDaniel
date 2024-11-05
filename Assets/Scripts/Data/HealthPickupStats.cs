using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPickupStats", menuName = "HealthPickup/Stats")]
public class HealthPickupStats : ScriptableObject {
    public Int16 healthAmount = 1;
}

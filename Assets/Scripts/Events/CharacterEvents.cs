using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents {
    public static UnityAction<GameObject, Int16> characterDamaged;
    public static UnityAction<GameObject, Int16> characterHealed;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "ScriptableObjects/Ammo")]
public class Ammo : ScriptableObject
{
    public string ammoName;
    public Sprite sprite;
    public float damage;
}

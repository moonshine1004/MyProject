using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
        
    }
    public int damage;
    
}

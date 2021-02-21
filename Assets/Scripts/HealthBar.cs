using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer barFill;

    public void UpdateDisplay(float health, float maxHealth)
    {
        barFill.size = new Vector2(health / maxHealth, barFill.size.y);
    }
}

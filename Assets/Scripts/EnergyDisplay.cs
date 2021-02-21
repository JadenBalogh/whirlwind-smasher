using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform barFill;

    void Start()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.AddEnergyChangedListener(UpdateEnergy);
    }

    private void UpdateEnergy(float energy, float maxEnergy)
    {
        barFill.anchorMax = new Vector2(barFill.anchorMax.x, energy / maxEnergy);
    }
}

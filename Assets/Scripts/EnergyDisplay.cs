using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform barFill;

    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.AddEnergyChangedListener(UpdateEnergy);
    }

    private void UpdateEnergy(float energy)
    {
        barFill.anchorMax = new Vector2(barFill.anchorMax.x, energy / player.MaxEnergy);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health health = null;
    [SerializeField] private GameObject healthBarParent = null;
    [SerializeField] private Image healthBarImage = null;

    private void Awake()
    {
        health.ClientOnHealthUpdated += HandleHealthUpdated;
    }

    private void OnDestroy()
    {
        health.ClientOnHealthUpdated -= HandleHealthUpdated;
    }

    //Turned this off since I want to be able to see the health bars at all times. If activate this turn the HealthBarCanvas off in inspector
    // private void OnMouseEnter() 
    // {
    //     healthBarParent.SetActive(true);
    // }

    // private void OnMouseExit() 
    // {
    //     healthBarParent.SetActive(false);
    // }

    private void HandleHealthUpdated(int currentHealth, int maxHealth)
    {
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;

        if( currentHealth != maxHealth )
        {
            healthBarParent.SetActive(true);
        } else
        {
            healthBarParent.SetActive(false);
        }
    }
}

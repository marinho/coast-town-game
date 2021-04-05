using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinanceSettingsUI : MonoBehaviour
{
    public Slider bondsPercentageSlider;
    public GameObject backendHandler;

    private FinanceSettings financeSettings;

    // Start is called before the first frame update
    void Start()
    {
        financeSettings = backendHandler.GetComponent<FinanceSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        bondsPercentageSlider.value = financeSettings.GetBondsPercentage();
    }
}

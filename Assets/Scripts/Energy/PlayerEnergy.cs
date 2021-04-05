using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    public Slider energyBarSlider;
    public GameObject energyBarSliderHandler;
    public GameObject energyBarSliderFill;

    public Sprite fullEnergySprite;
    public Sprite highEnergySprite;
    public Sprite mediumEnergySprite;
    public Sprite lowEnergySprite;
    [SerializeField] private Gradient energyColorGradient;

    public float currentEnergyLevel; // 0 to 4

    private void Start()
    {
        LoadCurrentEnergyLevel();
    }

    public void LoadCurrentEnergyLevel()
    {
        if (PlayerPrefs.HasKey(EnergyPrefKeys.CurrentEnergyLevel))
        {
            currentEnergyLevel = PlayerPrefs.GetFloat(EnergyPrefKeys.CurrentEnergyLevel);
        }
        else
        {
            currentEnergyLevel = 4;
            PlayerPrefs.SetFloat(EnergyPrefKeys.CurrentEnergyLevel, currentEnergyLevel);
        }
    }

    private void Update()
    {
        energyBarSlider.value = currentEnergyLevel - 1;

        var handlerImage = energyBarSliderHandler.GetComponent<Image>();
        if (currentEnergyLevel >= 4)
        {
            handlerImage.sprite = fullEnergySprite;
        }
        else if (currentEnergyLevel > 2.5)
        {
            handlerImage.sprite = highEnergySprite;
        }
        else if (currentEnergyLevel > 1)
        {
            handlerImage.sprite = mediumEnergySprite;
        }
        else
        {
            handlerImage.sprite = lowEnergySprite;
        }

        if (energyColorGradient != null && energyBarSliderFill != null)
        {
            var colorPosition = (currentEnergyLevel - 1f) / 3f;
            var energyColor = energyColorGradient.Evaluate(colorPosition);
            energyBarSliderFill.GetComponent<Image>().color = energyColor;
        }

    }
}
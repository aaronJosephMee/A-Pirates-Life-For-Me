using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject descriptionTextHolder;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetDescriptionText(String text)
    {
        descriptionText.text = text;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionTextHolder.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionTextHolder.SetActive(false);
    }
}

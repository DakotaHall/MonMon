using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectedButtonScript : MonoBehaviour, ISelectHandler,IDeselectHandler
{
    public TextMeshProUGUI buttonText;
    public Material selectedMaterial;
    public Material normalMaterial;
    public void OnSelect(BaseEventData eventData){
        buttonText.fontMaterial = selectedMaterial;
    }

    public void OnDeselect(BaseEventData eventData){
        buttonText.fontMaterial = normalMaterial;
    }
}

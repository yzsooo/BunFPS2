using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSelectionUI : MonoBehaviour
{
    public RectTransform selectionMenu;

    public TextMeshProUGUI currentSelectionText;
    public Image currentSelectionImage;

    private void Awake()
    {
        selectionMenu.gameObject.SetActive(false);
    }

    public void ToggleSelection(bool bOpen)
    {
        selectionMenu.gameObject.SetActive(bOpen);
    }

    // update the name and image of the current selection
    public void UpdateCurrentSelection(string newName, Sprite newImage)
    {
        currentSelectionText.text = newName;
        currentSelectionImage.sprite = newImage;
    }
}

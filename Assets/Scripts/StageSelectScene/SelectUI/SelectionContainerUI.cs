using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectionContainerUI : MonoBehaviour
{
    public StageSelectUI parent;

    public TextMeshProUGUI selectionName;
    public Image selectionIcon;

    private void Awake()
    {
        SetContainerUI();
        // add selectthiscontainer to the listener
        GetComponent<Button>().onClick.AddListener(SelectThisContainer);
    }

    // virtual func for setting up the container ui with names and icon
    public virtual void SetContainerUI()
    {

    }

    // virtual func for when this container is pressed
    public virtual void SelectThisContainer()
    {
        
    }
}

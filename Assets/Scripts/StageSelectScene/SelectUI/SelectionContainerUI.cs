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

    public virtual void SetContainerUI()
    {

    }

    public virtual void SelectThisContainer()
    {
        
    }
}

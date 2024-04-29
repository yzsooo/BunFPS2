using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageContainerUI : SelectionContainerUI
{
    public LevelInfoScriptableObject levelInfo;

    public override void SetContainerUI()
    {
        selectionName.text = levelInfo.LevelName;
    }

    public override void SelectThisContainer()
    {
        parent.SelectStage(levelInfo);
    }
}

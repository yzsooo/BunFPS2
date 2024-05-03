using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageContainerUI : SelectionContainerUI
{
    public LevelInfoScriptableObject levelInfo;

    // set container's name to the level name
    public override void SetContainerUI()
    {
        selectionName.text = levelInfo.LevelName;
    }

    // set the selected stage to this level
    public override void SelectThisContainer()
    {
        parent.SelectStage(levelInfo);
    }
}

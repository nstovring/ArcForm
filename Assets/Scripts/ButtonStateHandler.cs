using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonStateHandler
{
    public enum ToggleState { Default, Hover, Selected, Edited }
    public ToggleState myState = ToggleState.Default;
    public Color DefaultColor;
    public Color SelectedColor;
    public Color EditedColor;

    public Image myButtonImage;
    public void SetToEdited()
    {
        myButtonImage.CrossFadeColor(EditedColor, 0.1f, true, false);
        myState = ToggleState.Edited;
    }

    public void SetToSelected()
    {
        myButtonImage.CrossFadeColor(SelectedColor, 0.1f, true, false);
        myState = ToggleState.Selected;
    }

    public void SetToDefault()
    {
        myButtonImage.CrossFadeColor(DefaultColor, 0.1f, true, false);
        myState = ToggleState.Default;
    }
}


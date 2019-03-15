using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ILabelable
{
    TextMeshPro MyLabel { get;}

    void ShowInputField();
    void SetLabel(string label);
}

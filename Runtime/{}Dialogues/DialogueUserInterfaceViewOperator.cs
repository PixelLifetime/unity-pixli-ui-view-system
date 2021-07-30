using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixLi;

public class DialogueUserInterfaceViewOperator : MonoBehaviour
{
	public void DisplayDialogue(Dialogue dialogue) => DialogueUserInterfaceView._Instance.DisplayDialogue(dialogue: dialogue);
	public void DisplayNext() => DialogueUserInterfaceView._Instance.DisplayNext();
}
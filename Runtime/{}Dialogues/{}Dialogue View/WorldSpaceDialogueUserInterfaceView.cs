using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

namespace PixLi
{
	public class WorldSpaceDialogueUserInterfaceView : DialogueUserInterfaceView
	{
		[SerializeField] private Transform _worldSpaceTarget;

		public override void DisplayDialogue(Dialogue dialogue)
		{
			this.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(this._worldSpaceTarget.position);

			base.DisplayDialogue(dialogue);
		}

#if UNITY_EDITOR
		//protected override void OnDrawGizmos()
		//{
		//}

		[CustomEditor(typeof(WorldSpaceDialogueUserInterfaceView))]
		[CanEditMultipleObjects]
		public class WorldSpaceDialogueUserInterfaceViewEditor : ViewEditor
		{
		}
#endif
	}
}
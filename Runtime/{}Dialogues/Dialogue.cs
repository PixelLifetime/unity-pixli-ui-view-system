using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
	[CreateAssetMenu(fileName = "s Dialogue", menuName = "Storytelling/Dialogue")]
	public class Dialogue : ScriptableObject
	{
		[SerializeField] private List<DialogueData> _dialogueData;
		public List<DialogueData> _DialogueData => this._dialogueData;

#if UNITY_EDITOR
		[CustomEditor(typeof(Dialogue))]
		[CanEditMultipleObjects]
		public class DialogueEditor : Editor
		{
			private void OnEnable()
			{

			}

			public override void OnInspectorGUI()
			{
				this.DrawDefaultInspector();

#pragma warning disable 0219
				Dialogue sDialogue = this.target as Dialogue;
#pragma warning restore 0219
			}
		}
#endif
	}

	[System.Serializable]
	public class DialogueData
	{
		[SerializeField] [TextArea(1, 100)] private string _sentenceText;
		public string _SentenceText { get { return this._sentenceText; } }

		[SerializeField] private AudioClip _audioCover;
		public AudioClip _AudioCover { get { return this._audioCover; } }

		[SerializeField] private Collocutor _collocutor;
		public Collocutor _Collocutor { get { return this._collocutor; } }
	}
}
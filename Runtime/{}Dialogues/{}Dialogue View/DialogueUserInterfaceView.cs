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
	public class DialogueUserInterfaceView : View
	{
		[System.Serializable]
		public class DialogueUserInterfaceViewDisplayData
		{
			public DialogueData DialogueData;

			public DialogueUserInterfaceViewDisplayData(DialogueData dialogueData)
			{
				this.DialogueData = dialogueData;
			}
		}

		[System.Serializable]
		public class DialogueUserInterfaceViewOutput
		{
			[SerializeField] private TextMeshProUGUI _messageTextField;
			public TextMeshProUGUI _MessageTextField => this._messageTextField;

			[SerializeField] private TextMeshProUGUI _collocutorNameTextField;
			public TextMeshProUGUI _CollocutorNameTextField => this._collocutorNameTextField;

			[SerializeField] private Image _collocutorIconImageField;
			public Image _CollocutorIconImageField => this._collocutorIconImageField;
		}

		[SerializeField] protected DialogueUserInterfaceViewOutput viewOutput;
		public DialogueUserInterfaceViewOutput _ViewOutput => this.viewOutput;

		[SerializeField] private AudioClipInterruptivePlaybackEngine _audioClipInterruptivePlaybackEngine;
		public AudioClipInterruptivePlaybackEngine _AudioClipInterruptivePlaybackEngine => this._audioClipInterruptivePlaybackEngine;

		private Coroutine _voiceOverPlaybackCoroutine;

		public void Display(DialogueUserInterfaceViewDisplayData displayData)
		{
			this.Show();

			DialogueData dialogueData = displayData.DialogueData;

			this._audioClipInterruptivePlaybackEngine.Play(audioClip: dialogueData._AudioCover);

			this.viewOutput._MessageTextField.text = dialogueData._SentenceText;

			this.viewOutput._CollocutorNameTextField.text = dialogueData._Collocutor._ProfileName;
			this.viewOutput._CollocutorIconImageField.sprite = dialogueData._Collocutor._ProfileIcon;

			if (this._voiceOverPlaybackCoroutine != null)
				this.StopCoroutine(this._voiceOverPlaybackCoroutine);

			this._voiceOverPlaybackCoroutine = this.StartCoroutine(
				routine: CoroutineProcessorsCollection.InvokeAfter(
					seconds: dialogueData._AudioCover.length,
					action: () =>
					{
						this.DisplayNext();
					}
				)
			);
		}

		private DialogueUserInterfaceViewDisplayData _displayedData;
		private Dialogue _displayedDialogue;
		private int _displayDialogueDataIndex;

		public virtual void DisplayDialogue(Dialogue dialogue)
		{
			this._displayedDialogue = dialogue;
			this._displayedData = new DialogueUserInterfaceViewDisplayData(this._displayedDialogue._DialogueData[0]);

			this._displayDialogueDataIndex = 0;

			this.Display(this._displayedData);
		}

		public void DisplayNext()
		{
			this._displayDialogueDataIndex++;

			if (this._displayDialogueDataIndex < this._displayedDialogue._DialogueData.Count)
			{
				this._displayedData.DialogueData = this._displayedDialogue._DialogueData[this._displayDialogueDataIndex];
				this.Display(this._displayedData);
			}
			else
				this.Hide();
		}

		public static MonoBehaviourSingletonEmbedded<DialogueUserInterfaceView> S_Singleton_ { get; private set; }
		public static DialogueUserInterfaceView _Instance => S_Singleton_._Instance;

		protected override void Awake()
		{
			base.Awake();

			S_Singleton_ = new MonoBehaviourSingletonEmbedded<DialogueUserInterfaceView>(this);
		}

#if UNITY_EDITOR
		//protected override void OnDrawGizmos()
		//{
		//}

		[CustomEditor(typeof(DialogueUserInterfaceView))]
		[CanEditMultipleObjects]
		public class DialogueUserInterfaceViewEditor : ViewEditor
		{
		}
#endif
	}
}
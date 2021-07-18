using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
	[CreateAssetMenu(fileName = "s Collocutor", menuName = "Storytelling/Collocutor")]
	public class Collocutor : ScriptableObject
	{
		[SerializeField] private string _profileName;
		public string _ProfileName => this._profileName;

		[SerializeField] private Sprite _profileIcon;
		public Sprite _ProfileIcon => this._profileIcon;

#if UNITY_EDITOR
		[CustomEditor(typeof(Collocutor))]
		[CanEditMultipleObjects]
		public class CollocutorEditor : Editor
		{
			private void OnEnable()
			{

			}

			public override void OnInspectorGUI()
			{
				this.DrawDefaultInspector();

#pragma warning disable 0219
				Collocutor sCollocutor = target as Collocutor;
#pragma warning restore 0219
			}
		}
#endif
	}
}
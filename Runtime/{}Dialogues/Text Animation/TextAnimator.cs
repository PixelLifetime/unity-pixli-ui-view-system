using System.Collections;
using System.Collections.Generic;
using System.Text;
using PixLi;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextAnimator : MonoBehaviour
{
	[Header("Typing")]

	[Range(0.01f, 1f)]
	[SerializeField] private float _minNewCharacterDelayTime;
	public float _MinNewCharacterDelayTime => this._minNewCharacterDelayTime;

	[Range(0.01f, 1f)]
	[SerializeField] private float _maxNewCharacterDelayTime;
	public float _MaxNewCharacterDelayTime => this._maxNewCharacterDelayTime;

	[Header("Audio")]

	[Range(0.01f, 1f)]
	[SerializeField] private float _minNewCharacterSoundDelayTime;
	public float _MinNewCharacterSoundDelayTime => this._minNewCharacterSoundDelayTime;

	[Range(0.01f, 1f)]
	[SerializeField] private float _maxNewCharacterSoundDelayTime;
	public float _MaxNewCharacterSoundDelayTime => this._maxNewCharacterSoundDelayTime;

	[SerializeField] private AudioClipArchive _audioClipArchive;
	public AudioClipArchive _AudioClipArchive => this._audioClipArchive;

	private TMP_Text _textMesh;

	private Coroutine _textAnimationSoundCoroutine;

	private IEnumerator TextAnimationSoundProcess()
	{
		while (true)
		{
			AudioPlayer._Instance.Play(audioClip: this._audioClipArchive.Random(), idTag: IdTag.Audio.SoundEffect);

			yield return new WaitForSeconds(Random.Range(this._minNewCharacterSoundDelayTime, this._maxNewCharacterSoundDelayTime));
		}
	}

	private IEnumerator TextAnimationProcess(string text, System.Action onFinished)
	{
		this._textAnimationSoundCoroutine = this.StartCoroutine(this.TextAnimationSoundProcess());

		char[] characters = text.ToCharArray();

		StringBuilder stringBuilder = new StringBuilder(capacity: characters.Length);

		for (int a = 0; a < characters.Length; a++)
		{
			stringBuilder.Append(characters[a]);

			this._textMesh.text = stringBuilder.ToString();

			yield return new WaitForSeconds(Random.Range(this._minNewCharacterDelayTime, this._maxNewCharacterDelayTime));
		}

		this.StopCoroutine(this._textAnimationSoundCoroutine);
		
		onFinished?.Invoke();
	}

	private Coroutine _textAnimationCoroutine;

	public void StopAnimationProcesses()
	{
		if (this._textAnimationCoroutine != null)
		{
			this.StopCoroutine(this._textAnimationCoroutine);
			this.StopCoroutine(this._textAnimationSoundCoroutine);
		}
	}

	public void SetText(string text, System.Action onFinished)
	{
		this.StopAnimationProcesses();

		this._textAnimationCoroutine = this.StartCoroutine(
			this.TextAnimationProcess(text: text, onFinished: onFinished)
		);
	}

	private void Awake()
	{
		this._textMesh = this.GetComponent<TMP_Text>();
	}
}
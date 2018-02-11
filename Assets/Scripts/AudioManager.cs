using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX
{
	Jump,
	Teleport,
	Ascend,
	Squirt,
	PlayerHit,
	Steam
}

public class AudioManager : MonoBehaviour {

    public static AudioManager instance { get; private set; }

    [HideInInspector]
    public AudioSource _audioSource;

    [SerializeField]
    private AudioClip _jumpSFX;
	[SerializeField]
    private AudioClip _teleportSFX;
	[SerializeField]
    private AudioClip _ascendSFX;
	[SerializeField]
    private AudioClip _squirtSFX;
	[SerializeField]
    private AudioClip _playerHitSFX;
	[SerializeField]
    private AudioClip _steamSFX;
	

    private void Awake()
	{
        DontDestroyOnLoad(gameObject);
        instance = this;
	}

	private void Start()
	{
        _audioSource = this.GetComponent<AudioSource>();
    }

	public void PlayAudio(AudioSource source, SFX sfx)
	{
		switch(sfx)
		{
			case SFX.Jump:
                source.clip = _jumpSFX;
                source.Play();
                break;
			case SFX.Teleport:
                source.clip = _teleportSFX;
                source.Play();
                break;
			case SFX.Ascend:
                source.clip = _ascendSFX;
                source.Play();
                break;
			case SFX.Squirt:
                source.clip = _squirtSFX;
                source.Play();
                break;
			case SFX.PlayerHit:
                source.clip = _playerHitSFX;
                source.Play();
                break;
			case SFX.Steam:
                source.clip = _steamSFX;
                source.Play();
                break;
        }
	}
}

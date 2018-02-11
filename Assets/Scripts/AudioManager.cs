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

    private AudioSource _audioSource;

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

	public void PlayAudio(SFX sfx)
	{
		switch(sfx)
		{
			case SFX.Jump:
                _audioSource.PlayOneShot(_jumpSFX);
                break;
			case SFX.Teleport:
                _audioSource.PlayOneShot(_teleportSFX);
                break;
			case SFX.Ascend:
                _audioSource.PlayOneShot(_ascendSFX);
                break;
			case SFX.Squirt:
                _audioSource.PlayOneShot(_squirtSFX);
                break;
			case SFX.PlayerHit:
                _audioSource.PlayOneShot(_playerHitSFX);
                break;
			case SFX.Steam:
                _audioSource.PlayOneShot(_steamSFX);
                break;
        }
	}
}

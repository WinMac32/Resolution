using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotionMeterManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _lotionObjects;
    [SerializeField]
    private Image[] _lotions;

    private LotionManager _lotionManager;
    private float _maxLotion;
    private float _currentLotion;
    private int _lastLotionBottleCount = 0;
    private int _currentLotionBottleCount;
    private int _lotionsPerBottle;
    private bool _onRefill;

    void Start()
	{
        _lotionManager = GameManager.instance.lotionManager;
		_lotionManager.onUseLotion += HandleOnUseLotion;
        _lotionManager.onRefillLotion += HandleOnRefillLotion;

        _maxLotion = _lotionManager.maxLotion;

        for (int i = 0; i < _lotions.Length; ++i)
		{
            _lotions[i].enabled = false;
        }

		for (int i = 0; i < _lotionObjects.Length; ++i)
		{
			_lotionObjects[i].SetActive(false);
		}
		_currentLotionBottleCount = 4;
		UpdateBottleDisplay ();
    }

    void OnDestroy()
    {
        _lotionManager.onUseLotion -= HandleOnUseLotion;
        _lotionManager.onRefillLotion -= HandleOnRefillLotion;
    }   

    private void HandleOnRefillLotion()
    {
        _onRefill = true;
        HandleUse();
    }

	private void HandleOnUseLotion()
	{
        _onRefill = false;
        HandleUse();
    }

    private void HandleUse()
    {
        _currentLotion = _lotionManager.lotionStash;
        _lotionsPerBottle = (int)(_maxLotion / _lotionObjects.Length);
        _currentLotionBottleCount = Mathf.FloorToInt((_currentLotion - 1) / _lotionsPerBottle);

        if (_currentLotionBottleCount >= 0)
        {
            // Only update the display if we need to activate or deactivate some bottles
            if (_lastLotionBottleCount != _currentLotionBottleCount)
            {
                UpdateBottleDisplay();
            }

            if (_onRefill)
            {
                UpdateLotionAmountOnRefill();
            }
            else
            {
                UpdateLotionAmount();
            }
        }

        _lastLotionBottleCount = _currentLotionBottleCount;
    }

	private void UpdateBottleDisplay()
	{
		for (int i = 0; i < _lotionObjects.Length; ++i)
		{
            _lotionObjects[i].SetActive(i <= _currentLotionBottleCount);
        }

        for (int i = 0; i < _lotions.Length; ++i)
        {
            _lotions[i].enabled = (i <= _currentLotionBottleCount);
        }
    }

	private void UpdateLotionAmount()
	{
        float fillRatio = 0f;
        if (Mathf.FloorToInt(_currentLotion / _lotionsPerBottle) > _currentLotionBottleCount)
        {
            fillRatio = 1f;
        }
        else
        {
            fillRatio = (_currentLotion % _lotionsPerBottle) / _lotionsPerBottle;
        }

        _lotions[_currentLotionBottleCount].fillAmount = fillRatio;
    }

    private void UpdateLotionAmountOnRefill()
    {
        for (int i = _lastLotionBottleCount; i < _currentLotionBottleCount; ++i)
        {
            _lotions[i].fillAmount = 1f;
        }

        UpdateLotionAmount();
    }
}

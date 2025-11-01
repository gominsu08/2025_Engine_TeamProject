using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using PSW.Code.Container;
using PSW.Code.Sawtooth;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PSW.Code.Resource
{
    public class ItemDataPanel : MonoBehaviour
    {
        [SerializeField] private ItemListSO itemListSO;
        [SerializeField] private ResourceContainer resourceContainer;
        [SerializeField] private SawtoothSystem rootSawtooth;

        [SerializeField] private float time;


        private Dictionary<Tier, TierPanel> _panelDic = new Dictionary<Tier, TierPanel>();
        private WaitForSeconds wait = new WaitForSeconds(0.5f);

        private RectTransform _rectTransform;
        private bool _isLeft;
        private float _moveValue;
        private int _moveCount;

        private float _targetMoveValue;

        private string _soundName = "Button";

        private void Start()
        {
            _targetMoveValue = transform.localPosition.x;
            GetComponentsInChildren<TierPanel>().ToList().ForEach(v => _panelDic.Add(v.GetTier(), v));

            Dictionary<Tier, List<ItemSO>> itemListDic = new Dictionary<Tier, List<ItemSO>>();
            foreach (ItemSO item in itemListSO.itemSOList)
            {
                if (itemListDic.TryGetValue(item.tier, out List<ItemSO> itemList))
                    itemList.Add(item);
                else
                {
                    List<ItemSO> tempList = new();
                    tempList.Add(item);
                    itemListDic.Add(item.tier, tempList);
                }
            }


            for (int i = 1; i <= (int)Tier.ThirdTier; ++i)
                _panelDic[(Tier)i].Init(itemListDic[(Tier)i], resourceContainer);

            _rectTransform = GetComponent<RectTransform>();
        }

        public void PopUpPanel()
        {
            if (rootSawtooth.GetIsStopRotation() == false) return;

            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound(_soundName);

            _isLeft = !_isLeft;
            rootSawtooth.StartSawtooth(time, _isLeft, transform);
            _moveValue = _rectTransform.sizeDelta.x / time;
            StartCoroutine(PopDownPanel());
        }

        private IEnumerator PopDownPanel()
        {
            if (_isLeft == false)
            {
                _targetMoveValue += _moveValue;
                _moveCount--;
            }
            else
            {
                _targetMoveValue -= _moveValue;
                _moveCount++;
            }

            transform.DOLocalMoveX(_targetMoveValue, 0.5f);

            yield return wait;

            if (_moveCount < time && _moveCount > 0)
                StartCoroutine(PopDownPanel());
            else
            {
                rootSawtooth.SawtoothStop(false);
                rootSawtooth.ResetSawtooth();
            }
        }

    }

}
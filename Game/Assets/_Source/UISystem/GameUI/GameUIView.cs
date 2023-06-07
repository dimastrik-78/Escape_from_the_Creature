using System;
using UnityEngine;
using Utils;
using Utils.Event;

namespace UISystem.GameUI
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private GameObject hint;

        private void Awake()
        {
            Signals.Get<WinSignal>().AddListener(EndGame);
            Signals.Get<LoseSignal>().AddListener(EndGame);
        }

        public void HintEnable()
        {
            hint.SetActive(true);
        }

        public void HintDisable()
        {
            hint.SetActive(false);
        }

        private void EndGame()
        {
            gameObject.SetActive(false);
            
            Signals.Get<WinSignal>().RemoveListener(EndGame);
            Signals.Get<LoseSignal>().RemoveListener(EndGame);
        }
    }
}

using UnityEngine;

namespace UISystem.GameUI
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private GameObject hint;

        public void HintEnable()
        {
            hint.SetActive(true);
        }

        public void HintDisable()
        {
            hint.SetActive(false);
        }
    }
}

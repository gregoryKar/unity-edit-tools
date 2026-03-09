using UnityEngine;
using UnityEngine.UI;

namespace Karianakis.EditTools
{
    internal class DynamicDebugContainer : MonoBehaviour
    {

        [SerializeField] Image _icon;
        [SerializeField] TMPro.TextMeshProUGUI _contentText;
        //[SerializeField] GameObject _mainButton;

        public Image Icon => _icon;
        public TMPro.TextMeshProUGUI ContentText => _contentText;

    }
}

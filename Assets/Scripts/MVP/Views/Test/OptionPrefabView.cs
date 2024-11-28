using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPrefabView : MonoBehaviour
{
	[field: SerializeField] public Toggle RadioButton { get; private set; }
	[field: SerializeField] public TMP_Text Answer { get; private set; }
}
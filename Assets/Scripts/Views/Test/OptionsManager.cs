using UnityEngine;
using UnityEngine.UI;

public class OptionsManager
{
	private readonly Transform _optionsContainer;
	private readonly OptionPrefabView _optionPrefab;

	public OptionsManager(Transform optionsContainer, OptionPrefabView optionPrefab)
	{
		_optionsContainer = optionsContainer;
		_optionPrefab = optionPrefab;
	}

	public ToggleGroup CreateOptions(TestQuestion question)
	{
		ClearOptions();

		if (!_optionsContainer.TryGetComponent(out ToggleGroup toggleGroup))
		{
			toggleGroup = _optionsContainer.gameObject.AddComponent<ToggleGroup>();
		}

		foreach (TestOption option in question.Options)
		{
			OptionPrefabView optionObject = Object.Instantiate(_optionPrefab, _optionsContainer.position, Quaternion.identity, _optionsContainer);
			optionObject.Answer.text = option.Text;
			optionObject.RadioButton.group = toggleGroup;
		}

		return toggleGroup;
	}

	private void ClearOptions()
	{
		foreach (Transform child in _optionsContainer)
		{
			Object.Destroy(child.gameObject);
		}
	}
}
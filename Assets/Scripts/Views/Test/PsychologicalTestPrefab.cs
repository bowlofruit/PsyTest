using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PsychologicalTestPrefab : MonoBehaviour, IPointerClickHandler
{
	[field: SerializeField] public TMP_Text Name { get; private set;}
	[field: SerializeField] public TMP_Text Description { get; private set; }
	[field: SerializeField] public Image Logo { get; private set; }

	private TestContainer _testContainer;
	private TestPresenter _presenter;

	public void Init(string name, string description, Sprite logo, TestContainer testContainer, TestPresenter presenter)
	{
		Name.text = name;
		Description.text = description;
		Logo.sprite = logo;
		_testContainer = testContainer;
		_presenter = presenter;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		_presenter.StartTest(_testContainer);
	}
}
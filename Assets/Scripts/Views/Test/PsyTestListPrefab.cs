using Presenter.PsyTest;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public partial class PsyTestListPrefab : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private TMP_Text Name;
	[SerializeField] private TMP_Text Description;
	[SerializeField] private Image Logo;

	private TestContainer _testContainer;
	private TestSelectionMediator _testSelectionMediator;

	[Inject]
	public void Construct(TestSelectionMediator testSelectionMediator)
	{
		_testSelectionMediator = testSelectionMediator;
	}

	public void Init(string name, string description, Sprite logo, TestContainer testContainer)
	{
		Name.text = name;
		Description.text = description;
		Logo.sprite = logo;
		_testContainer = testContainer;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		_testSelectionMediator.SelectTest(_testContainer);
	}
}
// <copyright file="AsssetViewer.cs" company="beffio">
// Copyright (c) 2014 All Rights Reserved
// </copyright>
// <summary>This is script for view assets</summary>

using System;
using UnityEngine;

public sealed class AssetsViewer : MonoBehaviour
{
	[Serializable]
	public class AssetsGroup
	{
		public string Name;
		public KeyCode SelectGroupKey;
		public GameObject[] Items;
	}

	private enum NavigationButtonMode
	{
		ChangeItem,
		ChangeGroup,
	}

	[SerializeField]
	private float m_controlsHeight;

	[SerializeField]
	private Vector2 m_controlsOffset;

	[SerializeField]
	private Vector2 m_controlsSpacing;

	[SerializeField]
	private float m_groupButtonWidth;

	[SerializeField]
	private AssetsGroup[] m_groups;

	[SerializeField]
	private bool m_GUIEnabled;

	[SerializeField]
	private NavigationButtonMode m_navigationButtonMode;

	[SerializeField]
	private float m_navigationButtonWidth;

	[SerializeField]
	private KeyCode m_switchNextItemKey;

	[SerializeField]
	private KeyCode m_switchPrevItemKey;

	[SerializeField]
	private Vector2 m_timeScaleRange;

	[SerializeField]
	private KeyCode m_toggleGUIKey;

	private int m_currentGroupIndex;
	private int m_currentItemIndex;

	private GameObject CurrentItem
	{
		get
		{
			if (m_groups == null || m_groups.Length <= m_currentGroupIndex)
			{
				return null;
			}

			GameObject[] items = m_groups[m_currentGroupIndex].Items;
			return items != null && items.Length > m_currentItemIndex ? items[m_currentItemIndex] : null;
		}
	}

	private int CurrentGroupItemsCount
	{
		get
		{
			if (m_groups == null || m_groups.Length <= m_currentGroupIndex)
			{
				return 0;
			}

			GameObject[] items = m_groups[m_currentGroupIndex].Items;
			return items != null ? items.Length : 0;
		}
	}

	private string CurrentGroupName
	{
		get
		{
			if (m_groups == null || m_groups.Length <= m_currentGroupIndex)
			{
				return "";
			}

			return m_groups[m_currentGroupIndex].Name;
		}
	}

	#region Unity core events.
	private void Awake()
	{
		m_currentGroupIndex = 0;
		m_currentItemIndex = 0;

		if (m_groups != null)
		{
			foreach (AssetsGroup group in m_groups)
			{
				if (group.Items != null)
				{
					foreach (GameObject item in group.Items)
					{
						if (item != null)
						{
							item.SetActive(false);
						}
					}
				}
			}
		}

		SetActiveCurrentItem(true);
	}

	private void LateUpdate()
	{
		if (m_groups == null || m_groups.Length == 0)
		{
			return;
		}

		if (Input.GetKeyUp(m_toggleGUIKey))
		{
			m_GUIEnabled = !m_GUIEnabled;
		}

		if (Input.GetKeyUp(m_switchPrevItemKey))
		{
			ConditionalSwitchToPrevItem();
		}

		if (Input.GetKeyUp(m_switchNextItemKey))
		{
			ConditionalSwitchToNextItem();
		}

		for (int i = 0; i < m_groups.Length; ++i)
		{
			if (Input.GetKeyUp(m_groups[i].SelectGroupKey))
			{
				ChangeCurrentGroup(i);
			}
		}
	}

	private void OnGUI()
	{
		if (!m_GUIEnabled || m_groups == null || m_groups.Length == 0)
		{
			return;
		}

		float currentVerticalOffset = m_controlsOffset.y;

		GUIContent labelContent = new GUIContent("Time scale");
		Vector2 labelSize = GUI.skin.label.CalcSize(labelContent);
		GUI.Label(new Rect(m_controlsOffset.x, currentVerticalOffset, labelSize.x, labelSize.y), labelContent);

		currentVerticalOffset += labelSize.y + m_controlsSpacing.y;

		Time.timeScale = GUI.HorizontalSlider(new Rect(m_controlsOffset.x, currentVerticalOffset, m_groupButtonWidth, m_controlsHeight),
			Time.timeScale, m_timeScaleRange.x, m_timeScaleRange.y);

		currentVerticalOffset += m_controlsHeight + m_controlsSpacing.y;

		labelContent = new GUIContent(CurrentGroupName);
		labelSize = GUI.skin.label.CalcSize(labelContent);
		GUI.Label(new Rect(m_controlsOffset.x, currentVerticalOffset, labelSize.x, labelSize.y), labelContent);

		currentVerticalOffset += labelSize.y + m_controlsSpacing.y;

		if (GUI.Button(new Rect(m_controlsOffset.x, currentVerticalOffset,
			m_navigationButtonWidth, m_controlsHeight), "<"))
		{
			ConditionalSwitchToPrevItem();
		}

		if (GUI.Button(new Rect(m_controlsOffset.x + m_navigationButtonWidth + m_controlsSpacing.x, currentVerticalOffset,
			m_navigationButtonWidth, m_controlsHeight), ">"))
		{
			ConditionalSwitchToNextItem();
		}

		for (int i = 0; i < m_groups.Length; ++i)
		{
			currentVerticalOffset += m_controlsHeight + m_controlsSpacing.y;

			if (GUI.Button(new Rect(m_controlsOffset.x, currentVerticalOffset, m_groupButtonWidth, m_controlsHeight), m_groups[i].Name))
			{
				ChangeCurrentGroup(i);
			}
		}
	}
	#endregion //Unity core events.

	#region Class functions.
	private void ChangeCurrentGroup(int index)
	{
		SetActiveCurrentItem(false);
		m_currentGroupIndex = Mathf.Clamp(index, 0, m_groups != null ? Mathf.Max(m_groups.Length - 1, 0) : 0);
		m_currentItemIndex = 0;
		SetActiveCurrentItem(true);
	}

	private void ConditionalSwitchToNextItem()
	{
		if (m_navigationButtonMode == NavigationButtonMode.ChangeGroup)
		{
			ChangeCurrentGroup(m_currentGroupIndex + 1);
		}
		else
		{
			SwitchToNextItem();
		}
	}

	private void ConditionalSwitchToPrevItem()
	{
		if (m_navigationButtonMode == NavigationButtonMode.ChangeGroup)
		{
			ChangeCurrentGroup(m_currentGroupIndex - 1);
		}
		else
		{
			SwitchToPrevItem();
		}
	}

	private void SwitchToNextItem()
	{
		SetActiveCurrentItem(false);
		m_currentItemIndex = Mathf.Min(m_currentItemIndex + 1, Mathf.Max(CurrentGroupItemsCount, 1) - 1);
		SetActiveCurrentItem(true);
	}

	private void SwitchToPrevItem()
	{
		SetActiveCurrentItem(false);
		m_currentItemIndex = Mathf.Max(m_currentItemIndex - 1, 0);
		SetActiveCurrentItem(true);
	}

	private void SetActiveCurrentItem(bool state)
	{
		GameObject currentItem = CurrentItem;
		if (currentItem != null)
		{
			currentItem.SetActive(state);
		}
	}
	#endregion //Class functions.
}

using System;
using System.Collections.Generic;
using Devdog.General;
using Devdog.General.ThirdParty.UniLinq;
using Devdog.General.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Devdog.QuestSystemPro.UI
{
    [RequireComponent(typeof(UIWindow))]
    public class DetailedQuestWindowUI : MonoBehaviour
    {
        [Header("UI References")]
        [Required]
        public QuestWindowUIBase questDetailsWindow;
        public RectTransform questButtonContainer;
        public UIWindowPage noQuestSelected;

        public QuestSidebarUI questSidebarUI;


        [Header("Prefabs")]
        public QuestButtonUI questButtonUIPrefab;


        protected Dictionary<Quest, QuestButtonUI> uiCache = new Dictionary<Quest, QuestButtonUI>();
        protected Quest selectedQuest;
        protected INavigationHandler navigationHandler;
        protected UIWindow window;

        protected virtual void Awake()
        {
            navigationHandler = GetComponent<INavigationHandler>();
            if (navigationHandler == null)
            {
                navigationHandler = gameObject.AddComponent<NavigationHandler>();
            }

            window = GetComponent<UIWindow>();
        }

        protected virtual void Start()
        {
            QuestManager.instance.OnQuestStatusChanged += OnQuestStatusChanged;
            window.OnShow += OnShowWindow;
            window.OnHide += OnHideWindow;

            // Initial update.
            foreach (var quest in QuestManager.instance.GetQuestStates().activeQuests)
            {
                OnQuestStatusChanged(QuestStatus.InActive, quest);
            }
        }

        protected virtual void OnShowWindow()
        {
            SetNavigation();
        }

        protected virtual void OnHideWindow()
        { }

        protected virtual void OnQuestStatusChanged(QuestStatus before, Quest quest)
        {
            switch (quest.status)
            {
                case QuestStatus.InActive:
                case QuestStatus.Completed:
                case QuestStatus.Cancelled:

                    if (selectedQuest == quest)
                    {
                        if(noQuestSelected != null)
                        {
                            noQuestSelected.Show();
                        }
                    }

                    if (uiCache.ContainsKey(quest))
                    {
                        var a = uiCache[quest];
                        uiCache.Remove(quest);
                        Destroy(a.gameObject);
                    }

                    break;
                case QuestStatus.Active:

                    SaveQuestToggledState(quest, PlayerPrefs.HasKey(QuestUtility.GetQuestCheckedSaveKey(quest)));
                    questDetailsWindow.window.Show();
                    Repaint(quest);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SetNavigation();
        }

        protected virtual void SetNavigation()
        {
            navigationHandler.HandleNavigation(uiCache.Select(o => o.Value.button).Cast<Selectable>().ToArray());
        }

        public virtual void Repaint(Quest quest)
        {
            if (uiCache.ContainsKey(quest) == false)
            {
                uiCache[quest] = CreateUIButtonForQuest(quest);
            }

            uiCache[quest].Repaint(quest);
        }

        protected virtual QuestButtonUI CreateUIButtonForQuest(Quest quest)
        {
            var uiInst = Instantiate<QuestButtonUI>(questButtonUIPrefab);
            uiInst.transform.SetParent(questButtonContainer);
            UIUtility.ResetTransform(uiInst.transform);

            var questTemp = quest;
            uiInst.button.onClick.AddListener(() =>
            {
                OnQuestButtonClicked(questTemp);
            });

            if (uiInst.toggle != null)
            {
                uiInst.toggle.onValueChanged.AddListener((isOn) =>
                {
                    OnQuestToggleValueChanged(questTemp, isOn);
                });
            }

            return uiInst;
        }

        protected virtual void OnQuestButtonClicked(Quest quest)
        {
            selectedQuest = quest;
            questDetailsWindow.Repaint(quest); // Repaint the window's details.
        }

        protected virtual void OnQuestToggleValueChanged(Quest quest, bool isOn)
        {
            SaveQuestToggledState(quest, isOn);
        }

        private void SaveQuestToggledState(Quest quest, bool isOn)
        {
            var playerPrefsKey = QuestUtility.GetQuestCheckedSaveKey(quest);
            if (isOn)
            {
                PlayerPrefs.SetInt(playerPrefsKey, 1);
                if (questSidebarUI != null && questSidebarUI.ContainsQuest(quest) == false)
                {
                    questSidebarUI.AddQuest(quest);
                }
            }
            else
            {
                if (PlayerPrefs.HasKey(playerPrefsKey))
                {
                    PlayerPrefs.DeleteKey(playerPrefsKey);
                    questSidebarUI.RemoveQuest(quest);
                }
            }
        }
    }
}
﻿using System;
using Devdog.General.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Devdog.QuestSystemPro.Dialogue.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [Header("Settings")]
        public bool hideDialogueOwnerIconOnPlayerNode = true;

        [Header("Audio & Visuals")]
        public Image dialogueSpeakerImage;
        public Text dialogueOwnerName;

        [Header("Misc")]
        public RectTransform nodeUIContainer;

        public Dialogue currentDialogue
        {
            get { return DialogueManager.instance.currentDialogue; }
        }

        public Devdog.General.UI.UIWindow window { get; protected set; }
        protected NodeUIBase currentNodeUI;


        protected virtual void Awake()
        {
            window = GetComponent<Devdog.General.UI.UIWindow>();
        }

        protected virtual void Start()
        {
            window.OnHide += WindowOnHide;
            DialogueManager.instance.OnCurrentDialogueStatusChanged += OnCurrentDialogueStatusChanged;
            DialogueManager.instance.OnCurrentDialogueNodeChanged += OnCurrentDialogueNodeChanged;
        }

        protected virtual void OnDestroy()
        {
            window.OnHide -= WindowOnHide;

            if(DialogueManager.instance != null)
            {
                DialogueManager.instance.OnCurrentDialogueStatusChanged -= OnCurrentDialogueStatusChanged;
                DialogueManager.instance.OnCurrentDialogueNodeChanged -= OnCurrentDialogueNodeChanged;
            }
        }

        protected virtual void WindowOnHide()
        {
            if(currentDialogue != null)
            {
                currentDialogue.Stop();
            }
        }

        protected virtual void OnCurrentDialogueNodeChanged(NodeBase before, NodeBase after)
        {
            if (currentNodeUI != null)
            {
                Destroy(currentNodeUI.gameObject);
            }

            if (after.uiPrefab == null)
            {
                return;
            }

            if (after.ownerType != DialogueOwnerType.DialogueOwner)
            {
                SetDialogueSpeakerIcon(hideDialogueOwnerIconOnPlayerNode ? null : QuestManager.instance.settingsDatabase.playerDialogueIcon);
            }
            else
            {
                var owner = DialogueManager.instance.currentDialogueOwner;
                if (owner != null)
                {
                    SetDialogueSpeakerIcon(owner.ownerIcon);
                    if (dialogueOwnerName != null)
                    {
                        dialogueOwnerName.text = owner.ownerName;
                    }
                }
            }

            currentNodeUI = UnityEngine.Object.Instantiate<NodeUIBase>(after.uiPrefab);
            currentNodeUI.transform.SetParent(nodeUIContainer);
            Devdog.General.UI.UIUtility.ResetTransform(currentNodeUI.transform);
            Devdog.General.UI.UIUtility.InheritParentSize(currentNodeUI.transform);

            currentNodeUI.Repaint(after);
        }

        protected void SetDialogueSpeakerIcon(Sprite sprite)
        {
            if (dialogueSpeakerImage != null)
            {
                if (sprite == null)
                {
                    dialogueSpeakerImage.gameObject.SetActive(false);
                }
                else
                {
                    dialogueSpeakerImage.gameObject.SetActive(true);
                    dialogueSpeakerImage.sprite = sprite;
                }
            }
        }

        protected virtual void OnCurrentDialogueStatusChanged(DialogueStatus before, DialogueStatus after, Dialogue self, IDialogueOwner owner)
        {
            switch (after)
            {
                case DialogueStatus.InActive:
                    window.Hide();
                    break;
                case DialogueStatus.Active:
                    window.Show();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("after", after, null);
            }
        }
    }
}

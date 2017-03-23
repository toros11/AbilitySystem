using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EntitySystem;

public class QuestPage_DetailView : DetailView<Quest> {
    public QuestPage_DetailView() : base() {
        sections.Add(new QuestPage_NameSection(20f));
        sections.Add(new QuestPage_GeneralSection(10f));
        sections.Add(new QuestPage_ComponentSection(10f));
    }
}
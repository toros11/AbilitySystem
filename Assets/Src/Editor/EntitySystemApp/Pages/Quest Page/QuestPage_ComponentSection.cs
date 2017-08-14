using EntitySystem;

public class QuestPage_ComponentSection : ListSection<Quest> {

    public QuestPage_ComponentSection(float spacing) : base(spacing) { }

    protected override string FoldOutLabel {
        get { return "Componets"; }
    }

    protected override string ListRootName {
        get { return "components"; }
    }

    protected override SearchBox CreateSearchBox() {
        return new SearchBox(null, typeof(QuestComponent), AddListItem, "Add Component", "Components");
    }

}

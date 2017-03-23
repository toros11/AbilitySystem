using EntitySystem;

public class QuestPage : MasterDetailPage<Quest> {
    public QuestPage() : base() {
        detailView = new QuestPage_DetailView();
    }
}
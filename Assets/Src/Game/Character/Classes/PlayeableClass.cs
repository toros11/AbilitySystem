[System.Serializable]
public abstract class PlayableClass {
    public int level;
    public abstract GameClass GameClass { get; }
    public abstract LevelType LevelType { get; }
}

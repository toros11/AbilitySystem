using EntitySystem;
using System.Collections.Generic;
[System.Serializable]
public abstract class PlayableClass {
    public int level;
    public AbilityCreator[] abilities;
    public abstract GameClass GameClass { get; }
    public abstract LevelType LevelType { get; }
}

using EntitySystem;

public class Warlock : PlayableClass {
    public override GameClass GameClass { get { return GameClass.Warlock; } }
    public override LevelType LevelType { get { return LevelType.Caster; } }
}

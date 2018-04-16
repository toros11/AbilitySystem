using EntitySystem;

public class Cleric : PlayableClass {
    public override GameClass GameClass { get { return GameClass.Cleric; } }
    public override LevelType LevelType { get { return LevelType.Caster; } }
}

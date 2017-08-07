using EntitySystem;

public class Warrior : PlayableClass {
    public override GameClass GameClass { get { return GameClass.Warrior; } }
    public override LevelType LevelType { get { return LevelType.Melee; } }
}

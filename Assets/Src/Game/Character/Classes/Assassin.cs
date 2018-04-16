using EntitySystem;

public class Assassin : PlayableClass {
    public override GameClass GameClass { get { return GameClass.Assassin; } }
    public override LevelType LevelType { get { return LevelType.Melee; } }
}

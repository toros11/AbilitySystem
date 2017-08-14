using EntitySystem;

public class Ranger : PlayableClass {
    public override GameClass GameClass { get { return GameClass.Ranger; } }
    public override LevelType LevelType { get { return LevelType.Melee; } }
}

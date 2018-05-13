[System.Flags]
public enum ItemType {
    Unique = 0x1,
    Consumeable = 0x2,
    QuestItem = 0x4,
    Socketable = 0x8,
}

public enum WeaponCategory {
    OneHand, TwoHand, Ranged
}

public enum WeaponType {
    Sword, Knife, Club, Polearm
}

public enum ArmorType {
    None, Leather, Cloth, Plate, Chain
}

public enum DamageType {
    Blunt, Piercing, Slashing
}

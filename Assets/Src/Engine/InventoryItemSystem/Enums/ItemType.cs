[System.Flags]
public enum ItemType {
    Unique = 0x1,
    Consumeable = 0x2,
    QuestItem = 0x4,
    Socketable = 0x8,
}

[System.Flags]
public enum WeaponCategory {
    OneHand = 0x1,
    TwoHand = 0x2,
    Ranged = 0x4,
}

public enum WeaponType {
    Sword, Knife, Club, Polearm
}

public enum ArmorType {
    None, Leather, Cloth, Plate, Chain
}

[System.Flags]
public enum DamageType {
    Blunt = 0x1,
    Piercing = 0x2,
    Slashing = 0x4,
}

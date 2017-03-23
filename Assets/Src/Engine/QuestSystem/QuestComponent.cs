using System;
using UnityEngine;
using Intelligence;

namespace EntitySystem{
    public abstract class QuestComponent{
        [NonSerialized] public Quest quest;
        [NonSerialized] public Context context;
    }    
}
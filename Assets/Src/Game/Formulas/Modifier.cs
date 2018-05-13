using System;
using System.Collections.Generic;
using UnityEngine;
using EntitySystem;

namespace Intelligence {
    public abstract class Modifier {

        [NonSerialized]
        protected Context contextType;

        [NonSerialized]
        protected DiceCreator diceCreator;

        [NonSerialized]
        public BaseParameters user;

        [NonSerialized]
        public BaseParameters target;

        public abstract void ApplyModifier<T>(T t, ref float inValue);
        public virtual void SetContext(Context context) {
            this.contextType = context;
        }

        public virtual Type GetContextType() {
            return typeof(Context);
        }

        // debug function
        public virtual void SetParameters(BaseParameters p) {
            this.user =p;
        }

        public virtual void SetTargetParams(BaseParameters p) {
            this.target = p;
        }
    }

    public abstract class Modifier<T> : Modifier where T : Context {
        protected new T context;

        public override void SetContext(Context context) {
            this.context = context as T;
        }

        public override Type GetContextType() {
            return typeof(T);
        }
    }

    public abstract class EntityModifier<T,V> : Modifier<T> where T : Context where V : EntitySystemBase {
        protected V ContextEntity { get { return this.context.entity as V; } }
    }

    public abstract class AbilityModifier<T> : EntityModifier<T, Ability> where T : Context {
        // protected Ability Entity { get ; private set; }


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WeaponType
{
    Melee, Range
}

public enum WeaponState
{
    Attack, Reload, Serenity
}

public abstract class AWeapon : MonoBehaviour
{
    [SerializeField] private List<EventOnAttackState> events = new List<EventOnAttackState>();
    [SerializeField] private List<UnitDamage> damages = new List<UnitDamage>();
    [SerializeField] private WeaponSettings weaponSettings;


    protected WeaponState state = WeaponState.Serenity;

    #region PROPERTIES
    public WeaponSettings WeaponSettings => weaponSettings;

    public List<UnitDamage> Damages => damages;

    public abstract WeaponType WeaponType { get; }
    

    public WeaponState State
    {
        get
        {
            return state;
        }

        protected set
        {
            WeaponStateEvent?.Invoke(value);
            WeaponStateEventWithhWeapon?.Invoke(value, this);
            state = value;
        }

    }

    #endregion


    public delegate void WeaponStateEventHelper(WeaponState state);
    public event WeaponStateEventHelper WeaponStateEvent;

    public event Action<WeaponState, AWeapon> WeaponStateEventWithhWeapon;



    protected virtual void Awake()
    {
        WeaponStateEvent += WeaponStateListener;
    }

    protected virtual void OnDestroy()
    {
        WeaponStateEvent -= WeaponStateListener;
    }

    private void WeaponStateListener(WeaponState state)
    {
        StartCoroutine(EventStarter(state));
    }


    public IEnumerator EventStarter(WeaponState weaponState)
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].WeaponState == weaponState)
            {
                StartCoroutine(events[i].Invoke());
                yield return new WaitForSeconds(events[i].MinTime);
            }
        }
    }


    #region Переопределяемые методы


    protected virtual IEnumerator Attacking(float time)
    {
        State = WeaponState.Attack;
        yield return new WaitForSeconds(time);
    }


    protected virtual IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        yield return new WaitForSeconds(time);
    }

    protected virtual IEnumerator Serenity(float time)
    {
        State = WeaponState.Serenity;
        yield return new WaitForSeconds(time);
    }

    public abstract void Attack();

    #endregion

    #region Вспомогательный класс

    [System.Serializable]
    public class EventOnAttackState
    {
        [SerializeField] private WeaponState weaponState;
        [SerializeField] private float toEventStart;
        [SerializeField] private UnityEvent weaponEvent;

        private EventOnAttackState nextState;

        public EventOnAttackState NextState
        {
            get
            {
                return nextState;
            }

            set
            {
                nextState = value;
            }

        }

        public UnityEvent WeaponEvent
        {
            get
            {
                return weaponEvent;
            }
        }



        public WeaponState WeaponState
        {
            get
            {
                return weaponState;
            }
        }


        public float MinTime
        {
            get
            {

                return toEventStart;

            }
        }

        public delegate void WeaponEventStateHelper(EventOnAttackState weaponEvent);
        public event WeaponEventStateHelper EventState;

        public delegate void WeaponEventEndHelper();
        public event WeaponEventEndHelper EventEnd;

        public IEnumerator Invoke()
        {
            EventState?.Invoke(this);
            yield return new WaitForSeconds(toEventStart);
            weaponEvent?.Invoke();
            EventEnd?.Invoke();
        }
    }

    #endregion
}

[Serializable]
public class UnitDamage
{
    public UnitType Type;
    public float DamageToType;
}

[Serializable]
public class WeaponSettings
{
    public float TimeToAttack;
    public float DamageTime;
    public float TimeToReload;
    public float SerenityTime;
}

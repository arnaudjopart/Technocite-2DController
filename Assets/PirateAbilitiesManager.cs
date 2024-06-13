using com.ajc.turnbase;

public class PirateAbilitiesManager : CharacterAbilitiesManagerBase
{
    // Start is called before the first frame update
    void Start()
    {
        abilities.Add(new AttackAbility());
        abilities.Add(new HealAbility());
        abilities.Add(new BigAttackAbility());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class AttackAbility : Ability
{
    public AttackAbility() 
    { 
        Name = "Simple attack";
    }

    public override void Apply(Character _from, Character _to)
    {
        _from.Attack(_to);
    }
}

public class HealAbility : Ability
{
    public HealAbility()
    {
        Name = "Simple Heal";
    }

    public override void Apply(Character _from, Character _to)
    {
        _from.Heal(_to);
    }
}

public class HealFriendAbility : Ability
{
    public HealFriendAbility()
    {
        
    }

    public override void Apply(Character _from, Character _to)
    {
        throw new System.NotImplementedException();
    }
}

public class BigAttackAbility : Ability
{
    public BigAttackAbility()
    {
        Name = "BigAttack";
    }

    public override void Apply(Character _from, Character _to)
    {
        var bigAttackPoint = _from.GetDamage()*2;
        _from.Attack(_to, bigAttackPoint); 
    }
}
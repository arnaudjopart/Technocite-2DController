using com.ajc.turnbase;

public abstract class Ability 
{
    public string Name { get; set; }
    public string m_description;

    public abstract void Apply(Character _from, Character _to);
    
}
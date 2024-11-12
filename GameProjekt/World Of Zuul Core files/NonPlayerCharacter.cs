public class NPC
{
    public string name { get; set; }

    public NPC(string name)
    {
        this.name = name;
    }

    public virtual void Interact()
    {
    }

    public class secretary : NPC
    {
        public secretary(string name) : base(name)
        {
        }
        public override void Interact()
        {
        }

    }
    public class expert : NPC
    {
        public expert(string name) : base(name)
        {
        }
        public override void Interact()
        {
        }
    }

}

using static GameAssets;

public class NPC
{
    public string name { get; set; }

    public NPC(string name)
    {
        this.name = name;
    }

    public class Secretary : NPC
    {
        public Secretary(string name) : base(name) 
        { 
        }    

        public int getStatus(int money, int CO2, int energy, Context context) // Added context as a parameter
        {
            Console.WriteLine("Vil du have en status eller information? Status = S Info = I");
            string prompt = Console.ReadLine();
            int level = 0; // placeholder

            if (prompt == "S")
            {
                Console.WriteLine($"Økonomi: {money}");
                Console.WriteLine($"CO2 niveau: {CO2}");
                Console.WriteLine($"Energiforbrug: {energy}");
            }
            else if (prompt == "I")
            {
                Console.WriteLine("Vil du have mere information fra ekspert eller tilbage til rummet? Svar Info = I Tilbage = T");
                prompt = Console.ReadLine();
                if (prompt == "I")
                {
                    DisplayMoreInfo(context);
                }
                else if (prompt == "T")
                {
                    CommandGoBack(); 
                }
            }

            return level;
        }

        private void DisplayMoreInfo(Context context)
        {
            // not sure if this is needed
           
        }

        private void CommandGoBack()
        {
            // not sure if this is needed 
           
        }
    }

    public class Expert : NPC
    {
        private string current;

        public Expert(string name) : base(name) 
        {
        }

        public void DisplayMandatoryIntro(Context context)
        {
            current = context.GetCurrentName().ToString();

            switch (current)
            {
                case "Atomkraftværk":
                    Anim.CharSplit(db.GetSection("Atom ekspert"));
                    break;
                case "Vandanlæg":
                    Anim.CharSplit(db.GetSection("Vand ekspert"));
                    break;
                case "Vindanlæg":
                    Anim.CharSplit(db.GetSection("Vind ekspert"));
                    break;
                case "Solanlæg":
                    Anim.CharSplit(db.GetSection("Sol ekspert"));
                    break;
                default:
                    break;
            }

            Console.WriteLine("Vil du foretage et køb? Svar ja = J nej = N");
            string prompt = Console.ReadLine();
            if (prompt == "J")
            {
                Buy();
            }
            else if (prompt == "N")
            {
                Console.WriteLine("Vil du have mere information fra ekspert eller tilbage til rummet? Svar Info = I Tilbage = T");
                prompt = Console.ReadLine();
                if (prompt == "I")
                {
                    DisplayMoreInfo(context);
                }
                else if (prompt == "T")
                {
                    CommandGoBack();
                }
            }
        }

        public void DisplayMoreInfo(Context context)
        {
            current = context.GetCurrentName().ToString();

            switch (current)
            {
                case "Atomkraftværk":
                    Anim.CharSplit(db.GetSection("Atom ekspert"));
                    break;
                case "Vandanlæg":
                    Anim.CharSplit(db.GetSection("Vand ekspert"));
                    break;
                case "Vindanlæg":
                    Anim.CharSplit(db.GetSection("Vind ekspert"));
                    break;
                case "Solanlæg":
                    Anim.CharSplit(db.GetSection("Sol ekspert"));
                    break;
                default:
                    break;
            }
        }

        public void Buy()
        {
            Console.WriteLine("Vil du købe? ");
            string prompt = Console.ReadLine();
            SignContract();
        }

        private void CommandGoBack()
        {
            // not sure if this is needed 
           
        }
    }
}
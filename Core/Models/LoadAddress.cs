namespace Core.Models
{
    public class LoadAddress : Instruction
    {
        public Register R1 { get; }
        
        public string Label { get; }
        
        public static string Name { get; } = "la";
        
        public LoadAddress(Register r1, string label)
        {
            R1 = r1;
            Label = label;
        }

        public override Context Pipeline(Context context)
        {
            return context;
        }
    }
}
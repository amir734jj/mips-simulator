namespace Core.Models
{
    public class LoadImmediate : Instruction
    {
        public int Value { get; }

        public Register R1 { get; }
        
        public static string Name { get; } = "li";
        
        public LoadImmediate(Register r1, int value)
        {
            R1 = r1;
            Value = value;
        }

        public override Context Pipeline(Context context)
        {
            return new Context(
                context.Instructions,
                context.Registers.SetItem(R1, Value),
                context.Memory
            );
        }
    }
}
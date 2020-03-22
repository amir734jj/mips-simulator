using System.Collections.Immutable;

namespace Core.Models
{
    public class Move : Instruction
    {
        public Register R2 { get; }

        public Register R1 { get; }
        
        public static string Name { get; } = "move";
        
        public Move(Register r1, Register r2)
        {
            R1 = r1;
            R2 = r2;
        }

        public override Context Pipeline(Context context)
        {
            return new Context(
                context.Instructions,
                context.Registers.SetItem(R1, context.Registers.GetValueOrDefault(R2)),
                context.Memory
            );
        }
    }
}
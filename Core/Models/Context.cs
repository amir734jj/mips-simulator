using System;
using System.Collections.Immutable;

namespace Core.Models
{
    public class Context
    {
        public ImmutableDictionary<string, int> LabelAddress { get; }
        
        public ImmutableStack<Instruction> Instructions { get; }
        
        public ImmutableDictionary<Register, int> Registers { get; }
        
        public ImmutableArray<byte> Memory { get; }

        private Context(ImmutableStack<Instruction> instructions, ImmutableDictionary<Register, int> registers, ImmutableArray<byte> memory)
        {
            Instructions = instructions;
            Registers = registers;
            Memory = memory;
        }

        public Context MutateRegisters(
            Func<ImmutableDictionary<Register, int>, ImmutableDictionary<Register, int>> action)
        {
            return new Context(Instructions, action(Registers), Memory);
        }

        public Context MutateMemory(Func<ImmutableArray<byte>, ImmutableArray<byte>> action)
        {
            return new Context(Instructions, Registers, action(Memory));
        }
    }
}
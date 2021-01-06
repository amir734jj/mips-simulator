using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Models;
using Core.Tokens;
using EnumsNET;

namespace Core.Logic
{
    public class MipsRuntime
    {
        private readonly IReadOnlyList<IInstruction> _instructions;

        public MipsRuntime(IReadOnlyList<IInstruction> instructions)
        {
            _instructions = instructions;
        }

        public void Process()
        {
            /*var lookup = new Dictionary<string, Label>();
            var memory = new Dictionary<Register, object>();

            for (var index = 0; index < _instructions.Count; index++)
            {
                var instruction = _instructions[index];
                
                switch (instruction)
                {
                    case Label label:
                        lookup[label.Value] = label;
                        break;
                    case List<>
                        
                }
            }*/
        }
    }
}
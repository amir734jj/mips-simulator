using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Interfaces;
using Core.Models;
using Core.Tokens;
using EnumsNET;

namespace Core.Logic
{
    public class MipsRuntime
    {
        private readonly ImmutableList<IInstruction> _instructions;

        public MipsRuntime(IEnumerable<IInstruction> instructions)
        {
            _instructions = instructions.ToImmutableList();
        }

        public void Process()
        {
            var lookup = new Dictionary<string, Label>();
            var memory = Enums.GetValues<Register>().ToDictionary(x => x, _ => 0);

            foreach (var instruction in _instructions)
            {
                switch (instruction)
                {
                    case Label label:
                        lookup[label.Value] = label;
                        break;
                    case Add add:
                        memory[add.R1] = memory[add.R2] + memory[add.R3];
                        break;
                    case AddImmediate addImmediate:
                        memory[addImmediate.R1] = memory[addImmediate.R2] + addImmediate.Value;
                        break;
                    case LoadImmediate loadImmediate:
                        memory[loadImmediate.R1] = loadImmediate.Value;
                        break;
                    case LoadAddress loadAddress:
                        memory[loadAddress.R1] = _instructions.IndexOf(lookup[loadAddress.Label]);
                        break;
                    case Move move:
                        memory[move.R1] = memory[move.R2];
                        break;
                    case SystemCall systemCall:
                        var systemCallNumber = memory[Register.V0];
                        switch (systemCallNumber)
                        {
                            case 1:
                                Console.Write(memory[Register.A0]);
                                break;
                            case 4:
                                var directive = _instructions[memory[Register.A0] + 1];
                                var item = _instructions[memory[Register.A0] + 2];

                                switch (item)
                                {
                                    case StringPrimitive stringPrimitive:
                                        switch (directive)
                                        {
                                            case AsciiDirective _:
                                                Console.Write(stringPrimitive.Value);
                                                break;
                                            case AsciizDirective _:
                                                Console.Write(stringPrimitive.Value);
                                                break;
                                        }
                                        break;
                                }

                                break;
                            case 5:
                                memory[Register.V0] = int.Parse(Console.ReadLine() ?? "0");
                                break;
                            case 10:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine($"System call {systemCall} is not supported");
                                break;
                        }

                        break;
                    case Comment _:
                    case Semicolon _:
                        break;
                }
            }
        }
    }
}
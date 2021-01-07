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
            var labelLookUp = new Dictionary<string, Label>();
            var registers = Enums.GetValues<Register>().ToDictionary(x => x, _ => 0);
            var memory = new int[100];
            registers[Register.Sp] = memory.Length;

            var mainIndex = 0;
            int index;
            for ( index = 0; index < _instructions.Count; index++)
            {
                var instruction = _instructions[index];
                switch (instruction)
                {
                    case Label label:
                        labelLookUp[label.Value] = label;
                        if (label.Value == "main")
                        {
                            mainIndex = index;
                        }
                        break;
                }
            }

            for ( index = mainIndex; index < _instructions.Count; index++)
            {
                var instruction = _instructions[index];
                switch (instruction)
                {
                    case Add add:
                        registers[add.R1] = registers[add.R2] + registers[add.R3];
                        break;
                    case Sub sub:
                        registers[sub.R1] = registers[sub.R2] - registers[sub.R3];
                        break;
                    case AddImmediate addImmediate:
                        registers[addImmediate.R1] = registers[addImmediate.R2] + addImmediate.Value;
                        break;
                    case LoadImmediate loadImmediate:
                        registers[loadImmediate.R1] = loadImmediate.Value;
                        break;
                    case LoadAddress loadAddress:
                        registers[loadAddress.R1] = _instructions.IndexOf(labelLookUp[loadAddress.Label]);
                        break;
                    case LoadWord loadWord:
                        var loadWordAddress = loadWord.Offset + registers[loadWord.R2];
                        var loadWordAdjustment = (memory.Length - loadWordAddress) / 4;
                        registers[loadWord.R1] = memory[^loadWordAdjustment];
                        break;
                    case StoreWord storeWord:
                        var storeWordAddress = storeWord.Offset + registers[storeWord.R2];
                        var storeWordAdjustment = (memory.Length - storeWordAddress) / 4;
                        registers[storeWord.R1] = memory[^storeWordAdjustment];
                        break;
                    case Move move:
                        registers[move.R1] = registers[move.R2];
                        break;
                    case JumpUnconditional jumpUnconditional:
                        index = _instructions.IndexOf(labelLookUp[jumpUnconditional.Value]);
                        break;
                    case JumpRegister jumpRegister:
                        index = registers[jumpRegister.R1];
                        break;
                    case JumpAndLink jumpAndLink:
                        registers[Register.Ra] = index + 1;
                        index = _instructions.IndexOf(labelLookUp[jumpAndLink.Value]);
                        break;
                    case BranchUnconditional branchUnconditional:
                        index = _instructions.IndexOf(labelLookUp[branchUnconditional.Value]);
                        break;
                    case BranchEquals branchEquals:
                        if (registers[branchEquals.R1] == registers[branchEquals.R2])
                        {
                            index = _instructions.IndexOf(labelLookUp[branchEquals.Value]);
                        }
                        break;
                    case BranchNotEquals branchNotEquals:
                        if (registers[branchNotEquals.R1] != registers[branchNotEquals.R2])
                        {
                            index = _instructions.IndexOf(labelLookUp[branchNotEquals.Value]);
                        }
                        break;
                    case BranchEqualsZero branchEqualsZero:
                        if (registers[branchEqualsZero.R1] == 0)
                        {
                            index = _instructions.IndexOf(labelLookUp[branchEqualsZero.Value]);
                        }
                        break;
                    case BranchNotEqualsZero branchNotEqualsZero:
                        if (registers[branchNotEqualsZero.R1] != 0)
                        {
                            index = _instructions.IndexOf(labelLookUp[branchNotEqualsZero.Value]);
                        }
                        break;
                    case BranchLessThan branchLessThan:
                        if (registers[branchLessThan.R1] < registers[branchLessThan.R2])
                        {
                            index = _instructions.IndexOf(labelLookUp[branchLessThan.Value]);
                        }
                        break;
                    case BranchLessThanOrEquals branchLessThanOrEquals:
                        if (registers[branchLessThanOrEquals.R1] <= registers[branchLessThanOrEquals.R2])
                        {
                            index = _instructions.IndexOf(labelLookUp[branchLessThanOrEquals.Value]);
                        }
                        break;
                    case BranchGreaterThanZero branchGreaterThanZero:
                        if (registers[branchGreaterThanZero.R1] > 0)
                        {
                            index = _instructions.IndexOf(labelLookUp[branchGreaterThanZero.Value]);
                        }
                        break;
                    case BranchLessThanZero branchLessThanZero:
                        if (registers[branchLessThanZero.R1] < 0)
                        {
                            index = _instructions.IndexOf(labelLookUp[branchLessThanZero.Value]);
                        }
                        break;
                    case BranchGreaterThan branchGreaterThan:
                        if (registers[branchGreaterThan.R1] > registers[branchGreaterThan.R2])
                        {
                            index = _instructions.IndexOf(labelLookUp[branchGreaterThan.Value]);
                        }
                        break;
                    case BranchGreaterThanOrEquals branchGreaterThanOrEquals:
                        if (registers[branchGreaterThanOrEquals.R1] >= registers[branchGreaterThanOrEquals.R2])
                        {
                            index = _instructions.IndexOf(labelLookUp[branchGreaterThanOrEquals.Value]);
                        }
                        break;
                    case SystemCall systemCall:
                        var systemCallNumber = registers[Register.V0];
                        switch (systemCallNumber)
                        {
                            case 1:
                                Console.Write(registers[Register.A0]);
                                break;
                            case 4:
                                var directive = _instructions[registers[Register.A0] + 1];
                                var item = _instructions[registers[Register.A0] + 2];

                                switch (item)
                                {
                                    case StringPrimitive stringPrimitive:
                                        switch (directive)
                                        {
                                            case AsciiDirective _:
                                                Console.Write(stringPrimitive.Value);
                                                break;
                                            case AsciizDirective _:
                                                Console.WriteLine(stringPrimitive.Value);
                                                break;
                                        }

                                        break;
                                }
                                break;
                            case 5:
                                registers[Register.V0] = int.Parse(Console.ReadLine() ?? "0");
                                break;
                            case 10:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine($"System call {systemCall} is not supported");
                                break;
                        }
                        break;
                    case Label _:
                    case Comment _:
                    case Semicolon _:
                        break;
                    default:
                        Console.WriteLine(instruction.GetType() + " is not supported yet");
                        break;
                }
            }
        }
    }
}
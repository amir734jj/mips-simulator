using System.Linq;
using Core.Interfaces;
using Core.Models;
using Core.Tokens;
using EnumsNET;
using FParsec;
using FParsec.CSharp;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core; // extension functions (combinators & helpers)
using static FParsec.CSharp.PrimitivesCS; // combinator functions
using static FParsec.CSharp.CharParsersCS; // pre-defined parsers

namespace Core.Parser
{
    public class MipsParser
    {
        public MipsParser()
        {
            var skipCommaP = Between(WS, Optional(CharP(',')), WS);
            var stringP = ManyChars(NoneOf(new[] {' ', '\n', '\r'}));
            var quotedString = Between('"', ManyChars(NoneOf("\"")), '"');

            var registerP = CharP('$').AndR(Choice(Enums.GetValues<Register>()
                .Select(x => x.Name())
                .Select(x => StringCI(new string(x.Skip(1).ToArray()))).ToArray()));

            var addImmediateP = StringP("addi").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).AndL(skipCommaP).And(Int)
                .Map(x => (IInstruction)new AddImmediate(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2));
            
            var addP = StringP("add").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).AndL(skipCommaP).And(registerP)
                .Map(x => (IInstruction)new Add(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2.ToRegister()));
            
            var subP = StringP("sub").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).AndL(skipCommaP).And(registerP)
                .Map(x => (IInstruction)new Sub(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2.ToRegister()));

            var mulP = StringP("mul").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).AndL(skipCommaP).And(registerP)
                .Map(x => (IInstruction)new Multiply(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2.ToRegister()));
            
            var divP = StringP("div").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).AndL(skipCommaP).And(registerP)
                .Map(x => (IInstruction)new Divide(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2.ToRegister()));
            
            var loadImmediateP = StringP("li").And(WS).AndR(registerP).AndL(skipCommaP).And(Int)
                .Map(x => (IInstruction)new LoadImmediate(x.Item1.ToRegister(), x.Item2));

            var loadAddressP = StringP("la").And(WS).AndR(registerP).AndL(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new LoadAddress(x.Item1.ToRegister(), x.Item2));
            
            var moveP = StringP("move").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP)
                .Map(x =>  (IInstruction)new Move(x.Item1.ToRegister(), x.Item2.ToRegister()));

            var syscallP = StringP("syscall").Return((IInstruction)new SystemCall());

            var wordP = StringP(".word").Return((IInstruction)new Word());
            var asciiP = StringP(".ascii").Return((IInstruction)new AsciiDirective());
            var asciizP = StringP(".asciiz").Return((IInstruction)new AsciizDirective());
            var textP = StringP(".text").Return((IInstruction)new TextDirective());
            var dataP = StringP(".data").Return((IInstruction)new DataDirective());
            
            var labelP = Many1Chars(NoneOf(new[] {'"', ' ', ':', ';', '#','\r' ,'\n'})).AndLTry(CharP(':'))
                .Map(x => (IInstruction) new Label(x));
            var stringLiteralP = quotedString.Map(x => (IInstruction) new StringPrimitive(x));
            var integerLiteralP = Int.Map(x => (IInstruction) new IntegerPrimitive(x));
            var commentP = CharP('#').AndR(ManyChars(NoneOf(new[] {'\n'}))).Map(x => (IInstruction) new Comment(x));
            var semicolonP = CharP(';').Map(x => (IInstruction) new Semicolon());
            
            var branchUnconditionalP = StringP("b").And(WS).AndR(ManyChars(NoneOf(" ")))
                .Map(x => (IInstruction) new BranchUnconditional(x));

            var branchEqualsP = StringP("beq").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).And(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchEquals(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2));
            
            var branchEqualsZeroP = StringP("beqz").And(WS).AndR(registerP).AndL(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchEqualsZero(x.Item1.ToRegister(), x.Item2));
            
            var branchNotEqualsZeroP = StringP("bnez").And(WS).AndR(registerP).AndL(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchNotEqualsZero(x.Item1.ToRegister(), x.Item2));

            var branchNotEqualsP = StringP("bne").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).And(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchEquals(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2));

            var branchLessThanP = StringP("blt").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).And(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchLessThan(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2));
            
            var branchLessThanZeroP = StringP("bltz").And(WS).AndR(registerP).AndL(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchLessThanZero(x.Item1.ToRegister(), x.Item2));
            
            var branchLessThanOrEqualsP = StringP("ble").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).And(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchLessThanOrEquals(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2));
            
            var branchGreaterThanP = StringP("bgt").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).And(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchGreaterThan(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2));
            
            var branchGreaterThanZeroP = StringP("bgtz").And(WS).AndR(registerP).AndL(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchGreaterThanZero(x.Item1.ToRegister(), x.Item2));

            var branchGreaterThanOrEqualsZeroP = StringP("bgez").And(WS).AndR(registerP).AndL(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchGreaterThanOrEqualsZero(x.Item1.ToRegister(), x.Item2));
            
            var branchLessThanOrEqualsZeroP = StringP("blez").And(WS).AndR(registerP).AndL(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchLessThanOrEqualsZero(x.Item1.ToRegister(), x.Item2));

            var branchGreaterThanOrEqualsP = StringP("bge").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP).And(skipCommaP).And(stringP)
                .Map(x => (IInstruction) new BranchGreaterThanOrEquals(x.Item1.Item1.ToRegister(), x.Item1.Item2.ToRegister(), x.Item2));

            var jumpAndLinkP = StringP("jal").And(WS).AndR(ManyChars(NoneOf(" ")))
                .Map(x => (IInstruction) new JumpAndLink(x));
            
            var jumpRegisterP = StringP("jr").And(WS).AndR(registerP)
                .Map(x => (IInstruction) new JumpRegister(x.ToRegister()));

            var jumpUnconditional = StringP("j").And(WS).AndR(ManyChars(NoneOf(" ")))
                .Map(x => (IInstruction) new JumpUnconditional(x));
            
            var lwP = StringP("lw").And(WS).AndR(registerP).AndL(skipCommaP).And(Int).AndL(WS).AndL(CharP('(')).AndL(WS).And(registerP).AndL(WS).AndL(CharP(')'))
                .Map(x => (IInstruction)new LoadWord(x.Item1.Item1.ToRegister(), x.Item1.Item2, x.Item2.ToRegister()));
            
            var swP = StringP("sw").And(WS).AndR(registerP).AndL(skipCommaP).And(Int).AndL(WS).AndL(CharP('(')).AndL(WS).And(registerP).AndL(WS).AndL(CharP(')'))
                .Map(x => (IInstruction)new StoreWord(x.Item1.Item1.ToRegister(), x.Item1.Item2, x.Item2.ToRegister()));

            var atomicP = new[]
            {
                stringLiteralP, labelP, integerLiteralP, commentP, semicolonP, asciizP, asciiP, textP, wordP,
                dataP, loadImmediateP, moveP, loadAddressP, syscallP, addImmediateP, addP, subP, mulP, divP,
                branchEqualsZeroP, branchNotEqualsZeroP, branchEqualsP, branchNotEqualsP, branchLessThanZeroP,
                branchLessThanP, branchLessThanOrEqualsZeroP, branchLessThanOrEqualsP, branchGreaterThanZeroP,
                branchGreaterThanP, branchGreaterThanOrEqualsZeroP, branchGreaterThanOrEqualsP,
                branchUnconditionalP, jumpAndLinkP, jumpRegisterP, jumpUnconditional, lwP, swP
            };
            
            ProgramP = WS.AndR(Many(Choice(atomicP), WS, true));
        }

        public  FSharpFunc<CharStream<Unit>,Reply<FSharpList<IInstruction>>> ProgramP { get; }
    }
}
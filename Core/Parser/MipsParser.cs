using System;
using System.Collections.Generic;
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
            var quotedString = Between('"', ManyChars(NoneOf("\"")), '"');

            var registerP = CharP('$').AndR(Choice(Enums.GetValues<Register>()
                .Select(x => x.Name())
                .Select(x => StringCI(new string(x.Skip(1).ToArray()))).ToArray()));

            var loadImmediateP = StringP("li").And(WS).AndR(registerP).AndL(skipCommaP).And(Int)
                .Map(x => (IInstruction)new LoadImmediate(x.Item1.ToRegister(), x.Item2));

            var loadAddressP = StringP("la").And(WS).AndR(registerP).AndL(skipCommaP).And(ManyChars(NoneOf(" ")))
                .Map(x => (IInstruction) new LoadAddress(x.Item1.ToRegister(), x.Item2));
            
            var moveP = StringP("move").And(WS).AndR(registerP).AndL(skipCommaP).And(registerP)
                .Map(x =>  (IInstruction)new Move(x.Item1.ToRegister(), x.Item2.ToRegister()));

            var syscallP = StringP("syscall").Return((IInstruction)new SystemCall());

            var asciiP = StringP(".ascii").AndRTry(NotFollowedBy(CharP('z'))).Return((IInstruction)new AsciiDirective());
            var asciizP = StringP(".asciiz").Return((IInstruction)new AsciizDirective());
            var textP = StringP(".text").Return((IInstruction)new TextDirective());
            var codep = StringP(".code").Return((IInstruction)new CodeDirective());
            
            var labelP = ManyChars(NoneOf(new[] {' ', ':', ';', '#', '\n'})).AndLTry(CharP(':'))
                .Map(x => (IInstruction) new Label(x));
            var stringP = quotedString.Map(x => (IInstruction) new StringPrimitive(x));
            var integerP = Int.Map(x => (IInstruction) new IntegerPrimitive(x));
            var commentP = CharP('#').AndR(Many1Chars(NoneOf(new[] {'\n'}))).Map(x => (IInstruction) new Comment(x));
            var semicolonP = CharP(';').Map(x => (IInstruction) new Semicolon());

            var atomicP = new[]
            {
                labelP, stringP, integerP, commentP, semicolonP, asciiP, asciizP, textP,
                codep, loadImmediateP, moveP, loadAddressP, syscallP
            };
            
            ProgramP = WS.AndR(Many(Choice(atomicP), WS, true));
        }

        public  FSharpFunc<CharStream<Unit>,Reply<FSharpList<IInstruction>>> ProgramP { get; }
    }
}
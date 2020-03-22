using System.Collections.Generic;
using System.Linq;
using Core.Models;
using EnumsNET;
using Pidgin;
using static Pidgin.Parser;
using String = System.String;

namespace Core.Parser
{
    public class MipsParser
    {
        public MipsParser()
        {
            var skipCommaP = Char(',').IgnoreResult();

            var registerP = Char('$')
                .Then(Enums.GetValues<Register>().Select(x => x.Name())
                    .Select(x => String(new string(x.Skip(1).ToArray())))
                    .Aggregate((a, b) => a.Or(b))
                    .Map(v => $"${v}".ToRegister()));

            var loadImmediateP = Map(
                (a, b, c) => (Instruction) new LoadImmediate(b, c),
                String(LoadImmediate.Name).Between(SkipWhitespaces),
                registerP.Before(skipCommaP.Optional()).Between(SkipWhitespaces),
                Num.Between(SkipWhitespaces)
            );

            var moveP = Map(
                (a, b, c) => (Instruction) new Move(b, c),
                String(Move.Name).Between(SkipWhitespaces),
                registerP.Before(skipCommaP.Optional()).Between(SkipWhitespaces),
                registerP.Between(SkipWhitespaces)
            );

            var instructionsP = loadImmediateP.Or(moveP);

            ProgramP = instructionsP.SeparatedAndOptionallyTerminated(EndOfLine);
        }

        public Parser<char, IEnumerable<Instruction>> ProgramP { get; }
    }
}
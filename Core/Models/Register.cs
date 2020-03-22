using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Core.Models
{
    public enum Register
    {
        Zero,
        At,
        V0, V1,
        A0, A1, A2, A3,
        T0, T1, T2, T3, T4, T5, T6, T7,
        S0, S1, S2, S3, S4, S5, S6, S7,
        T8, T9,
        K0, K1,
        Gp,
        Sp,
        S8,
        Ra
    }

    public static class RegisterExtensions
    {
        public static string Name(this Register register)
        {
            return RegisterTable.GetValueOrDefault(register, string.Empty);
        }

        public static Register ToRegister(this string name)
        {
            return RegisterTable.First(y => y.Value == name).Key;
        }

        private static readonly ImmutableDictionary<Register, string> RegisterTable = new Dictionary<Register, string>
        {
            [Register.Zero] = "$zero",
            [Register.At] = "$at",
            [Register.V0] = "$v0",
            [Register.V1] = "$v1",
            [Register.A0] = "$a0",
            [Register.A1] = "$a1",
            [Register.A2] = "$a2",
            [Register.A3] = "$a3",
            [Register.T0] = "$t0",
            [Register.T1] = "$t1",
            [Register.T2] = "$t2",
            [Register.T3] = "$t3",
            [Register.T4] = "$t4",
            [Register.T5] = "$t5",
            [Register.T6] = "$t6",
            [Register.T7] = "$t7",
            [Register.S0] = "$s0",
            [Register.S1] = "$s1",
            [Register.S2] = "$s2",
            [Register.S3] = "$s3",
            [Register.S4] = "$s4",
            [Register.S5] = "$s5",
            [Register.S6] = "$s6",
            [Register.S7] = "$s7",
            [Register.T8] = "$t8",
            [Register.T9] = "$t9",
            [Register.K0] = "$k0",
            [Register.K1] = "$k1",
            [Register.Gp] = "$gp",
            [Register.Sp] = "$sp",
            [Register.S8] = "$s8",
            [Register.Ra] = "$ra",
        }.ToImmutableDictionary();
    }
}
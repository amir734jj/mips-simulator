# mips-simulator
MIPS simulator in C#

Features:
- Parser combinator using FParsec
- Basic instruction set including **integer** arithmetic, branch, jump
- Stack support

Specifications: 
- supported system calls: 1, 4, 5, 10
- supported directives:
  - .ascii
  - .asciiz
  - .text
  - .data
- supported instructions:
  - add
  - addi
  - sub
  - mul
  - div
  - b
  - beq
  - bne
  - blt
  - ble
  - bgt
  - bge
  - beqz
  - bneq
  - j
  - jr
  - jal
  - lw
  - sw
  - move
  - li
  - la

TODO:
- static memory support
- semantic analysis
- pretty printer

Notes:
- I included a factorial MIPS example so demonstrate the recursion support thanks to stack

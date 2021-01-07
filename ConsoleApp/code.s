    .text
str1:   .asciiz "Enter number: "
str2:   .asciiz "Result is (x + 4): "
    .code
main:
    li $t0, 3        # put 3 into register $t0
    
    li $v0, 4       ;
    la $a0, str1    ;
    syscall         ;
    
    li $v0, 5       ;
    syscall         ;
    
    move $t3, $v0   ;   # hold on to the input value
    
    li $v0, 4       ;
    la $a0, str2    ;
    syscall         ;
    
    addi $t1, $t0, 1
    move $t0, $t1
    add $t2, $t3, $t0
    
    li $v0, 1       ;
    move $a0, $t2   ;
    syscall         ;
    
    li $v0 10       ;
    syscall         ;

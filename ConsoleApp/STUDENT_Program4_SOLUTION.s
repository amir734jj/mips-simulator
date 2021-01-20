###########################################################
#       Program 4
#   Name:
#   Date:
#
#   Description:
#       Write a program to be used by the Wisconsin DNR to track the number of Deer Hunting Licenses issued
#       in a county and the state. It will display a menu with 5 choices numbered 1-5. They are (1) update
#       the count, (2) display the number of licenses already issued to a county whose number is input, (3)
#       display the county number and the number of licenses already issued to a range of counties whose
#       starting and ending county numbers are entered, (4) display the county number and the number of
#       licenses already issued for all 72 counties as well as total licenses issued in the state and the
#       last option (5) is to exit the program. In the event an invalid option is entered the program will
#       print an error message and reprint the menu. In the main you will declare a static array to hold 72
#       integers and 'total' (initialize to 0). All work will be done in subprograms and all arguments will
#       be passed on the system stack. Also, all user input should be validated with an appropriate message
#       if the input is invalid followed by a repeat of the prompting for input.
#
###########################################################
#       Register Usage
#   $t0     Holds array base address
#   $t1     Holds array length
#   $t2     Holds current total
#   $t3
#   $t4
#   $t5
#   $t6
#   $t7
#   $t8
#   $t9
###########################################################
        .data
array_counties_p:         .word 0:72      # array of counties all initialized to zero
array_size_counties_p:    .word 72        # size of county array
total_p:                  .word 0         # total number of licenses
###########################################################
        .text
main:
# load values from static variables
    la $t0, array_counties_p      # $t0 <-- base address of static array (i.e. array_counties_p)

    la $t9, array_size_counties_p # $t9 <-- base address of static variable array_size_counties_p
    lw $t1, 0($t9)                # $t1 <-- memory[$t9 + 0]

    la $t9, total_p               # $t9 <-- base address of static variable total_p
    lw $t2, 0($t9)                # $t2 <-- memory[$t9 + 0]


# calling subprogram menu
    addi $sp, $sp, -4             # allocate one word for $ra
    sw $ra, 0($sp)                # backup $ra on the stack

                                  # no need to backup any register as we are using static variables to store them

    addi $sp, $sp, -16            # allocate space for arguments In and OUT
    sw $t0, 0($sp)                # store $t0 as argument IN on the stack [$sp + 0]
    sw $t1, 4($sp)                # store $t1 as argument IN on the stack [$sp + 4]
    sw $t2, 8($sp)                # store $t2 as argument IN on the stack [$sp + 8]

    jal menu                      # calling subprogram menu

    lw $t2, 12($sp)               # load updated total from stack [$sp + 12]

    addi $sp, $sp, 16             # deallocate space for arguments IN and OUT

                                  # we did not backup any register, so there is no need to restore backups

    lw $ra, 0($sp)                # restore $ra from stack
    addi $sp, $sp, 4              # deallocate space for $ra


# updated the value in total_p static variable
    la $t9, total_p               # $t9 <-- base address of static variable total_p
    sw $t2, 0($t9)                # $t2 <-- memory[$t9 + 0]

mainEnd:
    li $v0, 10                    # halt
    syscall
###########################################################
#       menu subprogram
#
#   Subprogram description:
#       The 'menu' subprogram will receive three arguments (IN), the base address of the array and the
#       count of its elements and the number currently in total. It will have one argument (OUT) the new
#       total which you will store in total. It will repeatedly display the menu, calling each of the
#       selected options until ‘5’ is selected when it will return to the main. If option 2 is selected it
#       will ask for the number of the county to be displayed prior to calling the appropriate subprogram.
#
###########################################################
#       Arguments IN and OUT of subprogram
#   $sp+0   Holds array base address (IN)
#   $sp+4   Holds array length (IN)
#   $sp+8   Holds current total (IN)
###########################################################
#       Register Usage
#   $t0     Holds array base address
#   $t1     Holds array length
#   $t2     Holds current total
#   $t3     Holds constant value 1
#   $t4     Holds constant value 2
#   $t5     Holds constant value 3
#   $t6     Holds constant value 4
#   $t7     Holds constant value 5
#   $t8
#   $t9
###########################################################
        .data
menu_title_p:       .ascii  "-----------------------------------------\n"
menu_before_p:      .ascii  "Enter number of your choice:\n"
menu_one_p:         .ascii  "1. update licenses\n"
menu_two_p:         .ascii  "2. display a single county\n"
menu_three_p:       .ascii  "3. display a range of counties\n"
menu_four_p:        .ascii  "4. display all counties and total\n"
menu_five_p:        .ascii  "5. exit the menu\n"


menu_enter_county_p:.asciiz "Enter a county number to be displayed: "
menu_total_p:       .asciiz "Current total: "
menu_error_p:       .asciiz "Invalid entry!\n"
###########################################################
        .text
menu:
    lw $t0, 0($sp)              # load base address of array
    lw $t1, 4($sp)              # load array size
    lw $t2, 8($sp)              # load current total

menu_loop:
    li $t3, 1                   # initialize $t3 to 1
    li $t4, 2                   # initialize $t3 to 2
    li $t5, 3                   # initialize $t3 to 3
    li $t6, 4                   # initialize $t3 to 4
    li $t7, 5                   # initialize $t3 to 5

    li $v0, 4                   # print current total is:
    la $a0, menu_total_p
    syscall

    li $v0, 1                   # print current total value
    move $a0, $t2
    syscall

    li $v0, 11                  # print newline character using system call 11
    li $a0, 10
    syscall

    li $v0, 4                   # print the menu options
    la $a0, menu_title_p
    syscall

    li $v0, 5                   # read the menu entry
    syscall

    beq $v0, $t3, menu_option_1 # branch to menu_option_1 is entry is 1
    beq $v0, $t4, menu_option_2 # branch to menu_option_2 is entry is 2
    beq $v0, $t5, menu_option_3 # branch to menu_option_3 is entry is 3
    beq $v0, $t6, menu_option_4 # branch to menu_option_4 is entry is 4
    beq $v0, $t7, menu_option_5 # branch to menu_option_5 is entry is 5

    li $v0, 4                   # print error message
    la $a0, menu_error_p
    syscall

    b menu_loop                 # branch to the beginning of menu loop

# menu option 1
menu_option_1:
# calling subprogram update
    addi $sp, $sp, -4           # allocate one word for $ra
    sw $ra, 0($sp)              # backup $ra on the stack

    addi $sp, $sp, -12          # allocate three words for backup registers
    sw $t0, 0($sp)              # backup $t0 on the stack [$sp + 0]
    sw $t1, 4($sp)              # backup $t1 on the stack [$sp + 4]
    sw $t2, 8($sp)              # backup $t2 on the stack [$sp + 8]

    addi $sp, $sp, -16          # allocate space for arguments In and OUT
    sw $t0, 0($sp)              # store $t0 as argument IN on the stack [$sp + 0]
    sw $t1, 4($sp)              # store $t1 as argument IN on the stack [$sp + 4]
    sw $t2, 8($sp)              # store $t2 as argument IN on the stack [$sp + 8]

    jal update                  # call subprogram update

    lw $t9, 12($sp)             # load new total from stack [$sp + 12]
    addi $sp, $sp, 16           # deallocate space for arguments IN and OUT

    lw $t0, 0($sp)              # restore $t0 from stack [$sp + 0]
    lw $t1, 4($sp)              # restore $t1 from stack [$sp + 4]
    lw $t2, 8($sp)              # restore $t2 from stack [$sp + 8]
    addi $sp, $sp, 12           # deallocate space for backup register

    lw $ra, 0($sp)              # restore $ra from stack
    addi $sp, $sp, 4            # deallocate space for $ra

    move $t2, $t9               # update the current total

    b menu_loop                 # branch unconditionally to the beginning of menu loop


# menu option 2
menu_option_2:
    li $v0, 4                   # prompt for county number
    la $a0, menu_enter_county_p
    syscall

    li $v0, 5                   # read integer for county number
    syscall

    blez $v0, menu_option_2_error       # county number should be greater than zero
    bgt $v0, $t1, menu_option_2_error   # county number should be less than or equal to count of counties


# calling subprogram single
    addi $sp, $sp, -4           # allocate one word for $ra
    sw $ra, 0($sp)              # backup $ra on the stack

    addi $sp, $sp, -12          # allocate three words for backup registers
    sw $t0, 0($sp)              # backup $t0 on the stack [$sp + 0]
    sw $t1, 4($sp)              # backup $t1 on the stack [$sp + 4]
    sw $t2, 8($sp)              # backup $t2 on the stack [$sp + 8]

    addi $sp, $sp, -8           # allocate space for arguments In and OUT
    sw $t0, 0($sp)              # store $t0 as argument IN on the stack [$sp + 0]
    sw $v0, 4($sp)              # store $t1 as argument IN on the stack [$sp + 4]

    jal single                  # call subprogram single

                                # there is not argument OUT

    addi $sp, $sp, 8            # deallocate space for arguments IN and OUT

    lw $t0, 0($sp)              # restore $t0 from stack [$sp + 0]
    lw $t1, 4($sp)              # restore $t1 from stack [$sp + 4]
    lw $t2, 8($sp)              # restore $t2 from stack [$sp + 8]
    addi $sp, $sp, 12           # deallocate space for backup register

    lw $ra, 0($sp)              # restore $ra from stack
    addi $sp, $sp, 4            # deallocate space for $ra

    b menu_loop                 # branch unconditionally to the beginning of menu loop

# print error message for option 2
menu_option_2_error:
    li $v0, 4                   # print an error message
    la $a0, menu_error_p
    syscall

    b menu_option_2             # branch to beginning of menu_option_2 loop

# menu option 3
menu_option_3:
# calling subprogram range
    addi $sp, $sp, -4           # allocate one word for $ra
    sw $ra, 0($sp)              # backup $ra on the stack

    addi $sp, $sp, -12          # allocate three words for backup registers
    sw $t0, 0($sp)              # backup $t0 on the stack [$sp + 0]
    sw $t1, 4($sp)              # backup $t1 on the stack [$sp + 4]
    sw $t2, 8($sp)              # backup $t2 on the stack [$sp + 8]

    addi $sp, $sp, -8           # allocate space for arguments In and OUT
    sw $t0, 0($sp)              # store $t0 as argument IN on the stack [$sp + 0]
    sw $t1, 4($sp)              # store $t1 as argument IN on the stack [$sp + 4]

    jal range                   # call subprogram range

                                # there is not argument OUT

    addi $sp, $sp, 8            # deallocate space for arguments IN and OUT

    lw $t0, 0($sp)              # restore $t0 from stack [$sp + 0]
    lw $t1, 4($sp)              # restore $t1 from stack [$sp + 4]
    lw $t2, 8($sp)              # restore $t2 from stack [$sp + 8]
    addi $sp, $sp, 12           # deallocate space for backup register

    lw $ra, 0($sp)              # restore $ra from stack
    addi $sp, $sp, 4            # deallocate space for $ra

    b menu_loop                 # branch unconditionally to the beginning of menu loop


# menu option 4
menu_option_4:
# calling subprogram all
    addi $sp, $sp, -4           # allocate one word for $ra
    sw $ra, 0($sp)              # backup $ra on the stack

    addi $sp, $sp, -12          # allocate three words for backup registers
    sw $t0, 0($sp)              # backup $t0 on the stack [$sp + 0]
    sw $t1, 4($sp)              # backup $t1 on the stack [$sp + 4]
    sw $t2, 8($sp)              # backup $t2 on the stack [$sp + 8]

    addi $sp, $sp, -8           # allocate space for arguments In and OUT
    sw $t0, 0($sp)              # store $t0 as argument IN on the stack [$sp + 0]
    sw $t1, 4($sp)              # store $t1 as argument IN on the stack [$sp + 4]

    jal all                     # call subprogram all

                                # there is not argument OUT

    addi $sp, $sp, 8            # deallocate space for arguments IN and OUT

    lw $t0, 0($sp)              # restore $t0 from stack [$sp + 0]
    lw $t1, 4($sp)              # restore $t1 from stack [$sp + 4]
    lw $t2, 8($sp)              # restore $t2 from stack [$sp + 8]
    addi $sp, $sp, 12           # deallocate space for backup register

    lw $ra, 0($sp)              # restore $ra from stack
    addi $sp, $sp, 4            # deallocate space for $ra

    b menu_loop                 # branch unconditionally to the beginning of menu loop

# menu option 5
menu_option_5:
# jump back to main

menu_end:
    sw $t2, 12($sp)             # return the updated total number of licenses back to main

    jr $ra                      # jump back to caller subprogram
###########################################################
#       update subprogram
#
#   Subprogram description:
#       The 'update' subprogram (option 1) will receive three arguments (IN), the base address
#       of the array and the count of its elements and the current total. It will have one
#       argument (OUT) the new total. It will ask the user for the number (n) of the county you
#       wish to update, it will validate that the county number is valid (1-72) or print an error
#       message, it will than display the current number of licenses issued (labeled). It will ask
#       the number of new licenses that are being issued. It will the update element n-1 and the
#       total by the number entered.
#
###########################################################
#       Arguments IN and OUT of subprogram
#   $sp+0   Holds array base address (IN)
#   $sp+4   Holds array length (IN)
#   $sp+8   Holds current total (IN)
#   $sp+12  Holds new total (OUT)
###########################################################
#       Register Usage
#   $t0     Holds array base address
#   $t1     Holds array length
#   $t2     Holds current total
#   $t3
#   $t4
#   $t5
#   $t6
#   $t7
#   $t8     Holds original/new count of licenses
#   $t9     Holds respectively modified array base address
###########################################################
        .data
update_county_p: .asciiz "Enter the county number: "
update_count_p:  .asciiz "Licenses already issued: "
update_new_p:    .asciiz "Enter the number of new licenses issues: "
update_error_p:  .asciiz "Invalid entry! please enter a number in range\n"
###########################################################
        .text
update:
    lw $t0, 0($sp)              # load base address of array
    lw $t1, 4($sp)              # load array size
    lw $t2, 8($sp)              # load current total

update_loop:

update_enter_county:
    li $v0, 4                   # print enter county number:
    la $a0, update_county_p
    syscall

    li $v0, 5                   # read county number
    syscall

    blez $v0, update_error      # county number should be greater than zero
    bgt $v0, $t1, update_error  # county number should be less than total number of counties

    addi $t9, $v0, -1           # $t9 <-- county number - 1
    sll $t9, $t9, 2             # $t9 <-- $t9 * 2^2
    add $t9, $t0, $t9           # $t9 <-- $t9 + base address of array
    lw $t8, 0($t9)              # $t8 <-- memory[$t9 + 0]

    li $v0, 4                   # print licenses already issued:
    la $a0, update_count_p
    syscall

    li $v0, 1                   # print value of licenses already issued
    move $a0, $t8
    syscall

    li $v0, 11                  # print newline character using system call 11
    li $a0, 10
    syscall

update_enter_license:
    li $v0, 4                   # print enter the number of new licenses:
    la $a0, update_new_p
    syscall

    li $v0, 5                   # read number of new licenses
    syscall

    bltz $v0, update_error      # number of new licenses should be positive

    add $t8, $t8, $v0           # $t8 <-- $t8 + number of new licenses // update $t8
    sw $t8, 0($t9)              # memory[$t9 + 9] <-- $t8              // update county array

    add $t2, $t2, $v0           # $t2 <-- $t2 + number of new licenses // update the total

    b update_end                # branch to end of subprogram

update_error:
    li $v0, 4                   # print error message
    la $a0, update_error_p
    syscall

    b update_loop               # branch to beginning of the loop (all over again)

update_end:
    sw $t2, 12($sp)             # return new total back

    jr $ra                      # jump back to caller subprogram
###########################################################
#       single subprogram
#
#   Subprogram description:
#       The 'single' subprogram (option 2) will receive two arguments (IN), the base address of the array
#       the number of the county to be displayed and no arguments (OUT). It will display the county number
#       and the number of licenses that have been issued.
#
###########################################################
#       Arguments IN and OUT of subprogram
#   $sp+0   Holds array base address (IN)
#   $sp+4   Holds number of county to display (IN)
###########################################################
#       Register Usage
#   $t0     Holds array base address
#   $t1     Holds number of county to display
#   $t2
#   $t3
#   $t4
#   $t5
#   $t6
#   $t7
#   $t8
#   $t9
###########################################################
        .data
single_key_p:   .asciiz "county number: "
single_value_p: .asciiz "license count: "
###########################################################
        .text
single:
    lw $t0, 0($sp)              # load base address of array
    lw $t1, 4($sp)              # load the number of county to display

    li $v0, 4                   # print county number is:
    la $a0, single_key_p
    syscall

    li $v0, 1                   # print number of licenses for the county number
    move $a0, $t1
    syscall

    li $v0, 11                  # print tab character using system call 11
    li $a0, 9
    syscall

    li $v0, 4                   # print count of licenses is:
    la $a0, single_value_p
    syscall

    addi $t1, $t1, -1           # $t1 <-- $t1 - 1
    sll $t1, $t1, 2             # $t1 <-- $t1 * 2^2     // because we are working with array of integer
    add $t0, $t0, $t1           # $t0 <-- $t0 + $t1     // and each integer is 4 = 2^2 bytes

    li $v0, 1                   # print count of licenses for respective county number
    lw $a0, 0($t0)
    syscall

    li $v0, 11                  # print newline character using system call 11
    li $a0, 10
    syscall

single_end:
    jr $ra                      # jump back to caller subprogram
###########################################################
#       range subprogram
#
#   Subprogram description:
#       The 'range' subprogram (option 3) will receive two arguments(IN), the base address of the array
#       and the count of its elements and no arguments (OUT). It will ask the user for the starting and
#       ending county numbers. It will validated them and insure that the starting number is less than
#       or equal to the ending number. If it fails to pass these tests it will print an error message
#       and repeat the prompt. It will then call the ‘single’ subprogram in a loop to output the county
#       number and the number of licenses issued for the counties in the range.
#
###########################################################
#       Arguments IN and OUT of subprogram
#   $sp+0   Holds array base address (IN)
#   $sp+4   Holds array length (IN)
###########################################################
#       Register Usage
#   $t0     Holds array base address
#   $t1     Holds array length
#   $t2     Holds starting county number
#   $t3     Holds starting county number
#   $t4
#   $t5
#   $t6
#   $t7
#   $t8
#   $t9
###########################################################
        .data
range_start_p:  .asciiz "Enter starting range: "
range_end_p:    .asciiz "Enter ending range: "
range_error_p:  .asciiz "Invalid entry! range is invalid\n"

range_prompt_p: .asciiz "print of counties between range: "
###########################################################
        .text
range:
    lw $t0, 0($sp)              # load base address of array
    lw $t1, 4($sp)              # load array size

range_loop:
    li $v0, 4                   # print enter staring county
    la $a0, range_start_p
    syscall

    li $v0, 5                   # read integer for starting county
    syscall

    blez $v0, range_error       # starting county number should be greater than zero
    bgt $v0, $t1, range_error   # starting county number should be less than total of counties

    move $t2, $v0               # $t2 <-- $v0       // put starting county into $t2

    li $v0, 4                   # print enter ending county
    la $a0, range_end_p
    syscall

    li $v0, 5                   # read integer for ending county
    syscall

    blez $v0, range_error       # ending county number should be greater than zero
    bgt $v0, $t1, range_error   # ending county number should be less than total of counties

    bgt $t2, $v0, range_error   # starting county should be less than or equal to ending county

    move $t3, $v0               # $t3 <-- $v0       // put ending county into $t2

    li $v0, 4                   # print counties in entered range:
    la $a0, range_prompt_p
    syscall

    li $v0, 1                   # print entered starting county
    move $a0, $t2
    syscall

    li $v0, 11                  # print hyphen character using system call 11
    li $a0, 45
    syscall

    li $v0, 1                   # print entered ending character
    move $a0, $t3
    syscall

    li $v0, 11                  # print newline character using system call 11
    li $a0, 10
    syscall

range_print_loop:
    bgt $t2, $t3, range_end     # if starting county > ending county then branch to range_end

    addi $sp, $sp, -4           # allocate one word for $ra
    sw $ra, 0($sp)              # backup $ra on the stack

    addi $sp, $sp, -16          # allocate four words for backup registers
    sw $t0, 0($sp)              # backup $t0 on the stack [$sp + 0]
    sw $t1, 4($sp)              # backup $t1 on the stack [$sp + 4]
    sw $t2, 8($sp)              # backup $t2 on the stack [$sp + 8]
    sw $t3, 12($sp)             # backup $t3 on the stack [$sp + 12]

    addi $sp, $sp, -8           # allocate space for arguments IN and OUT
    sw $t0, 0($sp)              # store $t0 as argument IN on the stack [$sp + 0]
    sw $t2, 4($sp)              # store $t3 as argument IN on the stack [$sp + 4]

    jal single                  # call subprogram single

    addi $sp, $sp, 8            # deallocate space for arguments IN and OUT

    lw $t0, 0($sp)              # restore $t0 from stack [$sp + 0]
    lw $t1, 4($sp)              # restore $t1 from stack [$sp + 4]
    lw $t2, 8($sp)              # restore $t2 from stack [$sp + 8]
    lw $t3, 12($sp)             # restore $t3 from stack [$sp + 12]
    addi $sp, $sp, 16           # deallocate space for backup registers

    lw $ra, 0($sp)              # restore $ra from stack
    addi $sp, $sp, 4            # deallocate space for $ra

    addi $t2, $t2, 1            # increment starting county number by 1

    b range_print_loop          # branch unconditionally to the beginning of range printing loop

range_error:
    li $v0, 4                   # print error message
    la $a0, range_error_p
    syscall

    b range_loop                # branch unconditionally to the beginning of prompt loop

range_end:
    jr $ra                      # jump back to caller subprogram
###########################################################
#       range subprogram
#
#   Subprogram description:
#       The 'all' subprogram (option 4) will receive three arguments (IN), the base address of the array
#       and the count of its elements and the current total and no arguments (OUT). It will then call the
#       'single' subprogram in a loop to output the county number and the number of licenses issued for
#       the all the counties. It will then print the total of all the counties properly labeled.
#
###########################################################
#       Arguments IN and OUT of subprogram
#   $sp+0   Holds array base address (IN)
#   $sp+4   Holds array length (IN)
###########################################################
#       Register Usage
#   $t0     Holds array base address
#   $t1     Holds array length
#   $t2     Holds counter initialized to 1
#   $t3
#   $t4
#   $t5
#   $t6
#   $t7
#   $t8
#   $t9
###########################################################
         .data
all_prompt_p:  .asciiz "All counties:\n"
###########################################################
        .text
all:
    lw $t0, 0($sp)              # load base address of array
    lw $t1, 4($sp)              # load array size

    li $t2, 1                   # initialize counter to one

    li $v0, 4                   # print All counties:
    la $a0, all_prompt_p
    syscall

all_loop:
    bgt $t2, $t1, all_end       # if counter > array size then branch to all_end

    addi $sp, $sp, -4           # allocate one word for $ra
    sw $ra, 0($sp)              # backup $ra on the stack

    addi $sp, $sp, -16          # allocate four words for backup registers
    sw $t0, 0($sp)              # backup $t0 on the stack [$sp + 0]
    sw $t1, 4($sp)              # backup $t1 on the stack [$sp + 4]
    sw $t2, 8($sp)              # backup $t2 on the stack [$sp + 8]

    addi $sp, $sp, -8           # allocate space for arguments IN and OUT
    sw $t0, 0($sp)              # store $t0 as argument IN on the stack [$sp + 0]
    sw $t2, 4($sp)              # store $t3 as argument IN on the stack [$sp + 4]

    jal single                  # call subprogram single

    addi $sp, $sp, 8            # deallocate space for arguments IN and OUT

    lw $t0, 0($sp)              # restore $t0 from stack [$sp + 0]
    lw $t1, 4($sp)              # restore $t1 from stack [$sp + 4]
    lw $t2, 8($sp)              # restore $t2 from stack [$sp + 8]
    addi $sp, $sp, 16           # deallocate space for backup registers

    lw $ra, 0($sp)              # restore $ra from stack
    addi $sp, $sp, 4            # deallocate space for $ra

    addi $t2, $t2, 1            # increment counter by 1

    b all_loop                  # branch to the beginning of printing loop

all_end:
    jr $ra                      # jump back to caller subprogram
###########################################################

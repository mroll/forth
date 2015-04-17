( Short note on word api's:
    n - integer
    f - result of a boolean expression
    adr - variable address
    n_adr - node address
    r_adr - root address
    c_adr - code address [for delivery to EXECUTE]
)

: NODE  HERE 32 ALLOT DUP DUP 32 0 FILL ROT SWAP ! ; ( n -- adr )

: NULL?   @ 0= ; ( r_adr -- )

: LCHILD   8 + ; ( n_adr -- lc_adr )

: RCHILD   16 + ; ( n_adr -- rc_adr )

: LVL   24 + @ ; ( n_adr -- lvlr )

: SET_LVL  24 + ! ; ( lvl n_adr -- )

: +LVL  DUP LVL 1+ SWAP SET_LVL ; ( n_adr -- )

: SAVE   HERE 1 ALLOT TUCK ! ; ( n_adr -- 'n_adr )

: RROTATE   DUP @ SAVE SWAP DUP DUP @ LCHILD @ SWAP ! @ 2DUP RCHILD @ SWAP @ LCHILD ! RCHILD SWAP @ SWAP ! ; ( r_adr -- )

: LROTATE   DUP @ SAVE OVER @ RCHILD @ ROT TUCK ! 2DUP @ LCHILD @ SWAP @ RCHILD ! TUCK @ LCHILD SWAP @ SWAP ! @ +LVL ; ( r_adr -- )

: HAS_LC   LCHILD @ 0= IF 0 ELSE 1 THEN ; ( n_adr -- f )

: HAS_RC   RCHILD @ 0= IF 0 ELSE 1 THEN ; ( n_adr -- f )

: NOT0   1 * ; ( n -- f )

: SKEW   RECURSIVE DUP @ LVL NOT0 IF DUP DUP @ DUP HAS_LC IF LVL SWAP @ LCHILD @ LVL = IF DUP RROTATE ELSE
                THEN @ RCHILD SKEW ELSE DROP DROP DROP THEN ELSE DROP
                THEN ; ( r_adr -- )

: SPLIT  RECURSIVE DUP NULL? IF DROP ELSE
                DUP @ HAS_RC IF DUP @ RCHILD @ HAS_RC IF DUP DUP @ RCHILD @ LVL SWAP @ LVL DUP NOT0 IF = IF DUP LROTATE SPLIT ELSE
                DROP THEN ELSE
                DROP THEN ELSE
                DROP THEN ELSE
                DROP THEN THEN ; ( r_adr -- )

: HEIGHT   RECURSIVE DUP NULL? IF DROP -1 ELSE
                DUP 1 SWAP @ RCHILD HEIGHT + SWAP 1 SWAP @ LCHILD HEIGHT + MAX THEN ; ( r_adr -- h )

: BALINSERT   RECURSIVE DUP NULL? IF SWAP DUP 1 SWAP SET_LVL SWAP ! ELSE
                2DUP @ @ SWAP @ > IF TUCK TUCK @ LCHILD BALINSERT ELSE
                TUCK TUCK @ RCHILD BALINSERT THEN SKEW SPLIT THEN  ; ( n_adr r_adr -- )

: NSEARCH   RECURSIVE DUP NULL? IF DROP DROP 0 ELSE
                2DUP @ @ = IF 1 ELSE
                2DUP @ @ < IF @ LCHILD NSEARCH ELSE
                @ RCHILD NSEARCH THEN THEN THEN ; ( n r_adr -- f )

: NFIND   RECURSIVE DUP NULL? IF DROP DROP 0 ELSE
                2DUP @ @ = IF @ ELSE
                2DUP @ @ < IF @ LCHILD NFIND ELSE
                @ RCHILD NFIND THEN THEN THEN ; ( n r_adr -- n_adr [0 if not found] )

: PRETRAVERSE   RECURSIVE DUP NULL? IF DROP DROP ELSE
                    2DUP @ SWAP EXECUTE 2DUP @ LCHILD PRETRAVERSE @ RCHILD PRETRAVERSE THEN ; ( c_adr r_adr -- )

: NPRINT   @ . CR ; ( n_adr -- )

VARIABLE 'PT

' NPRINT 'PT !

: PT   'PT @ ; ( -- xt. puts execution token of NPRINT on the stack )

: PREORDER   PT SWAP PRETRAVERSE ; ( r_adr -- )


VARIABLE NROOT

1 NODE NROOT BALINSERT
2 NODE NROOT BALINSERT
3 NODE NROOT BALINSERT
4 NODE NROOT BALINSERT
5 NODE NROOT BALINSERT
6 NODE NROOT BALINSERT
7 NODE NROOT BALINSERT
NROOT PREORDER
NROOT HEIGHT
.S CR

( This prints out a balanced search tree with 7 nodes and leaves a clean stack :)

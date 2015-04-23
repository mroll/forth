( Short note on word api's:
    n - integer
    f - result of a boolean expression
    adr - variable address
    n_adr - node address
    r_adr - root address
    c_adr - code address [for delivery to EXECUTE]
)

: NODE  HERE 40 ALLOT DUP DUP 40 0 FILL ROT SWAP ! ; ( n -- adr )

: NULL?   @ 0= ; ( r_adr -- )

: XNULL? @ 0= 0= ;

: LCHILD   8 + ; ( n_adr -- lc_adr )

: RCHILD   16 + ; ( n_adr -- rc_adr )

: LVL   24 + @ ; ( n_adr -- lvlr )

: SET_LVL  24 + ! ; ( lvl n_adr -- )

: +LVL  DUP LVL 1+ SWAP SET_LVL ; ( n_adr -- )

: PTR   HERE 1 ALLOT ; ( -- ptr )

: SAVE   PTR TUCK ! ; ( n_adr -- 'n_adr )

: RROTATE   DUP @ SAVE SWAP DUP DUP @ LCHILD @ SWAP ! @ 2DUP RCHILD @ SWAP @ LCHILD ! RCHILD SWAP @ SWAP ! ; ( r_adr -- )

: LROTATE   DUP @ SAVE OVER @ RCHILD @ ROT TUCK ! 2DUP @ LCHILD @ SWAP @ RCHILD ! TUCK @ LCHILD SWAP @ SWAP ! @ +LVL ; ( r_adr -- )

: HAS_LC   LCHILD @ 0= 0= ; ( n_adr -- f )

: HAS_RC   RCHILD @ 0= 0= ; ( n_adr -- f )

: NOT0   0= 0= ; ( n -- f )

: SKEW   RECURSIVE DUP @ LVL NOT0 IF DUP DUP @ DUP HAS_LC IF LVL SWAP @ LCHILD @ LVL = IF DUP RROTATE ELSE
                THEN @ RCHILD SKEW ELSE DROP DROP DROP THEN ELSE
                DROP THEN ; ( r_adr -- )

: SPLIT  RECURSIVE DUP NULL? IF DROP ELSE
                DUP @ HAS_RC IF DUP @ RCHILD @ HAS_RC IF DUP DUP @ RCHILD @ LVL SWAP @ LVL DUP NOT0 IF = IF DUP LROTATE SPLIT ELSE
                DROP THEN ELSE
                DROP THEN ELSE
                DROP THEN ELSE
                DROP THEN THEN ; ( r_adr -- )

: HEIGHT   RECURSIVE DUP NULL? IF DROP -1 ELSE
                DUP 1 SWAP @ RCHILD HEIGHT + SWAP 1 SWAP @ LCHILD HEIGHT + MAX THEN ; ( r_adr -- h )

: AINSERT   RECURSIVE DUP NULL? IF SWAP DUP 1 SWAP SET_LVL SWAP ! ELSE
                2DUP @ @ SWAP @ > IF TUCK TUCK @ LCHILD AINSERT ELSE
                TUCK TUCK @ RCHILD AINSERT THEN SKEW SPLIT THEN ; ( n_adr r_adr -- )

: ADELETE   RECURSIVE DUP XNULL?
                                IF 2DUP @ @ =
                                    IF DUP DUP @ LCHILD NULL? SWAP @ RCHILD NULL? + 0=
                                        IF DUP @ LCHILD @ SAVE
                                            BEGIN
                                                DUP @ RCHILD XNULL?
                                                IF DUP DUP @ RCHILD @ SWAP ! 0
                                                ELSE 1
                                                THEN
                                            UNTIL ROT DROP @ @ OVER @ ! DUP @ @ SWAP @ LCHILD ADELETE
                                        ELSE DUP @ HAS_LC
                                            IF SWAP DROP DUP @ LCHILD @ SWAP !
                                            ELSE SWAP DROP DUP @ RCHILD @ SWAP !
                                            THEN
                                        THEN
                                    ELSE 2DUP @ @ <
                                        IF @ LCHILD
                                        ELSE @ RCHILD
                                        THEN ADELETE
                                    THEN
                                 THEN ; ( n r_adr -- )


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
1 NODE NROOT AINSERT
2 NODE NROOT AINSERT
3 NODE NROOT AINSERT
4 NODE NROOT AINSERT
5 NODE NROOT AINSERT
NROOT PREORDER

2 NROOT ADELETE
CR
NROOT PREORDER
4 NROOT ADELETE
CR
NROOT PREORDER
.S CR

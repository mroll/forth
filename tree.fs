( Short note on word api's:
    n - integer
    adr - variable address
    n_adr - node address
    r_adr - root address
    c_adr - code address [for delivery to EXECUTE
)

: NODE  HERE 24 ALLOT DUP DUP 24 0 FILL ROT SWAP ! ; ( n -- adr )
: NULL?   @ 0= ; ( r_adr -- )
: LCHILD   8 + ; ( n_adr -- lc_adr )
: RCHILD   16 + ; ( n_adr -- rc_adr )
: NINSERT   RECURSIVE DUP NULL? IF ! ELSE
                2DUP @ @ SWAP @ > IF @ LCHILD NINSERT ELSE
                @ RCHILD NINSERT THEN THEN ; ( n_adr r_adr -- )

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
: PT   'PT @ ; ( -- )
' NPRINT 'PT !

: PREORDER   PT SWAP PRETRAVERSE ;

: SAVE   HERE 1 ALLOT TUCK ! ; ( n_adr -- 'n_adr )

( this only does the right rotate as of now )
: SKEW   DUP @ SAVE SWAP DUP DUP @ LCHILD @ SWAP ! @ 2DUP RCHILD @ SWAP @ LCHILD ! RCHILD SWAP @ SWAP ! ; ( r_adr -- )


VARIABLE NROOT
4 NODE NROOT NINSERT
3 NODE NROOT NINSERT
5 NODE NROOT NINSERT
1 NODE NROOT NINSERT
2 NODE NROOT NINSERT
7 NODE NROOT NINSERT
10 NODE NROOT NINSERT
6 NODE NROOT NINSERT

NROOT PREORDER CR

NROOT SKEW

NROOT PREORDER CR

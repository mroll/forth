: NODE  HERE 24 ALLOT DUP DUP 24 0 FILL ROT SWAP ! ; ( n -- adr )
: LCHILD   8 + ; ( n_adr -- lc_adr )
: RCHILD   16 + ; ( n_adr -- rc_adr )
: NINSERT   RECURSIVE DUP @ 0= IF ! ELSE 2DUP @ @ SWAP @ > IF @ LCHILD NINSERT ELSE @ RCHILD NINSERT THEN THEN ; ( n_adr r_adr -- )
: NSEARCH   RECURSIVE DUP @ 0= IF DROP DROP 0 ELSE 2DUP @ @ = IF 1 ELSE 2DUP @ @ < IF @ LCHILD NSEARCH ELSE @ RCHILD NSEARCH THEN THEN THEN ; ( n r_adr -- f )
: NFIND   RECURSIVE DUP @ 0= IF DROP DROP 0 ELSE 2DUP @ @ = IF @ ELSE 2DUP @ @ < IF @ LCHILD NFIND ELSE @ RCHILD NFIND THEN THEN THEN ; ( n r_adr -- n_adr [0 if not found] )
: PREORDER   RECURSIVE DUP @ 0= IF DROP ELSE DUP @ @ . DUP @ LCHILD PREORDER @ RCHILD PREORDER THEN ; ( r_adr -- )


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

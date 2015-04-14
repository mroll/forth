( Copied from Leo Brodie's 'Starting Forth' )

VARIABLE BOARD 7 ALLOT
: CLEAR   BOARD  10 0 FILL ;    CLEAR
: SQR   BOARD + ;
: BAR   ." | " ;
: DASHES   CR  9 0 DO ." -" LOOP CR ;
: .BOX   SQR C@  DUP  0= IF 2 SPACES ELSE
            DUP 1 = IF ." X "  ELSE
                       ." O "  THEN THEN DROP ;
: DISPLAY   CR 9 0 DO
    I IF  I 3 MOD 0= IF DASHES ELSE BAR THEN THEN
        I .BOX  LOOP  CR QUIT ;
: PLAY   1-  0 MAX  8 MIN  SQR C! ;
: X!   1 SWAP  PLAY  DISPLAY ;
: O!   -1 SWAP  PLAY  DISPLAY ;

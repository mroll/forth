( Copied from Leo Brodie's 'Starting Forth' )

: STAR   42 EMIT ;
: STARS   0 DO STAR LOOP ;
: MARGIN   30 SPACES ;
: BAR   MARGIN 5 STARS CR ;
: BLIP   MARGIN STAR CR ;
: F   BAR BLIP BAR BLIP BLIP ;
F

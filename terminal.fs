#! /usr/bin/env gforth
require grf.fs
require font.fs
require dragon1.fs

( ------------------------------------------------------------ )
( Redirect gforth terminal to grf + font )

: grf-key
  0 char-box flip
  begin
    wait
    event case
      expose-event of clear 0 char-box flip endof
      press-event of last-key 255 char-box exit endof
    endcase
  again
;

: terminal
  800 600 window

  dragon1 font !
  10 font-width !  20 font-height !  100 font-weight!

  ( intercept input and output )
  ['] font-type is type
  ['] font-emit is emit
  ['] grf-key is xkey

  quit
;
terminal

#! /usr/bin/env gforth

require grf.fs

200 150 window

variable t
: draw height 0 do width 0 do i t @ + width mod 255 width */ j 255 height */ 8 lshift or i j pixel l! loop loop flip ;
: test begin poll draw event press-event = if exit then 1 t +! again ;
test bye
require grf.fs
require font.fs
require dragon1.fs

get-current
vocabulary slides also slides definitions
( private )

( ------------------------------------------------------------ )
( Formatting utilities )

: centered ( n -- ) width swap font-width @ * - 2/ font-x ! ;
: center-type ( a n -- ) dup centered font-type font-cr ;
: left-type ( a n -- ) font-type font-cr ;
: font-size! ( n -- ) width over / font-width !
                      height 2* swap / font-height ! ;
: font-pick ( b cols -- ) font-size!
                          width 20 */ font-weight!
                         0 font-slant ! ;
: normal  2 44 font-pick ;
: tiny  1 100 font-pick ;
: bullet  10 44 font-pick font-cr s"   ~ " font-type normal ;
: indent1  s"  " font-type ;
: indent4  normal s"     " font-type ;


( ------------------------------------------------------------ )
( Slide drawing )

1000 constant max-slides
create deck max-slides cells allot
variable deck-count
variable slide
: slide-num   tiny home height font-margin @ - font-y !
              slide @ 1+ s>d <# #s #> font-type
              s"  of " font-type
              deck-count @ s>d <# #s #> font-type ;
: draw   deck slide @ cells + @ clear execute slide-num flip ;
: slide-clip   slide @ 0 max deck-count @ 1- min slide ! ;
: slide-step ( n -- ) slide +! slide-clip draw ;
: backward -1 slide-step ;
: forward 1 slide-step ;

65361 constant left-key
65363 constant right-key
65307 constant escape-key
32 constant space-key

set-current
( public )

( Font sizes )
: super-big   10 20 font-pick ;
: big   6 30 font-pick ;
: normal   normal ;
: tiny   tiny ;
: font-pick ( b col -- ) font-pick ;
: font-size! ( n -- ) font-size! ;

( Bullets and Printing )
: center-type ( s -- ) center-type ;
: bspace font-width @ negate font-x +! ;
: bullet   bullet ;

: .f"   postpone s" postpone font-type ; immediate
: .fl"   postpone s" postpone left-type ; immediate
: *f"   postpone bullet postpone .fl" ; immediate
: +f"   postpone indent4 postpone .fl" ; immediate
: .title"   postpone big postpone home postpone indent1
            postpone .fl" postpone font-cr ; immediate
: fn. ( n -- )
    s>d swap over dabs <<# 32 hold #s rot sign #> font-type #>> ;

( Create unnamed slides )
: +slide ( fn -- ) deck deck-count @ cells + !  1 deck-count +! ;
: slide:   :noname ;
: ;slide   postpone ; +slide ; immediate

( Start the slideshow )
: slideshow
  depth throw
  1024 768 window
  begin
    wait
    event case
      expose-event of expose-count 0= if draw then endof
      press-event of
        last-keysym case
          left-key of backward endof
          right-key of forward endof
          space-key of forward endof
          escape-key of bye endof
        endcase
      endof
    endcase
  again
;

( Default font )
dragon1 font !

previous


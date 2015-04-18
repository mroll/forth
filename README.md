# Forth Hacks

## General

I would like to use this repository to store code I write while exploring the Forth language. I am using the GNU Forth interpreter (gforth) on Mac OSX Yosemite. Installation is probably easiest through homebrew: `brew install gforth`.

Forth files can be loaded and executed with `gforth <file> -e bye`. The 'bye' at the end simply exits the interpreter when the code is finished running.

## Andersson Trees

The file **tree.fs** contains an implementation of self-balancing Andersson trees. I first came to the idea of making some binary trees when thinking about a multiplayer for tic-tac-toe, one that would follow possible win paths down a tree. Once I had a simple tree with an insert method, I decided to look for a way to make it more interesting, and settled on finding some kind of balancing algorithm. Andersson trees looked simple and efficient.

I got my understanding of the trees from the Wikipedia article [Andersson Trees](http://en.wikipedia.org/wiki/AA_tree) and this tutorial: [Eternally Confuzed](http://eternallyconfuzzled.com/tuts/datastructures/jsw_tut_andersson.aspx).

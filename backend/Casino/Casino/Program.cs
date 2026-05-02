using Casino.Models;
using Casino.Menu;

var casino = new CasinoGame();
var menu = new CasinoMenu( casino );
menu.Run();
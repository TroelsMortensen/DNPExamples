This example illustrates how to create a very simple animation loop.
It uses javascript, defined in ./wwwroot/script.js.
This script is referenced in ./Pages/_Host.cshtml.
The functionality is in ./Pages/Animation.razor

The script defines an fps, which indicates how often the GameLoop method in Animation.razor.

The IDisposable interface is implemented in Animation.razor in order to stop the animation loop, whenever the Animation page is closed.
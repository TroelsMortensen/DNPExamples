function gameLoop(timeStamp) {
    if (!running) return;
    window.requestAnimationFrame(gameLoop);
    if (lastTime + fps <= timeStamp) {
        lastTime = timeStamp + fps;
        theInstance.invokeMethodAsync('GameLoop', timeStamp);
    }
}

window.initGame = (instance) => {
    window.fps = 1000 / 5;
    window.running = true;
    window.theInstance = instance;
    window.requestAnimationFrame(gameLoop);
};
window.lastTime = 0;
window.stop = () => running = false;
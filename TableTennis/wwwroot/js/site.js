// Scoreboard timer
window.pingisTimer = window.pingisTimer || {};

window.pingisTimer.start = function () {
    var timerElement = document.getElementById('timer');
    if (!timerElement) return;
    var startTime = Date.now();

    function updateTimer() {
        var elapsed = Math.floor((Date.now() - startTime) / 1000);
        var min = String(Math.floor(elapsed / 60)).padStart(2, '0');
        var sec = String(elapsed % 60).padStart(2, '0');
        timerElement.textContent = min + ':' + sec;
    }

    // Kör direkt och varje sekund
    updateTimer();
    window.pingisTimer._interval = setInterval(updateTimer, 1000);
};

// Starta om timern
window.pingisTimer.reset = function () {
    if (window.pingisTimer._interval) {
        clearInterval(window.pingisTimer._interval);
    }
    window.pingisTimer.start();
};

// Starta alltid timer om elementet finns på sidan!
document.addEventListener('DOMContentLoaded', function () {
    if (document.getElementById('timer')) {
        window.pingisTimer.reset();
    }
});

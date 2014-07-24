(function (w, d, s, g, js, fjs) {
    g = w.gapi || (w.gapi = {}); g.analytics = { q: [], ready: function (cb) { this.q.push(cb) } };
    js = d.createElement(s); fjs = d.getElementsByTagName(s)[0];
    js.src = 'https://apis.google.com/js/platform.js';
    fjs.parentNode.insertBefore(js, fjs); js.onload = function () { g.load('analytics') };
}(window, document, 'script'));

gapi.analytics.ready(function () {
    gapi.analytics.auth.authorize({
        container: 'auth-button',
        clientid: '177416409642-clnid6erj4abcvm1v9rkuhskogoi5m60.apps.googleusercontent.com'
    });

    gapi.analytics.auth.on('success', function () {
        $('#auth-button').hide();
        getVisitors();
        getIntranetVisitors();
        setInterval(getVisitors, 10000);
        setInterval(getIntranetVisitors, 10000);
    })
});
class Cef {
    constructor(/*name, */path) {
        this.cursor = true;
        this.open = false;
        this.external = false;
        this.headless = false;
        this.chat = false;
        this.hud = false;

        //this.name = name;
        this.path = path;
    }

    load() {
        if (this.open) {
            this.destroy();
        }

        const resolution = API.getScreenResolution();

        this.browser = API.createCefBrowser(resolution.Width, resolution.Height, this.external);
        API.waitUntilCefBrowserInit(this.browser);
        API.setCefBrowserPosition(this.browser, 0, 0);
        API.setCefBrowserHeadless(this.browser, this.headless);
        API.loadPageCefBrowser(this.browser, this.path);

        if (!this.chat) { API.setCanOpenChat(false); }
        if (!this.hud) { API.setHudVisible(false); }
        if (this.cursor) { API.showCursor(true); }
        this.setOpen(true);
    }

    destroy() {
        if (!this.open) {
            return;
        }
        API.destroyCefBrowser(this.browser);

        if (!this.chat) { API.setCanOpenChat(true); }
        if (!this.hud) { API.setHudVisible(true); }
        if (this.cursor) { API.showCursor(false); }

        this.setOpen(false);
    }

    eval(evalString) {
        this.browser.eval(evalString);
    }

    setExternal(newValue) { this.external = newValue; }
    setHeadless(newValue) { this.headless = newValue; }
    setCursorVisible(newValue) { this.cursor = newValue; }
    setChatVisible(newValue) { this.chat = newValue; }
    setHudVisible(newValue) { this.hud = newValue; }
    setOpen(newValue) { this.open = newValue; }
}

var cef = [];
/*0 - Connexion
1 - Binco
2 - Drogue
3 - Coffre
*/

API.onResourceStart.connect(function () {
    cef[0] = new Cef('Interfaces/Connexion/connexion.html');
    cef[0].setExternal(true);
    cef[0].setCursorVisible(true);

    cef[1] = new Cef('Interfaces/binco.html');
    cef[1].setExternal(true);

    cef[2] = new Cef('Interfaces/cannabis.html');
    cef[2].setExternal(true);

    cef[3] = new Cef('Interfaces/coffre.html');
    cef[3].setExternal(true);
    cef[3].setCursorVisible(true);

    cef[4] = new Cef('Interfaces/ATM/atm.html');
    cef[4].setExternal(true);
    cef[4].setCursorVisible(true);
});

API.onServerEventTrigger.connect(function (name, args) {
    if (name === "showLogin") { //Connexion
        cef[0].load();
    }
    if (name === "ShowCEFBinco") { //Connexion
        cef[1].setChatVisible(true);
        cef[1].load();
    }
    if (name === "HideCEFBinco") { //Connexion
        cef[1].destroy();
    }
    if (name === "ShowCEFDrogue") { //Connexion
        cef[2].setCursorVisible(true);
        cef[2].setChatVisible(true);
        cef[2].load();
    }
    if (name === "HideCEFDrogue") { //Connexion
        cef[2].destroy();
    }
    if (name === "ShowCoffre") { //Connexion
        if (cef[3].open) cef[3].destroy();
        else cef[3].load();
    }
    if (name === "showATM") { //Connexion
        cef[4].load();
    }

});
//--- Connexion
function sendInput(pseudo, password) {
    API.triggerServerEvent("CefConnexion", pseudo, password);
    cef[0].destroy();
}

function sendInputInscription(pseudo, NomIG, email, password) {
    API.triggerServerEvent("CefInscription", pseudo, NomIG, email, password);
}

//--- ATM
function retirerATM(montant) {
    cef[4].destroy();
    API.triggerServerEvent("RetirerATM", montant);
}

function consulterATM() {
    return API.getEntitySyncedData(API.getLocalPlayer(), "SoldeCompte");
}

function ExitATM() {
    cef[4].destroy();
}
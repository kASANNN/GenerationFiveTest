var Antifloodtime = new Date();

API.onKeyDown.connect(function (sender, args) {
    if ((new Date() - Antifloodtime) / 500 <= 1) {
        return;
    }
    Antifloodtime = new Date();
    switch (args.KeyCode) {
        case Keys.E:
        case Keys.A:
        case Keys.W:
        case Keys.R:
        case Keys.F1:
        case Keys.F2:
            {
                API.triggerServerEvent("keymanageronKeyDown", args.KeyValue);
                break;
            }
    }       
});
API.onKeyUp.connect(function (sender, args) {
    API.triggerServerEvent("keymanageronKeyUp", args.KeyValue);
});
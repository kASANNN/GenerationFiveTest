var blips = []
var BlipBalise = null;
var BlipBalise2 = null;
API.onServerEventTrigger.connect(function (name, args) {
    if (name === "showBlip") {
        API.setBlipTransparency(args[0], 255);
        }
    if (name === "deleteBlips") {
        for (var i = 0; i < blips.length; i++) {
            API.deleteEntity(blips[i]);
        }
    }
    if (name === "makeBlips") {
        for (var u = 1; u < args[0]; u++) {
           var blip = API.createBlip(args[u]);
            blips.push(blip);
        }
    }
    //pour le Commandes/CommandesPolice
    if (name === "BaliseLSPD") {
        BlipBalise = API.createBlip(new Vector3(args[0], args[1], args[2]));
        API.setBlipSprite(BlipBalise, 1);
        API.setBlipRouteVisible(BlipBalise, true);
        API.setBlipColor(BlipBalise, 24);
        API.setBlipRouteColor(BlipBalise, 24);
        API.setEntitySyncedData(API.getLocalPlayer(), args[3], BlipBalise);
    }

    if (name === "DeleteBaliseLSPD") {
        BlipBalise2 = API.getEntitySyncedData(API.getLocalPlayer(), args[0]);
        API.deleteEntity(BlipBalise2);
        API.setEntitySyncedData(API.getLocalPlayer(), args[0], null);
    }

    if (name === "RechargementPistol") {
        API.triggerServerEvent("RechargementPistol");
    }
});
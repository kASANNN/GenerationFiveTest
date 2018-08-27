var Blip = null;
var Blip2 = null;

API.onServerEventTrigger.connect(function (eventName, arguments) {
    if (eventName == "pointconduiteAE") {
        Blip = API.createBlip(new Vector3(arguments[0], arguments[1], arguments[2]));
        API.setBlipSprite(Blip, 1);
        API.setBlipRouteVisible(Blip, true);
        API.setBlipColor(Blip, 24);
        API.setBlipRouteColor(Blip, 24);
        API.setEntitySyncedData(API.getLocalPlayer(), "BlipAE", Blip);
    }
    if (eventName == "DeleteMarkerAE") {
        Blip2 = API.getEntitySyncedData(API.getLocalPlayer(), "BlipAE");
        API.deleteEntity(Blip2);
    }
});
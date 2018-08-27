API.onServerEventTrigger.connect(function(eventName, arguments) {
    if (eventName == "pointconv")
    {
        API.setWaypoint(-23.92922, -718.1271);
    }
    if (eventName == "retourconv")
    {
        API.setWaypoint(490.227, -1402.756);
    }
});
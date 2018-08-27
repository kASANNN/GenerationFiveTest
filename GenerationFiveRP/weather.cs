using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.IO;

namespace Weather
{
    #region Weather Objects

    public class Units
    {
        public string distance { get; set; }
        public string pressure { get; set; }
        public string speed { get; set; }
        public string temperature { get; set; }
    }

    public class Location
    {
        public string city { get; set; }
        public string country { get; set; }
        public string region { get; set; }
    }

    public class Wind
    {
        public string chill { get; set; }
        public string direction { get; set; }
        public string speed { get; set; }
    }

    public class Atmosphere
    {
        public string humidity { get; set; }
        public string pressure { get; set; }
        public string rising { get; set; }
        public string visibility { get; set; }
    }

    public class Astronomy
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
    }

    public class Image
    {
        public string title { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string link { get; set; }
        public string url { get; set; }
    }

    public class Condition
    {
        public string code { get; set; }
        public string date { get; set; }
        public string temp { get; set; }
        public string text { get; set; }
    }

    public class Forecast
    {
        public string code { get; set; }
        public string date { get; set; }
        public string day { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string text { get; set; }
    }

    public class Guid
    {
        public string isPermaLink { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
        public string link { get; set; }
        public string pubDate { get; set; }
        public Condition condition { get; set; }
        public List<Forecast> forecast { get; set; }
        public string description { get; set; }
        public Guid guid { get; set; }
    }

    public class Channel
    {
        public Units units { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public string lastBuildDate { get; set; }
        public string ttl { get; set; }
        public Location location { get; set; }
        public Wind wind { get; set; }
        public Atmosphere atmosphere { get; set; }
        public Astronomy astronomy { get; set; }
        public Image image { get; set; }
        public Item item { get; set; }
    }

    public class Results
    {
        public Channel channel { get; set; }
    }

    public class Query
    {
        public int count { get; set; }
        public string created { get; set; }
        public string lang { get; set; }
        public Results results { get; set; }
    }

    public class RootObject
    {
        public Query query { get; set; }
    }
    #endregion

    [Serializable]
    public struct WeatherData
    {
        public int RefreshRate { get; set; }
        public string WoeID { get; set; }
        public bool DisplayUpdate { get; set; }
    }

    public enum WeatherID
    {
        Extra_Sunny,
        Clear,
        Clouds,
        Smog,
        Foggy,
        Overcast,
        Rain,
        Thunder,
        Light_rain,
        Smoggy_light_rain,
        Very_light_snow,
        Windy_light_snow,
        Light_snow
    }

    public class Weather : Script
    {
        private Thread weatherThread;
        private WeatherData weatherData;

        public delegate void OnWeatherDataReceived(string weather);
        public static OnWeatherDataReceived onWeatherDataReceived;


        public Weather()
        {
            API.onResourceStart += OnResourceStart;
            API.onResourceStop += OnResourceStop;
        }

        private void OnResourceStop()
        {
            try
            {
                if (weatherThread != null)
                    weatherThread.Abort();
                onWeatherDataReceived -= WeatherDataReceived;
            }
            catch (Exception e)
            {
                API.consoleOutput(e.ToString());
            }
        }

        private void changeWeather(WeatherID weather)
        {
            if (weatherData.DisplayUpdate)
                API.consoleOutput("Changing weather to " + weather.ToString());
            API.setWeather((int)weather);
        }

        private void WeatherDataReceived(string weather)
        {
            if (weatherData.DisplayUpdate)
                API.consoleOutput("Current Weather: " + weather);
            switch (weather.ToLower())
            {
                //THUNDER
                case "tornado":
                case "tropical storm":
                case "hurricane":
                case "severe thunderstorms":
                case "thunderstorms":
                case "thundershowers":
                case "isolated thundershowers":
                    changeWeather(WeatherID.Thunder);
                    break;
                //CLEAR
                case "clear (night)":
                case "clear":
                    changeWeather(WeatherID.Clear);
                    break;
                //EXTRA SUNNY
                case "sunny":
                case "hot":
                    changeWeather(WeatherID.Extra_Sunny);
                    break;
                //CLOUDS
                case "mostly cloudy (night)":
                case "mostly cloudy (day)":
                case "cloudy":
                case "mostly cloudy":
                    changeWeather(WeatherID.Clouds);
                    break;
                //OVERCAST
                case "partly cloudy (day)":
                case "partly cloudy (night)":
                case "partly cloudy":
                case "sleet":
                case "dust":
                    changeWeather(WeatherID.Overcast);
                    break;
                //RAIN
                case "freezing drizzle":
                case "drizzle":
                case "freezing rain":
                case "showers":
                case "hail":
                case "scattered thunderstorms":
                case "scattered showers":
                case "mixed rain and hail":
                    changeWeather(WeatherID.Rain);
                    break;
                //CLEARING
                case "fair (night)":
                case "fair (day)":
                    changeWeather(WeatherID.Clear);
                    break;
                //FOGGY
                case "haze":
                case "foggy":
                case "smoky":
                    changeWeather(WeatherID.Foggy);
                    break;
                //BLIZZARD
                case "heavy snow":
                case "snow showers":
                    changeWeather(WeatherID.Light_snow);
                    break;
                //XMAS
                case "snow flurries":
                case "light snow showers":
                case "blowing snow":
                case "snow":
                    changeWeather(WeatherID.Light_snow);
                    break;
                default:
                    //CLEAR
                    changeWeather(WeatherID.Clear);
                    break;
            }
        }


        private void OnResourceStart()
        {
            onWeatherDataReceived += WeatherDataReceived;
            var data = File.ReadAllText(API.getResourceFolder() + "/Weather/data.json");
            weatherData = API.fromJson(data).ToObject<WeatherData>();

            API.consoleOutput("~~~~~ Live Weather ~~~~~");
            API.consoleOutput("City WoeID: " + weatherData.WoeID);
            API.consoleOutput("Refresh Rate: " + weatherData.RefreshRate);
            API.consoleOutput("Display Update: " + weatherData.DisplayUpdate);
            API.consoleOutput("~~~~~ Live Weather ~~~~~");

            weatherThread = new Thread(WeatherCheck);
            weatherThread.Start();
        }

        private void WeatherCheck()
        {
            while (true)
            {
                try
                {
                    string query = "https://query.yahooapis.com/v1/public/yql?format=json&q=SELECT * FROM weather.forecast WHERE u=%27c%27 AND woeid = " + weatherData.WoeID;
                    var json = new WebClient().DownloadString(query);
                    RootObject obj = API.fromJson(json).ToObject<RootObject>();
                    onWeatherDataReceived(obj.query.results.channel.item.condition.text);
                    Thread.Sleep(weatherData.RefreshRate);
                }
                catch (Exception e)
                {
                    API.consoleOutput("Weather exception: " + e);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
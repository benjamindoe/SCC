using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using Newtonsoft.Json.Linq;
using System.Linq;

public class Exchange
{
    public string Base { get; set; }
    public string Date { get; set; }
    public Dictionary<string,double> Rates { get; set; }
}
public class GoogleMaps
{
    public string html_instructions { get; set; }
    public distance Distance { get; set; }
}
public class distance
{
    public string text { get; set; }
    public int value { get; set; }
}

public static class RestClient
{
    private static HttpClient makeClient(string uri)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(uri);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return client;
    }
    public static Exchange getAllExRates(string Base="GBP")
    {
        HttpClient ccClient = makeClient("http://api.fixer.io/");
        HttpResponseMessage response = ccClient.GetAsync("latest?base=" + Base).Result;
        string json = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<Exchange>(json);
    }
    public static Exchange getExRate(string Base="GBP", string symbol = "")
    {
        HttpClient ccClient = makeClient("http://api.fixer.io/");
        HttpResponseMessage response = ccClient.GetAsync("latest?base="+Base+"&symbols=" + symbol).Result;
        string json = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<Exchange>(json);
    }
    public static List<string> getDirections(string start, string end)
    {
        HttpClient mapClient = makeClient("https://maps.googleapis.com/maps/api/directions/json");
        HttpResponseMessage response = mapClient.GetAsync("?origin=" +start+ "&destination="+end).Result;
        JObject mapRoutes = JObject.Parse(response.Content.ReadAsStringAsync().Result);
        IList<JToken> steps = mapRoutes["routes"][0]["legs"][0]["steps"].Children().ToList();
        List<string> instructions = new List<string>();
        foreach(JToken step in steps)
        {
            GoogleMaps instrucion = JsonConvert.DeserializeObject<GoogleMaps>(step.ToString());
            instructions.Add(instrucion.html_instructions + " (" + instrucion.Distance.text + ")");
        }
        return instructions;
    }
}
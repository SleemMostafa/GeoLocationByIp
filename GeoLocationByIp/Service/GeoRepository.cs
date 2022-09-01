using GeoLocationByIp.Model;

namespace GeoLocationByIp.Service
{
    public class GeoRepository
    {
        public LocationDetails_IpApi GepLocationById (string ip)
        {
            var Ip_Api_Url = $"http://ip-api.com/json/{ip}"; // 206.189.139.232 - This is a sample IP address. You can pass yours if you want to test          

            // Use HttpClient to get the details from the Json response
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // Pass API address to get the Geolocation details 
                httpClient.BaseAddress = new Uri(Ip_Api_Url);
                HttpResponseMessage httpResponse = httpClient.GetAsync(Ip_Api_Url).GetAwaiter().GetResult();
                // If API is success and receive the response, then get the location details
                if (httpResponse.IsSuccessStatusCode)
                {
                    var geolocationInfo = httpResponse.Content.ReadFromJsonAsync<LocationDetails_IpApi>().GetAwaiter().GetResult();
                    if (geolocationInfo != null)
                    {
                        return new LocationDetails_IpApi
                        {
                            city = geolocationInfo.city,
                            country = geolocationInfo.country,
                            countryCode = geolocationInfo.countryCode,
                            isp = geolocationInfo.isp,
                            lat = geolocationInfo.lat,
                            lon = geolocationInfo.lon,
                            org = geolocationInfo.org,
                            query = geolocationInfo.query,
                            region = geolocationInfo.region,
                            regionName = geolocationInfo.regionName,
                            status = geolocationInfo.status,
                            timezone = geolocationInfo.timezone,
                            zip = geolocationInfo.zip,
                        };

                    }
                    return null;
                }
                return null;
            }

        }
    }
}

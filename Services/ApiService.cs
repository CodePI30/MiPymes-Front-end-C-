using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using MiPymes_Front_end_C_.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MiPymes_Front_end_C_.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<MiPyme>> GetMiPymesAsync (string? rnc = null)
        {
            var response = await _httpClient.GetStringAsync($"MyTable?rnc={rnc}");
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.DeserializeObject<List<MiPyme>>(response, settings);
        }


        public async Task<MiPyme> GetMiPymeByRNC(string? rnc = null)
        {
            // Hacemos la solicitud a la API
            var response = await _httpClient.GetStringAsync($"MyTable?rnc={rnc}");

            // Configuración de deserialización
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            // Deserializar el JSON a una lista de MiPyme
            var miPymesList = JsonConvert.DeserializeObject<List<MiPyme>>(response, settings);

            // Obtén el primer elemento de la lista o null si está vacío
            return miPymesList?.FirstOrDefault();
        }



        public async Task<MiPyme> PostNewMiPymeAsync(MiPyme newMyPymes)
        {
            var response = await _httpClient.PostAsJsonAsync("MyTable", newMyPymes);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MiPyme>();
        }

        // Método para actualizar una MiPyme existente (PUT)
        public async Task PutMiPymeAsync(string rnc, MiPyme updatedMiPyme)
        {
            var response = await _httpClient.PutAsJsonAsync($"MyTable/{rnc}", updatedMiPyme);
            response.EnsureSuccessStatusCode();
            
        }

        public async Task<bool> DeleteMiPymeAsync(string rnc)
        {
            var response = await _httpClient.DeleteAsync($"MyTable/{rnc}");
            return response.IsSuccessStatusCode;
        }

        //========================================================================================

        //Servicio API para obtener empresas por RNC
        public async Task<Empresa> GetEmpresaByRNCAsync(string rnc)
        {
            var response = await _httpClient.GetStringAsync($"DGII/consultar/{rnc}");
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.DeserializeObject<Empresa>(response, settings);
        }
    }
}

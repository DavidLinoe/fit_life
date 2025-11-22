using System.Text;
using System.Text.Json;

namespace fit_life.Services
{
    public class LlmService: ILlmService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public LlmService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> ObterResposta(string prompt)
        {
            var apiKey = _configuration["LLM:Key"];
            var baseUrl = _configuration["LLM:Url"];

            var apiUrl = $"{baseUrl}?key={apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(apiUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                return $"Erro ao conectar com a IA: {response.StatusCode} - {errorMsg}";
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            try
            {
                using var doc = JsonDocument.Parse(jsonResponse);
                var textoResposta = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return textoResposta ?? "Sem resposta da IA.";
            }
            catch
            {
                return "A IA respondeu, mas não consegui ler o formato.";
            }
        }

        public async Task<string> GerarTreino(string perfil, string objetivo, int diasPorSemana)
        {
            var promptPersonalizado = $@"
                Atue como um Personal Trainer especialista chamado 'FitLife Coach'.
                Crie um plano de treino para um aluno com o seguinte perfil:
                - Nível: {perfil}
                - Objetivo: {objetivo}
                - Disponibilidade: {diasPorSemana} dias por semana.

                Retorne a resposta formatada com tópicos, separando por Dia A, Dia B, etc.
                Seja direto e motivador.
            ";

            return await ObterResposta(promptPersonalizado);
        }
    }
}

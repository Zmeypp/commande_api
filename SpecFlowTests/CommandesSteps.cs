using TechTalk.SpecFlow;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;
using CommandesAPI;

namespace SpecFlowTests
{
    [Binding]
    public class CommandesSteps
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private Commande _nouvelleCommande;
        private Commande _commandeObtenue;

        public CommandesSteps(HttpClient client)
        {
            _client = client;
        }

        [Given(@"que l'API est en cours d'exécution")]
        public void GivenApiIsRunning()
        {
            // Pas d'actions nécessaires, l'API est supposée être en cours d'exécution
        }

        [Given(@"je suis sur la page de création de commande")]
        public void GivenIAmOnCreateOrderPage()
        {
            // Pas d'actions nécessaires, il s'agit simplement d'une spécification
        }

        [When(@"je crée une nouvelle commande avec les détails suivants:")]
        public async Task WhenICreateNewOrder(Table table)
        {
            _nouvelleCommande = table.CreateInstance<Commande>();
            _response = await _client.PostAsJsonAsync("/commandes", _nouvelleCommande);
            _response.EnsureSuccessStatusCode();
        }

        [Then(@"la commande est créée avec succès")]
        public async Task ThenOrderIsCreatedSuccessfully()
        {
            Assert.Equal(HttpStatusCode.Created, _response.StatusCode);
        }

        [Then(@"je peux voir la commande dans la liste des commandes")]
        public async Task ThenICanSeeOrderInOrderList()
        {
            var commandes = await _client.GetFromJsonAsync<List<Commande>>("/commandes");
            Assert.Contains(commandes, c => c.Id == _nouvelleCommande.Id);
        }

        [Given(@"je connais l'ID d'une commande existante")]
        public void GivenIHaveExistingOrderId()
        {
            // Supposons que nous avons un ID valide pour une commande existante
        }

        [When(@"je demande les détails de la commande avec l'ID spécifié")]
        public async Task WhenIRequestOrderDetailsById()
        {
            _response = await _client.GetAsync($"/commandes/{existingOrderId}");
            _response.EnsureSuccessStatusCode();
            _commandeObtenue = await _response.Content.ReadFromJsonAsync<Commande>();
        }

        [Then(@"je reçois les détails de la commande demandée")]
        public void ThenIGetOrderDetails()
        {
            Assert.NotNull(_commandeObtenue);
            // Ajoutez des assertions supplémentaires ici pour vérifier les détails de la commande obtenue
        }

        // Ajoutez les autres étapes de test correspondant aux autres phrases du test Cucumber
    }
}

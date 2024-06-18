using Microsoft.AspNetCore.Mvc;
using NET105_ASM_MVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace NET105_ASM_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {

            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(new
                {
                    email = model.Email,
                    password = model.Password
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var client = _httpClientFactory.CreateClient();

                try
                {
                    var response = await client.PostAsync("https://localhost:7251/api/Token", content);
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("API Response: " + apiResponse);

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenResponse = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                        string token = tokenResponse.token;

                        HttpContext.Session.SetString("JWToken", token);
      
                        return RedirectToAction("ViewList");
                    }
                    else
                    {
                        Console.WriteLine($"API Response Status: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("HTTP Request Exception: " + ex.Message);
                    ModelState.AddModelError(string.Empty, "Error connecting to API.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    ModelState.AddModelError(string.Empty, "An error occurred.");
                }
            }

            return View(model);
        }
        private async Task<string> GetTokenAsync()
        {
            return await Task.FromResult(HttpContext.Session.GetString("JWToken"));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Token not available. Please log in.";
                return View("Login");
            }
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                FoodItem fooditem = new FoodItem();
            var ft = await client.GetAsync($"https://localhost:7251/api/FoodItems/{id}");
            if (ft.IsSuccessStatusCode)
            {
                var a = await ft.Content.ReadAsStringAsync();
                fooditem = JsonConvert.DeserializeObject<FoodItem>(a);
            }
            return View(fooditem);
        }

        public async Task<IActionResult> EditCombo(int id)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Token not available. Please log in.";
                return View("Login");
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Combo combo = new Combo();
            var ft = await client.GetAsync($"https://localhost:7251/api/Comboes/{id}");
            if (ft.IsSuccessStatusCode)
            {
                var a = await ft.Content.ReadAsStringAsync();
                combo = JsonConvert.DeserializeObject<Combo>(a);
            }
            return View(combo);
        }

        [HttpPost]
        public async Task<IActionResult> EditCombo(int id, Combo combo)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Token not available. Please log in.";
                return View("Login");
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var ft = new StringContent(JsonConvert.SerializeObject(combo), Encoding.UTF8, "application/json");
            var a = await client.PutAsync($"https://localhost:7251/api/Comboes/{id}", ft);
            if (a.IsSuccessStatusCode)
                return RedirectToAction(nameof(ViewList));

            return View(combo);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, FoodItem foodItem)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Token not available. Please log in.";
                return View("Login");
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var ft = new StringContent(JsonConvert.SerializeObject(foodItem), Encoding.UTF8, "application/json");
            var a = await client.PutAsync($"https://localhost:7251/api/FoodItems/{id}", ft);
            if (a.IsSuccessStatusCode)
                return RedirectToAction(nameof(ViewList));

            return View(foodItem);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Token not available. Please log in.";
                return View("Login");
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            FoodItem foodItem = new FoodItem();
            var ft = await client.GetAsync($"https://localhost:7251/api/FoodItems/{id}");
            if (ft.IsSuccessStatusCode)
            {
                var a = await ft.Content.ReadAsStringAsync();
                foodItem = JsonConvert.DeserializeObject<FoodItem>(a);
            }
            return View(foodItem);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Token not available. Please log in.";
                return View("Login");
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"https://localhost:7251/api/FoodItems/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(ViewList));
            }
            return View(0); // Adjust this as needed
        }

        public async Task<IActionResult> Details(int id)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Token not available. Please log in.";
                return View("Login");
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            FoodItem foodItem = new FoodItem();
            var response = await client.GetAsync($"https://localhost:7251/api/FoodItems/{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                foodItem = JsonConvert.DeserializeObject<FoodItem>(apiResponse);
            }

            return View(foodItem);
        }

        public async Task<IActionResult> ViewList()
        {

            var token = HttpContext.Session.GetString("JWToken");
  
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Token not available. Please log in.";
                return View("Login");
            }
                List<FoodItem> FoodItemList = new List<FoodItem>();
            var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var fooditem = await client.GetAsync("https://localhost:7251/api/FoodItems");
                if (fooditem.IsSuccessStatusCode)
                {
                    var fi = await fooditem.Content.ReadAsStringAsync();
                    FoodItemList = JsonConvert.DeserializeObject<List<FoodItem>>(fi);
                }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            List<Combo> combo = new List<Combo>();
            var response1 = await client.GetAsync($"https://localhost:7251/api/Comboes");
            if (response1.IsSuccessStatusCode)
            {
                var apiResponse = await response1.Content.ReadAsStringAsync();
                combo = JsonConvert.DeserializeObject<List<Combo>>(apiResponse);
            }
            ViewBag.ComboList = combo;
            return View(FoodItemList);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreateCombo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCombo(Combo combo)
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Token not available. Please log in.";
                return View("Login");
            }
            List<FoodItem> FoodItemList = new List<FoodItem>();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(combo), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7251/api/Comboes", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(ViewList));
            }

            return View(combo);
        }
        [HttpPost]
        public async Task<IActionResult> Create(FoodItem fooditem)
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Token not available. Please log in.";
                return View("Login");
            }
            List<FoodItem> FoodItemList = new List<FoodItem>();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(fooditem), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7251/api/FoodItems", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(ViewList));
            }

            return View(fooditem);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

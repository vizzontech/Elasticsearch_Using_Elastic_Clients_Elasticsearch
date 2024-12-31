using Elasticsearch_Using_Elastic.Clients.Elasticsearch.Models;
using Elasticsearch_Using_Elastic.Clients.Elasticsearch.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elasticsearch_Using_Elastic.Clients.Elasticsearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ISearchService _searchService;
        private static int _maxSize = 1000;
        public HomeController(ISearchService searchService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _searchService = searchService;
        }

        public async Task<IActionResult> Index(
         int pageNumber = 1,
         int pageSize = 10,
         string searchString = null)
        {
            ViewData["SearchString"] = searchString;

            var airbnbDatas = new List<AirbnbData>();

            var matchAll = await _searchService.SearchDocumentsByName(pageNumber, _maxSize, searchString);

            if (matchAll != null && matchAll.Documents.Any())
            {
                foreach(var doc in matchAll.Hits)
                {
                    AirbnbData? airbnb = doc.Source;
                    airbnb._id = doc.Id;
                    airbnbDatas.Add(airbnb);
                }
            }

            _logger.LogInformation($"Searched document count :{airbnbDatas.Count}");
            var pagedList = PaginatedList<AirbnbData>.Create(airbnbDatas.AsQueryable(), pageNumber, pageSize);

            return View(pagedList);
        }

        public IActionResult Create()
        {
            return View (new AirbnbData() { id = Guid.NewGuid().ToString() });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AirbnbData airbnb)
        {
            await _searchService.AddDoc(airbnb);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            AirbnbData data = await _searchService.Get(id);

            return View(data ?? new AirbnbData());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, AirbnbData airbnb)
        {
            await _searchService.UpdateDoc(id, airbnb);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _searchService.DeleteDoc(id);

            return RedirectToAction(nameof(Index), new {
                pageNumber = 1,
                pageSize = 10,
                searchString = "bed"
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

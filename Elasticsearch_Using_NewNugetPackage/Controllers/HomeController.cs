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
         string searchString = "")
        {
            ViewData["SearchString"] = searchString;

            IReadOnlyCollection<AirbnbData> airbnbDatas = new List<AirbnbData>();

            var matchAll = await _searchService.SearchDocumentsByName(pageNumber, _maxSize, searchString);

            if (matchAll != null && matchAll.Documents.Any())
                airbnbDatas = matchAll.Documents;

            _logger.LogInformation($"Searched document count :{airbnbDatas.Count}");
            var pagedList = PaginatedList<AirbnbData>.Create(matchAll.Documents.AsQueryable(), pageNumber, pageSize);

            return View(pagedList);
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

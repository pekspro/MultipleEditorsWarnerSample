using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MultipleEditorsWarnerSample.Database;

namespace MultipleEditorsWarnerSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<ColorEntity> Colors => ColorDatabase.Instance.Colors;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [FromQuery]
        public string? UserName { get; set; }

        public string SuggestedUserName { get; set; } = string.Empty;


        private static int UserNamePos = -1;


        public void OnGet()
        {
            if(string.IsNullOrWhiteSpace(UserName))
            {
                string[] userNames = new string[] { "Alice", "Bob", "Carol", "Dan", "Eve", "Frank", "Grace", "Heidi" };

                SuggestedUserName = userNames[(++UserNamePos) % userNames.Length];
            }
        }
    }
}
